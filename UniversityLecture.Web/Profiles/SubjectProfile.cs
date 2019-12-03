using AutoMapper;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;

namespace UniversityLecture.WEB.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectDto>()
                .ForMember(dst => dst.Duration, opt => opt.MapFrom(src =>  
                     $"{src.Duration.Hours}:{src.Duration.Minutes}"));
        }
    }
}