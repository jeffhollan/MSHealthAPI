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

        internal void EndTimeInclusive(DateTime startTime, DateTime endTime)
        {
            if (runActivities != null)
            {
                runActivities.RemoveAll(a => (a.endTime < startTime || a.endTime > endTime));
                if (runActivities.FirstOrDefault() == null)
                    runActivities = null;
            }
            if (bikeActivities != null)
            { 
                bikeActivities.RemoveAll(a => (a.endTime < startTime || a.endTime > endTime));
                    if (bikeActivities.FirstOrDefault() == null)
                        bikeActivities = null;
            }
            if (freePlayActivities != null)
            { 
                freePlayActivities.RemoveAll(a => (a.endTime < startTime || a.endTime > endTime));
                if (freePlayActivities.FirstOrDefault() == null)
                    freePlayActivities = null;
            }
            if (guidedWorkoutActivities != null)
            { 
                guidedWorkoutActivities.RemoveAll(a => (a.endTime < startTime || a.endTime > endTime));
                if (guidedWorkoutActivities.FirstOrDefault() == null)
                    guidedWorkoutActivities = null;
            }
            if (golfActivities != null)
            { 
                golfActivities.RemoveAll(a => (a.endTime < startTime || a.endTime > endTime));
                if (golfActivities.FirstOrDefault() == null)
                    golfActivities = null;
            }
            if (sleepActivities != null)
            { 
                sleepActivities.RemoveAll(a => (a.endTime < startTime || a.endTime > endTime));
                if (sleepActivities.FirstOrDefault() == null)
                    sleepActivities = null;
            }
        }

        internal bool NoActivities()
        {
            if (runActivities == null && bikeActivities == null && freePlayActivities == null && guidedWorkoutActivities == null && golfActivities == null && sleepActivities == null)
                return true;
            else
                return false;
        }

    }
}