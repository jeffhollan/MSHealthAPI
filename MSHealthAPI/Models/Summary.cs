using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class Summary
    {
        public string userId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string parentDay { get; set; }
        public bool isTransitDay { get; set; }
        public string period { get; set; }
        public string duration { get; set; }
        public int stepsTaken { get; set; }
        public CalorieSummary caloriesBurnedSummary { get; set; }
        public HeartrateSummary heartRateSummary { get; set; }
        public DistanceSummary distanceSummary { get; set; }
    }

    public class CalorieSummary
    {
        public string period { get; set; }
        public int totalCalories { get; set; }
    }

    public class HeartrateSummary
    {
        public string period { get; set; }
        public int averageHeartRate { get; set; }
        public int peakHeartRate { get; set; }
        public int lowestHeartRate { get; set; }
    }

    public class DistanceSummary
    {
        public string period { get; set; }
        public long totalDistance { get; set; }
        public long totalDistanceOnFoot { get; set; }
    }
}