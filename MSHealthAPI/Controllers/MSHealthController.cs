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
        //CloudIsolatedStorage storage = Runtime.FromAppSettings().IsolatedStorage;

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/GetProfile")]
        [Metadata("Get Profile", "Returns information about the current health user")]
        private async Task<HttpResponseMessage> GetProfile()
        {
            if (authorization == null || authorization.access_token == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");

            await tokenHandler.CheckToken();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync("https://api.microsofthealth.net/v1/me/Profile");
                return result;
            }
        }

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/GetHourlySummary")]
        [Trigger(TriggerType.Poll, typeof(SummaryResponse))]
        [Metadata("Get Hourly Summary")]
        public async Task<HttpResponseMessage> GetHourlySummary(string triggerState)
        { 
            if (string.IsNullOrEmpty(triggerState))
                triggerState = DateTime.UtcNow.AddDays(-1).ToString("o");
            else
            {
                var triggerDate = DateTime.Parse(triggerState);
                if (triggerDate.Hour == DateTime.UtcNow.Hour && triggerDate.Date == DateTime.UtcNow.Date)
                {
                    return Request.EventWaitPoll(TimeSpan.FromMinutes((60 - DateTime.UtcNow.Minute)), triggerState = triggerDate.ToUniversalTime().ToString("o"));
                }
                triggerState = triggerDate.ToUniversalTime().Add(TimeSpan.FromHours(-1)).ToString("o");
            }

            await tokenHandler.CheckToken();

            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized. Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Summaries/{0}?startTime={1}", "Hourly", triggerState));
                return Request.EventTriggered( new SummaryResponse(JsonConvert.DeserializeObject<Summaries>((await result.Content.ReadAsStringAsync())))  , triggerState = DateTime.UtcNow.ToString("o"));
            }
        }

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "MSHealth Activity Result", typeof(ActivityResponse))]
        [HttpGet, Route("api/GetActivites")]
        [Metadata("Get Activites", "Returns a set of activities and their data from Microsoft Health")]
        public async Task<HttpResponseMessage> GetActivites(string startTime)
        {
            if (string.IsNullOrEmpty(startTime))
                startTime = DateTime.UtcNow.AddDays(-1).ToString("o");
            else
                startTime = DateTime.Parse(startTime).ToUniversalTime().ToString("o");

            await tokenHandler.CheckToken();

            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized. Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Activities?startTime={0}", startTime));
                string content = await result.Content.ReadAsStringAsync();
                ActivityList resultList = JsonConvert.DeserializeObject<ActivityList>(content);
                //resultList.RemoveActive();
                return Request.CreateResponse<ActivityResponse>(HttpStatusCode.OK, FlattenResult(resultList));
            }

        }


        private ActivityResponse FlattenResult(ActivityList resultList)
        {

            ActivityResponse result = new ActivityResponse();
            result.runActivites = Mapper.Map<List<RunActivity>, List<RunResponse>>(resultList.runActivities);
            result.bikeActivities = Mapper.Map<List<BikeActivity>, List<BikeResponse>>(resultList.bikeActivities);
            result.freePlayActivites = Mapper.Map<List<FreePlayActivity>, List<FreePlayResponse>>(resultList.freePlayActivities);
            result.golfActivities = Mapper.Map<List<GolfActivity>, List<GolfResponse>>(resultList.golfActivities);
            result.guidedWorkoutActivities = Mapper.Map<List<GuidedWorkoutActivity>, List<GuidedWorkoutResponse>>(resultList.guidedWorkoutActivities);
            result.sleepActivities = Mapper.Map<List<SleepActivity>, List<SleepResponse>>(resultList.sleepActivities);
            return result;

        }

      
    }
}
