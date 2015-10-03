namespace MSHealthAPI.Models
{
    public class RunActivity : Activity
    {
        public string pausedDuration { get; set; }
        public long? splitDistance { get; set; }

        public PerformanceSummary performanceSummary { get; set; }

        public DistanceSummary distanceSummary {get; set; }
    }

    public class PerformanceSummary
    {
        public int finishHeartRate { get; set; }
        public int recoveryHeartRateAt1Minute { get; set; }
        public int recoveryHeartRateAt2Minutes { get; set; }
        public HeartRateZones heartRateZones { get; set; }

        public class HeartRateZones
        {
            public int? underAerobic { get; set; }
            public int? aerobic { get; set; }
            public int? anaerobic { get; set; }
            public int? fitnessZone { get; set; }
            public int? healthyHeart { get; set; }
            public int? redline { get; set; }
            public int? overRedline { get; set; }
        }
    }
}