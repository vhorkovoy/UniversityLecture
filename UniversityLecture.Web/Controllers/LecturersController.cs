using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;
using UniversityLecture.Repo.Interfaces;
using System.Data.Entity;

namespace UniversityLecture.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturersController : ULBaseController
    {
        public LecturersController(IRepository repo, IMapper mapper)
           : base(repo, mapper)
        {
        }
        ///<summary>List all existing lectures.</summary>
        ///<remarks>Return all lecturer with subject.</remarks>
        [HttpGet]
        public IEnumerable<LecturerDto> Get()
        {
            return _Mapper.Map<List<Lecturer>, List<LecturerDto>>(_Repo.
                GetAll<Lecturer>().Include(l => l.Subject).ToList());
        }
    }
}
