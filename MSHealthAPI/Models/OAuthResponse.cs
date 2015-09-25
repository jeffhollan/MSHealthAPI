using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class OAuthResponse
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string user_id { get; set; }

        public DateTime expires;
    }
}