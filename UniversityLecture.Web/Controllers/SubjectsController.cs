using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;
using UniversityLecture.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace UniversityLecture.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ULBaseController
    {
        public SubjectsController(IRepository repo, IMapper mapper)
            :base(repo, mapper)
        {
        }

        ///<summary>List all existing subjects.</summary>
        ///<remarks>Return all subjects.</remarks>
        [HttpGet]
        public IEnumerable<SubjectDto> Get()
        {
            return _Mapper.Map<List<Subject>, List<SubjectDto>>(_Repo.GetAll<Subject>().ToList());
        }
    }
}
