using MSHealthAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public abstract class Activity
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private DateTime _dayId;
        private DateTime _createdTime;
  

        public string activityType { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public DateTime startTime { get { return _startTime;  } set { _startTime = DateTimeOffset.Parse(value.ToString("o")).ToOffset(MSHealthController.timezoneOffset).DateTime; } }
        public DateTime endTime { get { return _endTime; } set { _endTime = DateTimeOffset.Parse(value.ToString("o")).ToOffset(MSHealthController.timezoneOffset).DateTime; } }
        public DateTime dayId { get { return _endTime.Date; } set { _dayId = DateTimeOffset.Parse(value.ToString("o")).ToOffset(MSHealthController.timezoneOffset).DateTime; } }
        public DateTime createdTime { get { return _createdTime; } set { _createdTime = DateTimeOffset.Parse(value.ToString("o")).ToOffset(MSHealthController.timezoneOffset).DateTime; } }
        public string createdBy { get; set; }
        public string name { get; set; }
        public string duration { get; set; }

        public CalorieSummary caloriesBurnedSummary { get; set; }
        public HeartrateSummary heartRateSummary { get; set; }
    }
}