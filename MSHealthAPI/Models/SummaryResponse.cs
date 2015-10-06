using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI.Models
{
    public class SummaryResponse
    {
        public DateTime previousTriggerState { get; set; }

        public List<TabularSummary> rows { get; set; }

        public SummaryResponse(Summaries mshealthResponse, int delay)
        {
            rows = new List<TabularSummary>();
            foreach (var summary in mshealthResponse.summaries)
            {
                if (summary.endTime < DateTime.UtcNow.AddHours((-1) * (1 - delay)))
                {
                    rows.Add(new TabularSummary
                    {
                        userId = summary.userId,
                        startTime = summary.startTime,
                        endTime = summary.endTime,
                        parentDay = summary.parentDay,
                        averageHeartRate = summary.heartRateSummary.averageHeartRate,
                        duration = summary.duration,
                        isTransitDay = summary.isTransitDay,
                        lowestHeartRate = summary.heartRateSummary.lowestHeartRate,
                        peakHeartRate = summary.heartRateSummary.peakHeartRate,
                        period = summary.period,
                        stepsTaken = summary.stepsTaken,
                        totalCalories = summary.caloriesBurnedSummary.totalCalories,
                        totalDistance = summary.distanceSummary.totalDistance,
                        totalDistanceOnFoot = summary.distanceSummary.totalDistanceOnFoot
                    });
                }
            }
        }

        public SummaryResponse(Summaries mshealthResponse, int delay, DateTime lastSyncedBand) : this(mshealthResponse, delay)
        {
            rows.RemoveAll(q => q.endTime > lastSyncedBand);
            previousTriggerState = lastSyncedBand;
        }
    }

    public class Summaries
    {
        public List<Summary> summaries { get; set; }
    }

    public class TabularSummary
    {
        public string userId { get; set; }
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
        public DateTimeOffset parentDay { get; set; }
        public bool isTransitDay { get; set; }
        public string period { get; set; }
        public string duration { get; set; }
        public int stepsTaken { get; set; }
        public int totalCalories { get; set; }
        public int averageHeartRate { get; set; }
        public int peakHeartRate { get; set; }
        public int lowestHeartRate { get; set; }
        public long totalDistance { get; set; }
        public long totalDistanceOnFoot { get; set; }
    }
}