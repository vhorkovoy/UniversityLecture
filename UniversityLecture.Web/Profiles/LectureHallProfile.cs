using AutoMapper;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;

namespace UniversityLecture.WEB.Profiles
{
    public class LectureHallProfile : Profile
    {
        public LectureHallProfile()
        {
            CreateMap<LectureHall, LectureHallDto>();
        }
    }
}