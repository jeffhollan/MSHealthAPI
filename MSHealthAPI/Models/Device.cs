using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class Device
    {
        public List<DeviceProfile> deviceProfiles { get; set; }
    }

    public class DeviceProfile
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public DateTime lastSuccessfulSync { get; set; }
        public string deviceFamily { get; set; }
    }
}