using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;
using UniversityLecture.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UniversityLecture.Bll.Validators;

namespace UniversityLecture.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ULBaseController
    {
        public ReservationsController(IRepository repo, IMapper mapper, 
            ILogger<ReservationsController> logger)
           : base(repo, mapper, logger)
        {
        }

        ///<summary>List all existing reservations.</summary>
        ///<remarks>Return all reservations.</remarks>
        [HttpGet]
        public IEnumerable<ReservationDto> Get()
        {
            return _Mapper.Map<List<Reservation>, List<ReservationDto>>(_Repo.GetAll<Reservation>().ToList());
        }

        ///<summary>Specific reservation.</summary>
        ///<remarks>Return specific reservation by reservation id.</remarks>
        ///<param name = "id">Reservation id</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var reservation = _Repo.GetAll<Reservation>().SingleOrDefault(r => r.ID == id);
            if(reservation == null)
                return NotFound();
            return Ok(_Mapper.Map<Reservation, ReservationDto>(reservation));
        }

        ///<summary>Create new reservation.</summary>
        ///<remarks>
        ///<b>Reservation.Date</b> format <i>'dd.MM.yyyy'</i>. 
        /// <br /> 
        /// <br /> 
        ///<b>Reservation.StartAt</b> format <i>'H:mm'</i> or <i>'hh:mm'</i>
        /// <br /> 
        /// <br /> 
        ///<b>Reservation.EndAt</b> format <i>'H:mm'</i> or <i>'hh:mm'</i>
        ///</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]ReservationDto reservation)
        {
            var reserv = _Mapper.Map<ReservationDto, Reservation>(reservation);
            var validator = new ReservationValidator(_Repo);
            var results = validator.Validate(reserv);
            if (results.IsValid)
            {
                _Repo.Create(reserv);
                _Repo.SaveChanges();
                _Logger.LogInformation($"Reservation {reserv.ID} was created");
                return CreatedAtAction(nameof(Get), new { id = reserv.ID });
            }
            results.Errors.ToList().ForEach(e => ModelState.AddModelError("Reservation is not valid", e.ErrorMessage));
            return BadRequest(new ValidationProblemDetails(ModelState));     
        }

        ///<summary>Delete reservation.</summary>
        ///<remarks>Delete exsting reservation by id.</remarks>
        ///<param name = "id">Reservation id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var reservation = _Repo.GetAll<Reservation>().
                SingleOrDefault(r => r.ID == id);
            if (reservation == null)
                return NotFound();
            _Repo.Delete(reservation);
            _Repo.SaveChanges();
            _Logger.LogInformation($"Reservation {id} was deleted");
            return Ok();
        }
    }
}
