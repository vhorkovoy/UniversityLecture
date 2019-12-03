using AutoMapper;
using System;
using System.Globalization;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;

namespace UniversityLecture.WEB.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>().
                ForMember(dst => dst.Date, opt => opt.MapFrom(src =>
                    src.StartDate.Date.ToString("dd.MM.yyyy"))).
                ForMember(dst => dst.StartAt, opt => opt.MapFrom(src =>
                    $"{src.StartDate.Hour}:{src.StartDate.Minute}")).
                ForMember(dst => dst.EndAt, opt => opt.MapFrom(src =>
                    $"{src.EndDate.Hour}:{src.EndDate.Minute}"));

            CreateMap<ReservationDto, Reservation>().
                ForMember(dst => dst.StartDate,opt => opt.MapFrom(src => 
                    DateTime.ParseExact($"{src.Date} {src.StartAt}", "d.M.yyyy H:mm", 
                    CultureInfo.InvariantCulture))).
                ForMember(dst => dst.EndDate, opt => opt.MapFrom(src =>
                     DateTime.ParseExact($"{src.Date} {src.EndAt}", "d.M.yyyy H:mm",
                     CultureInfo.InvariantCulture)));
        }
    }
}