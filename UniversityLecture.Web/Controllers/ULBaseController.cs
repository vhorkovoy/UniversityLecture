using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniversityLecture.Repo.Interfaces;


namespace UniversityLecture.WEB.Controllers
{
    public class ULBaseController : ControllerBase
    {
        protected readonly ILogger _Logger;
        protected readonly IMapper _Mapper;
        protected readonly IRepository _Repo;
        
        public ULBaseController(IRepository repo = null, IMapper mapper = null, ILogger logger = null)
        {
            _Logger = logger;
            _Repo = repo;
            _Mapper = mapper;
        }
       
    }
}
