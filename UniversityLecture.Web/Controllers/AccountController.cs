using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityLecture.WEB.Models;
using UniversityLecture.Repo.Interfaces;
using UniversityLecture.WEB.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace UniversityLecture.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ULBaseController
    {
        private readonly IAuthenticate _authService;
        public AccountController(ILogger<AccountController> logger, IAuthenticate authService)
           : base(logger: logger)
        {
            _authService = authService;
        }

        ///<summary>Returns JWT.</summary>
        ///<remarks>For testing please use login: 'demo', password: 'demo'.</remarks>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody]AuthenticationDto authentication)
        {
            var token = _authService.GetToken(authentication.Login, authentication.Password);
            if (string.IsNullOrEmpty(token))
            {
                _Logger.LogWarning($"Authentication filed for {authentication.Login}");
                return BadRequest();
            }
            return Ok(token);
        }
    }
}