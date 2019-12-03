using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;
using UniversityLecture.Repo.Interfaces;


namespace UniversityLecture.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LectureHallsController : ULBaseController
    {

        public LectureHallsController(IRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        ///<summary>List all existing lecture halls.</summary>
        ///<remarks>Return all lecture halls.</remarks>
        [HttpGet]
        public IEnumerable<LectureHallDto> Get()
        {
            return _Mapper.Map<List<LectureHall>, List<LectureHallDto>>(_Repo.GetAll<LectureHall>().ToList());
        }
    }
}
