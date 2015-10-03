namespace MSHealthAPI.Models
{
    public class GolfActivity : Activity
    {
        public int? totalStepCount { get; set; }
        public int? totalDistanceWalked { get; set; }
        public int? parOrBetterCount { get; set; }
        public int? longestDriveDistance { get; set; }
        public int? longestStrokeDistance { get; set; }
    }
}