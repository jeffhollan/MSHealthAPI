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
        public List<FreePlayActivity> freePlayActivities { get; set; }
        public List<GuidedWorkoutActivity> guidedWorkoutActivities { get; set; }

        public List<GolfActivity> golfActivities { get; set; }
        public List<SleepActivity> sleepActivities { get; set; }

        internal void RemoveActive()
        {
            foreach (var activity in runActivities)
                if (activity.endTime == null)
                    runActivities.Remove(activity);

            foreach (var activity in bikeActivities)
                if (activity.endTime == null)
                    bikeActivities.Remove(activity);

            foreach (var activity in sleepActivities)
                if (activity.endTime == null)
                    sleepActivities.Remove(activity);

            foreach (var activity in freePlayActivities)
                if (activity.endTime == null)
                    freePlayActivities.Remove(activity);

            foreach (var activity in guidedWorkoutActivities)
                if (activity.endTime == null)
                    guidedWorkoutActivities.Remove(activity);

            foreach (var activity in golfActivities)
                if (activity.endTime == null)
                    golfActivities.Remove(activity);
        }
    }
}