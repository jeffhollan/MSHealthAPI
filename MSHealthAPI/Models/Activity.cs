using MSHealthAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public abstract class Activity
    {
        private DateTimeOffset _startTime;
        private DateTimeOffset _endTime;
        private DateTimeOffset _dayId;
        private DateTimeOffset _createdTime;

        public string activityType { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public DateTimeOffset startTime { get { return _startTime;  } set { _startTime = value.ToOffset(MSHealthController.timezoneOffset); } }
        public DateTimeOffset endTime { get { return _endTime; } set { _endTime = value.ToOffset(MSHealthController.timezoneOffset); } }
        public DateTimeOffset dayId { get { return _endTime.Date; } set { _dayId = value.ToOffset(MSHealthController.timezoneOffset); } }
        public DateTimeOffset createdTime { get { return _createdTime; } set { _createdTime = value.ToOffset(MSHealthController.timezoneOffset); } }
        public string createdBy { get; set; }
        public string name { get; set; }
        public string duration { get; set; }

        public CalorieSummary caloriesBurnedSummary { get; set; }
        public HeartrateSummary heartRateSummary { get; set; }
    }
}