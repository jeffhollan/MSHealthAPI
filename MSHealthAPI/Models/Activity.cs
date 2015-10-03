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
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public DateTime dayId { get; set; }
        public DateTime createdTime { get; set; }
        public string createdBy { get; set; }
        public string name { get; set; }
        public string duration { get; set; }

        public CalorieSummary caloriesBurnedSummary { get; set; }
        public HeartrateSummary heartRateSummary { get; set; }
    }
}