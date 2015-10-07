using MSHealthAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class Summary
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private DateTime _parentDay;

        public string userId { get; set; }
        public DateTime startTime { get { return _startTime; } set { _startTime = DateTimeOffset.Parse(value.ToString("o")).ToOffset(MSHealthController.timezoneOffset).DateTime; } }
        public DateTime endTime { get { return _endTime; } set { _endTime = DateTimeOffset.Parse(value.ToString("o")).ToOffset(MSHealthController.timezoneOffset).DateTime; } }
        public DateTime parentDay { get { return _endTime.Date; } set { _parentDay = value; } }
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