using MSHealthAPI.Controllers;
using System;

namespace MSHealthAPI.Models
{
    public class SleepActivity : Activity
    {
        private DateTimeOffset _wakeupTime;
        private DateTimeOffset _fallAsleepTime;

        public string awakeDuration { get; set; }
        public string sleepDuration { get; set; }
        public int? numberOfWakeups { get; set; }
        public string fallAsleepDuration { get; set; }
        public int? sleepEfficiencyPercentage { get; set; }
        public string totalRestlessSleepDuration { get; set; }
        public string totalRestfulSleepDuration { get; set; }
        public int? restingHeartRate { get; set; }
        public DateTimeOffset fallAsleepTime { get { return _fallAsleepTime; } set { _fallAsleepTime = value.ToOffset(MSHealthController.timezoneOffset); } }
        public DateTimeOffset wakeupTime { get { return _wakeupTime; } set { _wakeupTime = value.ToOffset(MSHealthController.timezoneOffset); } }
    }
}