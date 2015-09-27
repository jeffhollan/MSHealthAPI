﻿using Microsoft.Azure.AppService.ApiApps.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MSHealthAPI.Models;
using System.Text;
using System.Configuration;
using TRex.Metadata;

namespace MSHealthAPI.Controllers
{
    public class AuthenticationController : ApiController
    {
        private static string clientId = ConfigurationManager.AppSettings["clientId"];
        private static string redirectUrl = "https://" + ConfigurationManager.AppSettings["siteUrl"] + ".azurewebsites.net/redirect";
        private static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        //private CloudIsolatedStorage storage = Runtime.FromAppSettings().IsolatedStorage;

        [Metadata(Visibility = VisibilityType.Internal)]
        [HttpGet, Route("showRedirect")]
        public string ShowRedirect()
        {
            return "Your redirect URL is: " + redirectUrl;
        }

        [Metadata(Visibility = VisibilityType.Internal)]
        [HttpGet, Route("authorize")]
        public System.Web.Http.Results.RedirectResult Authorize()
        {
            return Redirect(String.Format("https://login.live.com/oauth20_authorize.srf?client_id={0}&scope={1}&response_type=code&redirect_uri={2}", clientId, "mshealth.ReadProfile mshealth.ReadActivityHistory mshealth.ReadDevices mshealth.ReadActivityLocation offline_access", redirectUrl));
        }

        [Metadata(Visibility = VisibilityType.Internal)]
        [HttpGet, Route("redirect")]
        public async Task<HttpResponseMessage> CompleteAuth(string code)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(string.Format("https://login.live.com/oauth20_token.srf?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}&grant_type=authorization_code", clientId, redirectUrl, clientSecret, code), new FormUrlEncodedContent(RequestKeyValue(code)));
                var jsonResult = await result.Content.ReadAsStringAsync();
                var OAuthResult = JsonConvert.DeserializeObject<OAuthResponse>(jsonResult);
                OAuthResult.expires = DateTime.UtcNow.AddSeconds(OAuthResult.expires_in);
                MSHealthController.authorization = OAuthResult;
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully Authenticated");
                
            }
        }

      

        public static async Task CheckToken()
        {
            if (DateTime.UtcNow.CompareTo(MSHealthController.authorization.expires) >= 0)
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(string.Format("https://login.live.com/oauth20_token.srf?client_id={0}&redirect_uri={1}&client_secret={2}&refresh_token={3}&grant_type=refresh_token",
                        clientId, redirectUrl, clientSecret, MSHealthController.authorization.refresh_token));

                    var jsonResult = await result.Content.ReadAsStringAsync();
                    var OAuthResult = JsonConvert.DeserializeObject<OAuthResponse>(jsonResult);
                    OAuthResult.expires = DateTime.UtcNow.AddSeconds(OAuthResult.expires_in);
                    MSHealthController.authorization = OAuthResult;
                }
            }
        }

        private static List<KeyValuePair<string, string>> RequestKeyValue(string code)
        {
            return new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string,string>("redirect_uri", redirectUrl),
                    new KeyValuePair<string,string>("client_secret", clientSecret),
                    new KeyValuePair<string,string>("code", code),
                    new KeyValuePair<string,string>("grant_type", "authorization_code")
            };
        }
    }
}
