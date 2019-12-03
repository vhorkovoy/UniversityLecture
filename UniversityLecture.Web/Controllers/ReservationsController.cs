using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniversityLecture.Core;
using UniversityLecture.WEB.Models;
using UniversityLecture.Repo.Interfaces;
using System.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
        ///<remarks>Return all reservations including lecture hall, lecurer and subject.</remarks>
        [HttpGet]
        public IEnumerable<ReservationDto> Get()
        {
            return _Mapper.Map<List<Reservation>, List<ReservationDto>>(_Repo.GetAll<Reservation>().
                Include(r => r.LectureHall).
                Include(r =>r.Lecturer).
                Include(r => r.Lecturer.Subject).ToList());
        }

        ///<summary>Specific reservation.</summary>
        ///<remarks>Return specific reservation by reservation id.</remarks>
        ///<param name = "id">Reservation id</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var reservation = _Repo.GetAll<Reservation>().
                Include(r => r.LectureHall).
                Include(r => r.Lecturer).
                Include(r => r.Lecturer.Subject).
                SingleOrDefault(r => r.ID == id);
            if(reservation == null)
                return NotFound();
            return Ok(_Mapper.Map<Reservation, ReservationDto>(reservation));
        }

        ///<summary>Create new reservation.</summary>
        ///<remarks>Create new reservation.</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody]ReservationDto reservation)
        {
            var reserv = _Mapper.Map<ReservationDto, Reservation>(reservation);
            _Repo.Create(reserv);
            _Repo.SaveChanges();
            _Logger.LogInformation($"Reservation {reserv.ID} was created");
            return CreatedAtAction(nameof(Get), new { id = reserv.ID });
        }

        ///<summary>Delete reservation.</summary>
        ///<remarks>Delete exsting reservation by id.</remarks>
        ///<param name = "id">Reservation id</param>
        [HttpDelete]
        [Route("{id}")]
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
