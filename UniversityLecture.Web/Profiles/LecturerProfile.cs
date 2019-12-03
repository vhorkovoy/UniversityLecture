using AutoMapper;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;

namespace UniversityLecture.WEB.Profiles
{
    public class LecturerProfile : Profile
    {
        public LecturerProfile()
        {
            CreateMap<Lecturer, LecturerDto>();
        }
    }
}