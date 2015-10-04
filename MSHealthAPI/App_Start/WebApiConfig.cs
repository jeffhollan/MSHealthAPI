using AutoMapper;
using MSHealthAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MSHealthAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<RunActivity, RunResponse>()
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
                .ForMember(d => d.period, o => o.MapFrom(s => s.distanceSummary.period))
                .ForMember(d => d.speed, o => o.MapFrom(s => s.distanceSummary.speed));
                cfg.CreateMap<BikeActivity, BikeResponse>();
                });
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
