using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class SummaryResponse
    {
        public JObject response {
            get; set; }

        public SummaryResponse(string v)
        {
            response = JObject.Parse(v);
            
        }
    }
}