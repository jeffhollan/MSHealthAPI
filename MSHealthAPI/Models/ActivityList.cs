using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class ActivityList
    {
        public List<RunActivity> runActivities { get; set; }
        public List<BikeActivity> bikeActivities { get; set; }
        public List<FreePlayActivity> freePlayActivites { get; set; }
        public List<GuidedWorkoutActivity> guidedWorkoutActivities { get; set; }

        public List<GolfActivity> golfActivities { get; set; }
        public List<SleepActivity> sleepActivities { get; set; }
    }
}