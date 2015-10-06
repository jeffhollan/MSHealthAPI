using AutoMapper;
using Microsoft.Azure.AppService.ApiApps.Service;
using MSHealthAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace MSHealthAPI.Controllers
{
    
    public class MSHealthController : ApiController
    {
        public static OAuthResponse authorization;
        private AuthenticationController tokenHandler = new AuthenticationController();
        internal static TimeSpan timezoneOffset;

        //CloudIsolatedStorage storage = Runtime.FromAppSettings().IsolatedStorage;



        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/TriggerOnDeviceSync")]
        [Trigger(TriggerType.Poll, typeof(SummaryResponse))]
        [Metadata("Trigger on Device Sync", "On a device sync, will return hourly summaries up to sync hour")]
        public async Task<HttpResponseMessage> TriggerOnDeviceSynce(string triggerState,
            [Metadata("Timezone Offset", "Timezone offset for desired return time (e.g. -7 for Pacific Daylight Time)", VisibilityType.Default)] int offset = 0)
        {
            timezoneOffset = new TimeSpan(offset, 0, 0);
            if (string.IsNullOrEmpty(triggerState))
                triggerState = DateTime.UtcNow.AddDays(-1).ToString("o");
            else
            {
                var triggerDate = DateTime.Parse(triggerState);
                triggerState = triggerDate.ToUniversalTime().ToString("o");
            }

            await tokenHandler.CheckToken();

            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized. Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var profileResult = await client.GetAsync("https://api.microsofthealth.net/v1/me/Profile");

                var lastSyncedBand = 
                    (DateTime)JObject.Parse((await profileResult.Content.ReadAsStringAsync()))["lastUpdateTime"];
                if (lastSyncedBand == null || lastSyncedBand < DateTime.Parse(triggerState).ToUniversalTime())
                    return Request.EventWaitPoll(null, triggerState);

                var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Summaries/{0}?startTime={1}&endTime={2}", "Hourly", DateTime.Parse(triggerState).ToUniversalTime().ToString("o"),lastSyncedBand.ToUniversalTime().ToString("o")));
                var sumResponse = new SummaryResponse(JsonConvert.DeserializeObject<Summaries>((await result.Content.ReadAsStringAsync())), 1, lastSyncedBand);
                if (sumResponse.rows == null || sumResponse.rows.FirstOrDefault() == null)
                    return Request.EventWaitPoll(null, triggerState);

                return Request.EventTriggered(sumResponse, triggerState = sumResponse.rows.First().endTime.ToUniversalTime().ToString("o"));
            }
        }

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/GetHourlySummary")]
        [Trigger(TriggerType.Poll, typeof(SummaryResponse))]
        [Metadata("Get Hourly Summary")]
        public async Task<HttpResponseMessage> GetHourlySummary(string triggerState,
            [Metadata("Timezone Offset", "Timezone offset for desired return time (e.g. -7 for Pacific Daylight Time)", VisibilityType.Default)] int offset = 0,
            [Metadata("Delay", "How many hours to delay results. This is to help offset the time it takes to sync with your band.  Defaults to 1, must be >= 1.", VisibilityType.Advanced)] int delay = 1)
        {
            timezoneOffset = new TimeSpan(offset, 0, 0);
            if (string.IsNullOrEmpty(triggerState))
                triggerState = DateTime.UtcNow.AddDays(-1).ToString("o");
            else
            {
                var triggerDate = DateTime.Parse(triggerState);
                if (triggerDate.Hour == DateTime.UtcNow.Hour && triggerDate.Date == DateTime.UtcNow.Date)
                {
                    return Request.EventWaitPoll(TimeSpan.FromMinutes((60 - DateTime.UtcNow.Minute)), triggerState = triggerDate.ToUniversalTime().ToString("o"));
                }
                triggerState = triggerDate.ToUniversalTime().Add(TimeSpan.FromHours(-1 * delay)).ToString("o");
            }

            await tokenHandler.CheckToken();

            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized. Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Summaries/{0}?startTime={1}", "Hourly", DateTime.Parse(triggerState).ToUniversalTime().ToString("o")));
                return Request.EventTriggered( new SummaryResponse(JsonConvert.DeserializeObject<Summaries>((await result.Content.ReadAsStringAsync())), delay)  , triggerState = DateTime.UtcNow.ToString("o"));
            }
        }

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/TriggerOnActivity")]
        [Trigger(TriggerType.Poll, typeof(ActivityResponse))]
        [Metadata("Trigger on Activity")]
        public async Task<HttpResponseMessage> TriggerOnActivity(string triggerState,
            [Metadata("Timezone Offset", "Timezone offset for desired return time (e.g. -7 for Pacific Daylight Time)", VisibilityType.Default)] int offset = 0)
        {
            timezoneOffset = new TimeSpan(offset, 0, 0);
            if (string.IsNullOrEmpty(triggerState))
                triggerState = DateTime.UtcNow.ToString("o");
            else
            {
                triggerState = DateTime.Parse(triggerState).ToUniversalTime().ToString("o");
            }

            await tokenHandler.CheckToken();

            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized. Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Activities?startTime={0}", DateTime.Parse(triggerState).AddDays(-1).ToUniversalTime().ToString("o")));
                string content = await result.Content.ReadAsStringAsync();
                ActivityList resultList = JsonConvert.DeserializeObject<ActivityList>(content);
                resultList.EndTimeInclusive(DateTime.Parse(triggerState).ToUniversalTime(), DateTime.UtcNow);
                if (resultList.NoActivities())
                    return Request.EventWaitPoll(null, triggerState);
                else
                    return Request.EventTriggered(FlattenResult(resultList), DateTime.UtcNow.ToString("o"));
            }
        }

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "MSHealth Activity Result", typeof(ActivityResponse))]
        [HttpGet, Route("api/GetActivites")]
        [Metadata("Get Activites", "Returns a set of activities and their data from Microsoft Health")]
        public async Task<HttpResponseMessage> GetActivites([Metadata("Activities After Time", "Will return all activities that ended after the time passed in.")] string activityTime,
            [Metadata("Timezone Offset", "Timezone offset for desired return time (e.g. -7 for Pacific Daylight Time)", VisibilityType.Default)] int offset = 0,
            [Required(AllowEmptyStrings = true)][Metadata("Activites Before Time", "An end-time cap into the activities returned", VisibilityType.Advanced)] string endTime = null)
        {
            timezoneOffset = new TimeSpan(offset, 0, 0);
            string startTime;
            if (string.IsNullOrEmpty(activityTime))
                startTime = DateTime.UtcNow.AddDays(-1).ToString("o");
            else
                startTime = DateTime.Parse(activityTime).ToUniversalTime().AddDays(-1).ToString("o");

            endTime = (string.IsNullOrEmpty(endTime)) ? DateTime.UtcNow.ToString("o") : DateTime.Parse(endTime).ToUniversalTime().ToString("o");

            await tokenHandler.CheckToken();

            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized. Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Activities?startTime={0}", DateTime.Parse(startTime).ToUniversalTime().ToString("o")));
                string content = await result.Content.ReadAsStringAsync();
                ActivityList resultList = JsonConvert.DeserializeObject<ActivityList>(content);
                resultList.EndTimeInclusive(DateTime.Parse(activityTime).ToUniversalTime(), DateTime.Parse(endTime).ToUniversalTime());
                return Request.CreateResponse<ActivityResponse>(HttpStatusCode.OK, FlattenResult(resultList));
            }

        }


        private ActivityResponse FlattenResult(ActivityList resultList)
        {

            ActivityResponse result = new ActivityResponse();
            result.runActivities = Mapper.Map<List<RunActivity>, List<RunResponse>>(resultList.runActivities);
            result.bikeActivities = Mapper.Map<List<BikeActivity>, List<BikeResponse>>(resultList.bikeActivities);
            result.freePlayActivities = Mapper.Map<List<FreePlayActivity>, List<FreePlayResponse>>(resultList.freePlayActivities);
            result.golfActivities = Mapper.Map<List<GolfActivity>, List<GolfResponse>>(resultList.golfActivities);
            result.guidedWorkoutActivities = Mapper.Map<List<GuidedWorkoutActivity>, List<GuidedWorkoutResponse>>(resultList.guidedWorkoutActivities);
            result.sleepActivities = Mapper.Map<List<SleepActivity>, List<SleepResponse>>(resultList.sleepActivities);
            return result;

        }


        ///
        /// Deprioritized for now - no big use case and don't want to expose this personal info on API that could be public by default
        ///
        //[Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        //[HttpGet, Route("api/GetProfile")]
        //[Metadata("Get Profile", "Returns information about the current health user")]
        //public async Task<HttpResponseMessage> GetProfile()
        //{
        //    if (authorization == null || authorization.access_token == null)
        //        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");

        //    await tokenHandler.CheckToken();

        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
        //        var result = await client.GetAsync("https://api.microsofthealth.net/v1/me/Profile");
        //        return result;
        //    }
        //}


    }
}
