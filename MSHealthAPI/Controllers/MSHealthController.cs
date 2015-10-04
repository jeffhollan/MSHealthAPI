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
                Mapper.CreateMap<ActivityList, ActivityResponse>();
                Mapper.CreateMap<RunActivity, RunResponse>();
                var resultResponse = Mapper.Map<ActivityResponse>(resultList);
                return Request.CreateResponse(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(resultResponse)), "application/json");
            }

        }


        public async Task<HttpResponseMessage> TestMapper()
        {
            ActivityList resultList = JsonConvert.DeserializeObject<ActivityList>(testJson);
            string RunResultString = JsonConvert.SerializeObject(resultList.runActivities.FirstOrDefault());
            var result = JsonConvert.DeserializeObject<RunResponse>(RunResultString);
            //var result = Mapper.Map<RunResponse>(resultList.runActivities.FirstOrDefault());
            return Request.CreateResponse<RunResponse>(result);

        }


        private string testJson = @"{
	""freePlayActivities"": [
		{
			""activityType"": ""FreePlay"",
			""performanceSummary"": {
				""finishHeartRate"": 68,
				""recoveryHeartRateAt1Minute"": 68,
				""heartRateZones"": {
					""underAerobic"": 103,
					""aerobic"": 4,
					""fitnessZone"": 9,
					""healthyHeart"": 22
				}
			},
			""id"": ""2519584076252247969"",
			""startTime"": ""2015-10-03T17:26:14.775+00:00"",
			""endTime"": ""2015-10-03T19:42:59.775+00:00"",
			""dayId"": ""2015-10-03T00:00:00.000+00:00"",
			""duration"": ""PT2H16M45S"",
			""caloriesBurnedSummary"": {
				""period"": ""Activity"",
				""totalCalories"": 467
			},
			""heartRateSummary"": {
				""period"": ""Activity"",
				""averageHeartRate"": 82,
				""peakHeartRate"": 143,
				""lowestHeartRate"": 55
			}
		}
	],
	""runActivities"": [
		{
			""activityType"": ""Run"",
			""performanceSummary"": {
				""finishHeartRate"": 92,
				""heartRateZones"": {
					""underAerobic"": 3,
					""healthyHeart"": 1
				}
			},
			""distanceSummary"": {
				""period"": ""Activity"",
				""totalDistance"": 8554,
				""actualDistance"": 8554,
				""elevationGain"": 266,
				""elevationLoss"": 129,
				""waypointDistance"": 2500,
				""pace"": 1874000
			},
			""splitDistance"": 160934,
			""id"": ""2519584593959552223"",
			""startTime"": ""2015-10-03T03:03:24.044+00:00"",
			""endTime"": ""2015-10-03T03:06:04.044+00:00"",
			""dayId"": ""2015-10-02T00:00:00.000+00:00"",
			""duration"": ""PT2M40S"",
			""caloriesBurnedSummary"": {
				""period"": ""Activity"",
				""totalCalories"": 8
			},
			""heartRateSummary"": {
				""period"": ""Activity"",
				""averageHeartRate"": 92,
				""peakHeartRate"": 98,
				""lowestHeartRate"": 80
			}
		}
	],
	""itemCount"": 2
}";
    }
}
