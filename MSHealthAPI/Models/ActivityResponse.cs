using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class ActivityResponse
    {
        public List<RunResponse> runActivites { get; set; }
        public List<BikeResponse> bikeActivities { get; set; }
        public List<FreePlayResponse> freePlayActivites { get; set; }
        public List<GuidedWorkoutResponse> guidedWorkoutActivities { get; set; }

        public List<GolfResponse> golfActivities { get; set; }
        public List<SleepResponse> sleepActivities { get; set; }
        public ActivityResponse(ActivityList activityList)
        {
            runActivites = mapRuns(activityList.runActivities);
            bikeActivities = mapBikes(activityList.bikeActivities);
            freePlayActivites = mapFreePlays(activityList.freePlayActivites);
            guidedWorkoutActivities = mapGuidedWorkouts(activityList.guidedWorkoutActivities);
            golfActivities = mapGolfs(activityList.golfActivities);
            sleepActivities = mapSleeps(sleepActivities);
        }

        

        private List<RunResponse> mapRuns(List<RunActivity> runActivities)
        {
            List<RunResponse> runResponses = new List<RunResponse>();
            foreach(var activity in runActivites)
            {
               
            }
        }

        private List<BikeResponse> mapBikes(List<BikeActivity> bikeActivities)
        {
            throw new NotImplementedException();
        }

        private List<FreePlayResponse> mapFreePlays(List<FreePlayActivity> freePlayActivites)
        {
            throw new NotImplementedException();
        }

        private List<GolfResponse> mapGolfs(List<GolfActivity> golfActivities)
        {
            throw new NotImplementedException();
        }

        private List<GuidedWorkoutResponse> mapGuidedWorkouts(List<GuidedWorkoutActivity> guidedWorkoutActivities)
        {
            throw new NotImplementedException();
        }

        private List<SleepResponse> mapSleeps(List<SleepResponse> sleepActivities)
        {
            throw new NotImplementedException();
        }
        public abstract class AbstractActivityResponse
        {
            public string activityType { get; set; }
            public string id { get; set; }
            public string userId { get; set; }
            public DateTime startTime { get; set; }
            public DateTime endTime { get; set; }
            public DateTime dayId { get; set; }
            public DateTime createdTime { get; set; }
            public string createdBy { get; set; }
            public string name { get; set; }
            public string duration { get; set; }
            public string period { get; set; }
            public int averageHeartRate { get; set; }
            public int peakHeartRate { get; set; }
            public int lowestHeartRate { get; set; }
            public int totalCalories { get; set; }
        }
        public class RunResponse : AbstractActivityResponse
        {
            public string pausedDuration { get; set; }
            public long? splitDistance { get; set; }

            public int finishHeartRate { get; set; }
            public int recoveryHeartRateAt1Minute { get; set; }
            public int recoveryHeartRateAt2Minutes { get; set; }

            public int? underAerobic { get; set; }
            public int? aerobic { get; set; }
            public int? anaerobic { get; set; }
            public int? fitnessZone { get; set; }
            public int? healthyHeart { get; set; }
            public int? redline { get; set; }
            public int? overRedline { get; set; }
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

        public class BikeResponse : AbstractActivityResponse
        {
        }

        public class FreePlayResponse : AbstractActivityResponse
        {
        }

        public class GuidedWorkoutResponse : AbstractActivityResponse
        {
        }

        public class GolfResponse : AbstractActivityResponse
        {
        }

        public class SleepResponse : AbstractActivityResponse
        {
        }
    }
}