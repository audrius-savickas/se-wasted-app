﻿using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Retrieve all restaurants
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        public IActionResult GetAll()
        {
            var restaurants = _restaurantService.GetAllRestaurants();

            return Ok(restaurants);
        }

        /// <summary>
        /// Retrieve a single restaurant
        /// </summary>
        /// <param name="id">Identifies uniquely the restaurant</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RestaurantDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(string id)
        {
            var restaurant = _restaurantService.GetRestaurantById(id);

            return (restaurant != null) ? Ok(restaurant) : NotFound();
        }

        /// <summary>
        /// Register a new restaurant in the application
        /// </summary>
        /// <param name="creds">Credentials of the restaurant</param>
        /// <param name="restaurantDto">Representation of the restaurant</param>
        /// <returns>id, which is the Identifier for the new restaurant</returns>
        [HttpPost(Name = nameof(Post))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public IActionResult Post([FromQuery] Credentials creds, [FromBody] RestaurantDto restaurantDto)
        {
            try
            {
                string id = _restaurantService.Register(creds, restaurantDto);
                return CreatedAtAction(nameof(Post), new { id });
            }
            catch (System.Exception exception)
            {
                return Forbid(exception.Message);
            }
        }

        /// <summary>
        /// Update a restaurant. Cannot update creds.
        /// </summary>
        /// <param name="id">Identifies uniquely the restaurant</param>
        /// <param name="restaurantDto">Representation of the restaurant</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(Put))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Put(string id, [FromBody] RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                Id = id,
                Name = restaurantDto.Name,
                Address = restaurantDto.Address,
                Coords = restaurantDto.Coords,
                Credentials = new Credentials()
            };

            _restaurantService.UpdateRestaurant(restaurant);

            return Ok();
        }

        /// <summary>
        /// Delete an existing account
        /// </summary>
        /// <param name="creds">Credentials of the restaurant</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete([FromQuery] Credentials creds)
        {
            try
            {
                _restaurantService.DeleteAccount(creds);
                return Ok();
            } catch (System.Exception exception)
            {
                return Unauthorized(exception.Message);
            }
        }
    }
}
