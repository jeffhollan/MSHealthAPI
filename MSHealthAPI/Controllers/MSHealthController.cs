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
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace MSHealthAPI.Controllers
{
    
    public class MSHealthController : ApiController
    {
        public static OAuthResponse authorization;
        //CloudIsolatedStorage storage = Runtime.FromAppSettings().IsolatedStorage;

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/GetProfile")]
        [Metadata("Get Profile", "Returns information about the current health user")]
        public async Task<HttpResponseMessage> GetProfile()
        {
            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized");

            await AuthenticationController.CheckToken();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
                var result = await client.GetAsync("https://api.microsofthealth.net/v1/me/Profile");
                return result;
            }
        }

        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
        [HttpGet, Route("api/GetSummary")]
        [Trigger(TriggerType.Poll, typeof(JObject))]
        [Metadata("Get Summary")]
        public async Task<HttpResponseMessage> GetSummary(string triggerState, string period)
        {
            if (authorization == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorized");

            if (string.IsNullOrEmpty(triggerState))
                triggerState = DateTime.UtcNow.AddDays(-1).ToString("o");
            else
                triggerState = DateTime.Parse(triggerState).ToString("o");

            await AuthenticationController.CheckToken();

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authorization.access_token);
            //    var result = await client.GetAsync(string.Format("https://api.microsofthealth.net/v1/me/Summaries/{0}?startTime={1}", period, triggerState));
            //    return Request.EventTriggered(new SummaryResponse((await result.Content.ReadAsStringAsync())), triggerState = DateTime.UtcNow.ToString("o"));
            //}
            return Request.EventTriggered(authorization, triggerState = DateTime.UtcNow.ToString("o"));
        }

    }
}
