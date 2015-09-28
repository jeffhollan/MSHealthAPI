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
        public List<TabularSummary> rows { get; set; }

        public SummaryResponse(Summaries mshealthResponse)
        {
            rows = new List<TabularSummary>();
            foreach (var summary in mshealthResponse.summaries)
            {
                if (DateTime.Parse(summary.endTime) < DateTime.UtcNow)
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
    }

    public class Summaries
    {
        public List<Summary> summaries { get; set; }
    }

    public class TabularSummary
    {
        public string userId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string parentDay { get; set; }
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