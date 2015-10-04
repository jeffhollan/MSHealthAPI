using AutoMapper;
using MSHealthAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSHealthAPI
{
    public class CreateMapping
    {
        public static void CreateMappings()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<RunActivity, RunResponse>()
                .ForMember(d => d.duration, o => o.ResolveUsing<DurationResolver>())
                .ForMember(d => d.finishHeartRate, o => o.MapFrom(s => s.performanceSummary.finishHeartRate))
                .ForMember(d => d.recoveryHeartRateAt1Minute, o => o.MapFrom(s => s.performanceSummary.recoveryHeartRateAt1Minute))
                .ForMember(d => d.recoveryHeartRateAt2Minutes, o => o.MapFrom(s => s.performanceSummary.recoveryHeartRateAt2Minutes))
                .ForMember(d => d.aerobic, o => o.MapFrom(s => s.performanceSummary.heartRateZones.aerobic))
                .ForMember(d => d.anaerobic, o => o.MapFrom(s => s.performanceSummary.heartRateZones.anaerobic))
                .ForMember(d => d.fitnessZone, o => o.MapFrom(s => s.performanceSummary.heartRateZones.fitnessZone))
                .ForMember(d => d.healthyHeart, o => o.MapFrom(s => s.performanceSummary.heartRateZones.healthyHeart))
                .ForMember(d => d.overRedline, o => o.MapFrom(s => s.performanceSummary.heartRateZones.overRedline))
                .ForMember(d => d.redline, o => o.MapFrom(s => s.performanceSummary.heartRateZones.redline))
                .ForMember(d => d.underAerobic, o => o.MapFrom(s => s.performanceSummary.heartRateZones.underAerobic))
                .ForMember(d => d.actualDistance, o => o.MapFrom(s => s.distanceSummary.actualDistance))
                .ForMember(d => d.altitudeGain, o => o.MapFrom(s => s.distanceSummary.altitudeGain))
                .ForMember(d => d.altitudeLoss, o => o.MapFrom(s => s.distanceSummary.altitudeLoss))
                .ForMember(d => d.maxAltitude, o => o.MapFrom(s => s.distanceSummary.maxAltitude))
                .ForMember(d => d.minAltitude, o => o.MapFrom(s => s.distanceSummary.minAltitude))
                .ForMember(d => d.overallPace, o => o.MapFrom(s => s.distanceSummary.overallPace))
                .ForMember(d => d.pace, o => o.MapFrom(s => s.distanceSummary.pace))
                .ForMember(d => d.speed, o => o.MapFrom(s => s.distanceSummary.speed))
                .ForMember(d => d.totalDistance, o => o.MapFrom(s => s.distanceSummary.totalDistance))
                .ForMember(d => d.totalDistanceOnFoot, o => o.MapFrom(s => s.distanceSummary.totalDistanceOnFoot))
                .ForMember(d => d.waypointDistance, o => o.MapFrom(s => s.distanceSummary.waypointDistance))
                .ForMember(d => d.averageHeartRate, o => o.MapFrom(s => s.heartRateSummary.averageHeartRate))
                .ForMember(d => d.lowestHeartRate, o => o.MapFrom(s => s.heartRateSummary.lowestHeartRate))
                .ForMember(d => d.peakHeartRate, o => o.MapFrom(s => s.heartRateSummary.peakHeartRate))
                .ForMember(d => d.period, o => o.MapFrom(s => s.heartRateSummary.period))
                .ForMember(d => d.totalCalories, o => o.MapFrom(s => s.caloriesBurnedSummary.totalCalories));
                cfg.CreateMap<BikeActivity, BikeResponse>()
                .ForMember(d => d.duration, o => o.ResolveUsing<DurationResolver>())
                .ForMember(d => d.finishHeartRate, o => o.MapFrom(s => s.performanceSummary.finishHeartRate))
                .ForMember(d => d.recoveryHeartRateAt1Minute, o => o.MapFrom(s => s.performanceSummary.recoveryHeartRateAt1Minute))
                .ForMember(d => d.recoveryHeartRateAt2Minutes, o => o.MapFrom(s => s.performanceSummary.recoveryHeartRateAt2Minutes))
                .ForMember(d => d.aerobic, o => o.MapFrom(s => s.performanceSummary.heartRateZones.aerobic))
                .ForMember(d => d.anaerobic, o => o.MapFrom(s => s.performanceSummary.heartRateZones.anaerobic))
                .ForMember(d => d.fitnessZone, o => o.MapFrom(s => s.performanceSummary.heartRateZones.fitnessZone))
                .ForMember(d => d.healthyHeart, o => o.MapFrom(s => s.performanceSummary.heartRateZones.healthyHeart))
                .ForMember(d => d.overRedline, o => o.MapFrom(s => s.performanceSummary.heartRateZones.overRedline))
                .ForMember(d => d.redline, o => o.MapFrom(s => s.performanceSummary.heartRateZones.redline))
                .ForMember(d => d.underAerobic, o => o.MapFrom(s => s.performanceSummary.heartRateZones.underAerobic))
                .ForMember(d => d.actualDistance, o => o.MapFrom(s => s.distanceSummary.actualDistance))
                .ForMember(d => d.altitudeGain, o => o.MapFrom(s => s.distanceSummary.altitudeGain))
                .ForMember(d => d.altitudeLoss, o => o.MapFrom(s => s.distanceSummary.altitudeLoss))
                .ForMember(d => d.maxAltitude, o => o.MapFrom(s => s.distanceSummary.maxAltitude))
                .ForMember(d => d.minAltitude, o => o.MapFrom(s => s.distanceSummary.minAltitude))
                .ForMember(d => d.overallPace, o => o.MapFrom(s => s.distanceSummary.overallPace))
                .ForMember(d => d.pace, o => o.MapFrom(s => s.distanceSummary.pace))
                .ForMember(d => d.speed, o => o.MapFrom(s => s.distanceSummary.speed))
                .ForMember(d => d.totalDistance, o => o.MapFrom(s => s.distanceSummary.totalDistance))
                .ForMember(d => d.totalDistanceOnFoot, o => o.MapFrom(s => s.distanceSummary.totalDistanceOnFoot))
                .ForMember(d => d.waypointDistance, o => o.MapFrom(s => s.distanceSummary.waypointDistance))
                .ForMember(d => d.averageHeartRate, o => o.MapFrom(s => s.heartRateSummary.averageHeartRate))
                .ForMember(d => d.lowestHeartRate, o => o.MapFrom(s => s.heartRateSummary.lowestHeartRate))
                .ForMember(d => d.peakHeartRate, o => o.MapFrom(s => s.heartRateSummary.peakHeartRate))
                .ForMember(d => d.period, o => o.MapFrom(s => s.heartRateSummary.period))
                .ForMember(d => d.totalCalories, o => o.MapFrom(s => s.caloriesBurnedSummary.totalCalories));
                cfg.CreateMap<FreePlayActivity, FreePlayResponse>()
                .ForMember(d => d.duration, o => o.ResolveUsing<DurationResolver>())
                .ForMember(d => d.averageHeartRate, o => o.MapFrom(s => s.heartRateSummary.averageHeartRate))
                .ForMember(d => d.lowestHeartRate, o => o.MapFrom(s => s.heartRateSummary.lowestHeartRate))
                .ForMember(d => d.peakHeartRate, o => o.MapFrom(s => s.heartRateSummary.peakHeartRate))
                .ForMember(d => d.period, o => o.MapFrom(s => s.heartRateSummary.period))
                .ForMember(d => d.totalCalories, o => o.MapFrom(s => s.caloriesBurnedSummary.totalCalories));
                cfg.CreateMap<GuidedWorkoutActivity, GuidedWorkoutResponse>()
                .ForMember(d => d.duration, o => o.ResolveUsing<DurationResolver>())
                .ForMember(d => d.averageHeartRate, o => o.MapFrom(s => s.heartRateSummary.averageHeartRate))
                .ForMember(d => d.lowestHeartRate, o => o.MapFrom(s => s.heartRateSummary.lowestHeartRate))
                .ForMember(d => d.peakHeartRate, o => o.MapFrom(s => s.heartRateSummary.peakHeartRate))
                .ForMember(d => d.period, o => o.MapFrom(s => s.heartRateSummary.period))
                .ForMember(d => d.totalCalories, o => o.MapFrom(s => s.caloriesBurnedSummary.totalCalories));
                cfg.CreateMap<GolfActivity, GolfResponse>()
                .ForMember(d => d.duration, o => o.ResolveUsing<DurationResolver>())
                .ForMember(d => d.averageHeartRate, o => o.MapFrom(s => s.heartRateSummary.averageHeartRate))
                .ForMember(d => d.lowestHeartRate, o => o.MapFrom(s => s.heartRateSummary.lowestHeartRate))
                .ForMember(d => d.peakHeartRate, o => o.MapFrom(s => s.heartRateSummary.peakHeartRate))
                .ForMember(d => d.period, o => o.MapFrom(s => s.heartRateSummary.period))
                .ForMember(d => d.totalCalories, o => o.MapFrom(s => s.caloriesBurnedSummary.totalCalories));
                cfg.CreateMap<SleepActivity, SleepResponse>()
                .ForMember(d => d.duration, o => o.ResolveUsing<DurationResolver>())
                .ForMember(d => d.fallAsleepDuration, o => o.ResolveUsing<GenericDurationResolver>().FromMember(s => s.fallAsleepDuration))
                .ForMember(d => d.awakeDuration, o => o.ResolveUsing<GenericDurationResolver>().FromMember(s => s.awakeDuration))
                .ForMember(d => d.sleepDuration, o => o.ResolveUsing<GenericDurationResolver>().FromMember(s => s.sleepDuration))
                .ForMember(d => d.totalRestfulSleepDuration, o => o.ResolveUsing<GenericDurationResolver>().FromMember(s => s.totalRestfulSleepDuration))
                .ForMember(d => d.totalRestlessSleepDuration, o => o.ResolveUsing<GenericDurationResolver>().FromMember(s => s.totalRestlessSleepDuration))
                .ForMember(d => d.averageHeartRate, o => o.MapFrom(s => s.heartRateSummary.averageHeartRate))
                .ForMember(d => d.lowestHeartRate, o => o.MapFrom(s => s.heartRateSummary.lowestHeartRate))
                .ForMember(d => d.peakHeartRate, o => o.MapFrom(s => s.heartRateSummary.peakHeartRate))
                .ForMember(d => d.period, o => o.MapFrom(s => s.heartRateSummary.period))
                .ForMember(d => d.totalCalories, o => o.MapFrom(s => s.caloriesBurnedSummary.totalCalories));
            });
        }

        public class DurationResolver : ValueResolver<Activity, double>
        {
            protected override double ResolveCore(Activity source)
            {
                return System.Xml.XmlConvert.ToTimeSpan(source.duration).TotalMinutes;
            }
        }

        public class GenericDurationResolver : ValueResolver<string, double>
        {
            protected override double ResolveCore(string source)
            {
                if(!string.IsNullOrEmpty(source))
                    return System.Xml.XmlConvert.ToTimeSpan(source).TotalMinutes;

                return 0;
            }
        }
    }
}