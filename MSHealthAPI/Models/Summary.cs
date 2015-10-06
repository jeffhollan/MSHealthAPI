using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class Summary
    {
        public string userId { get; set; }
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
        public DateTimeOffset parentDay { get; set; }
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

        public long? actualDistance { get; set; }
        public int? altitudeGain { get; set; }
        public int? altitudeLoss { get; set; }
        public int? maxAltitude { get; set; }
        public int? minAltitude { get; set; }

        public long? waypointDistance { get; set; }
        public int? speed { get; set; }
        public int? pace { get; set; }
        public int? overallPace { get; set; }
    }
}