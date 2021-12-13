using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Make a new reservation.
        /// </summary>
        /// <returns> Id, which is the Identifier for the new reservation. </returns>
        [HttpPost(Name = nameof(MakeReservation))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult MakeReservation([FromQuery] string foodId, [FromQuery] string customerId)
        {
            try
            {
                string id = _reservationService.MakeReservation(foodId, customerId);
                return CreatedAtAction(nameof(MakeReservation), new { id });
            }
            catch (Exception exception) when (exception is EntityNotFoundException || exception is ArgumentException)
            {
                return BadRequest(exception.Message);
            }
        }

        // atsaukti rezervacija POST  customerId foodId
    }
}
