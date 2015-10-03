using System;

namespace MSHealthAPI.Models
{
    public class SleepActivity : Activity
    {
        public string awakeDuration { get; set; }
        public string sleepDuration { get; set; }
        public int? numberOfWakeups { get; set; }
        public string fallAsleepDuration { get; set; }
        public int? sleepEfficiencyPercentage { get; set; }
        public string totalRestlessSleepDuration { get; set; }
        public string totalRestfulSleepDuration { get; set; }
        public int? restingHeartRate { get; set; }
        public DateTime fallAsleepTime { get; set; }
        public DateTime wakeupTime { get; set; }
    }
}