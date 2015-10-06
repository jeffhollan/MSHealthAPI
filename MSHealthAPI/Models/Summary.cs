using MSHealthAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class Summary
    {
        private DateTimeOffset _startTime;
        private DateTimeOffset _endTime;
        private DateTimeOffset _parentDay;

        public string userId { get; set; }
        public DateTimeOffset startTime { get { return _startTime; } set { _startTime = value.ToOffset(MSHealthController.timezoneOffset); } }
        public DateTimeOffset endTime { get { return _endTime; } set { _endTime = value.ToOffset(MSHealthController.timezoneOffset); } }
        public DateTimeOffset parentDay { get { return _endTime.Date; } set { _parentDay = value; } }
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