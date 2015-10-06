using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public abstract class Activity
    {
        public string activityType { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
        public DateTimeOffset dayId { get; set; }
        public DateTimeOffset createdTime { get; set; }
        public string createdBy { get; set; }
        public string name { get; set; }
        public string duration { get; set; }

        public CalorieSummary caloriesBurnedSummary { get; set; }
        public HeartrateSummary heartRateSummary { get; set; }
    }
}