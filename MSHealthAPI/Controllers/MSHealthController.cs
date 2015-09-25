using Microsoft.Azure.AppService.ApiApps.Service;
using MSHealthAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MSHealthAPI.Controllers
{
    [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.Unauthorized, "You have not yet authorized.  Please go to https://{url}/authorize to authorize against Microsoft Health Service.  See the GitHub repo for details.")]
    public class MSHealthController : ApiController
    {
        CloudIsolatedStorage storage = Runtime.FromAppSettings().IsolatedStorage;
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetProfile()
        {
            try { 
                string access_token = await GetToken();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
                    var result = await client.GetAsync("https://api.microsofthealth.net/v1/me/Profile");
                    return result;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You have not yet authorized");
            }


        }

        private async System.Threading.Tasks.Task<string> GetToken()
        {
            var json = await storage.ReadAsStringAsync("OAuthResponse");
            if (string.IsNullOrEmpty(json))
                throw new UnauthorizedAccessException("You have not yet authorized");

            var response = JsonConvert.DeserializeObject<OAuthResponse>(json);
            return response.access_token;
        }
    }
}
