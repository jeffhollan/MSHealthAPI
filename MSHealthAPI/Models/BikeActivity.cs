namespace MSHealthAPI.Models
{
    public class BikeActivity : Activity
    {
        public long splitDistance { get; set; }
        public PerformanceSummary performanceSummary { get; set; }
        public DistanceSummary distanceSummary { get; set; }
    }
}