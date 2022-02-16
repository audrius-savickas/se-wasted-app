using Contracts.DTOs;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
       
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Register a new customer in the application
        /// </summary>
        /// <param name="creds">Credentials of the customer</param>
        /// <param name="customerRegisterRequest">Representation of the customer</param>
        /// <returns>id, which is the Identifier for the new customer</returns>
        [HttpPost(Name = nameof(RegisterCustomer))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult RegisterCustomer([FromQuery] Credentials creds, [FromBody] CustomerRegisterRequest customerRegisterRequest)
        {
            try
            {
                string id = _customerService.Register(creds, customerRegisterRequest);
                return CreatedAtAction(nameof(RegisterCustomer), new { id });
            }
            catch (AuthorizationException exception)
            {
                return Conflict(exception.Message);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Update a customers password.
        /// </summary>
        /// <param name="credentials">New credentials of the customer</param>
        /// <returns></returns>
        [HttpPut(Name = nameof(ChangeCustomerPass))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ChangeCustomerPass([FromBody] Credentials credentials)
        {
            try
            {
                _customerService.ChangePass(credentials.Mail, credentials.Password);
                return Ok();
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Check if the log in credentials of a customer are correct
        /// </summary>
        /// <param name="creds">Credentials of the customer</param>
        /// <returns>Id of the customer that logged in</returns>
        [HttpPost("Login", Name = nameof(LoginCustomer))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult LoginCustomer([FromBody] Credentials creds)
        {
            if (_customerService.Login(creds))
            {
                return Ok(_customerService.GetCustomerIdFromMail(creds.Mail));
            }
            return Unauthorized("Invalid credentials.");
        }

        /// <summary>
        /// Retrieve all food items that are reserved by customer.
        /// </summary>
        /// <param name="id">Customer identification</param>
        /// <returns></returns>
        [HttpGet("{id}/food")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FoodResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAllReservedFoods(string id)
        {
            try
            {
                return Ok(_customerService.GetReservedFoodFromCustomerId(id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieve customer by id.
        /// </summary>
        /// <param name="id">Customer identification</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCustomerById(string id)
        {
            try
            {
                return Ok(_customerService.GetCustomerDtoById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
