using Contracts.DTOs;
using Domain.Models;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApi.Helpers;
using Domain.Models.QueryParameters;
using Services.Mappers;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IEmailService _emailService;

        public RestaurantController(IRestaurantService restaurantService, IEmailService emailService)
        {
            _restaurantService = restaurantService;
            _emailService = emailService;
        }

        /// <summary>
        /// Retrieve all restaurants
        /// </summary>
        /// <param name="restaurantParameters"></param>
        /// <param name="userCoordinates">Optional coordinates of the user</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll([FromQuery] RestaurantParameters restaurantParameters, [FromQuery] Coords userCoordinates = null)
        {
            try
            {
                InputValidator.ValidateRestaurantSortOrder(restaurantParameters.SortOrder, userCoordinates);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            if (userCoordinates != null)
            {
                try
                {
                    userCoordinates = new Coords(userCoordinates.Longitude, userCoordinates.Latitude);
                }
                catch (ArgumentException e)
                {
                    return BadRequest(e.Message);
                }
            }

            var restaurants = _restaurantService.GetAllRestaurants(restaurantParameters);

            this.AddPaginationMetadata(restaurants);

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
            try
            {
                var restaurant = _restaurantService.GetRestaurantById(id);
                restaurant.FoodCount = _restaurantService.GetFoodCountFromRestaurant(id);
                return Ok(restaurant);
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        /// <summary>
        /// Register a new restaurant in the application
        /// </summary>
        /// <param name="creds">Credentials of the restaurant</param>
        /// <param name="restaurantRegisterRequest">Representation of the restaurant</param>
        /// <returns>id, which is the Identifier for the new restaurant</returns>
        [HttpPost(Name = nameof(Post))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult Post([FromQuery] Credentials creds, [FromBody] RestaurantRegisterRequest restaurantRegisterRequest)
        {
            try
            {
                string id = _restaurantService.Register(creds, restaurantRegisterRequest);
                return CreatedAtAction(nameof(Post), new { id });
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
        /// Update a restaurant. Cannot update creds.
        /// </summary>
        /// <param name="id">Identifies uniquely the restaurant</param>
        /// <param name="restaurantDto">Representation of the restaurant</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(Put))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Put(string id, [FromBody] RestaurantDto restaurantDto)
        {
            try
            {
                var restaurant = new Restaurant
                {
                    Id = id,
                    Name = restaurantDto.Name,
                    Address = restaurantDto.Address,
                    Coords = restaurantDto.Coords,
                    Credentials = new Credentials(),
                    Description = restaurantDto.Description,
                    ImageURL = restaurantDto.ImageURL,
                };

                _restaurantService.UpdateRestaurant(restaurant);

                return Ok();
            }
            catch (EntityNotFoundException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        /// <summary>
        /// Update a restaurants password.
        /// </summary>
        /// <param name="credentials">New credentials of the restaurant</param>
        /// <returns></returns>
        [HttpPut(Name = nameof(ChangePass))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ChangePass([FromBody] Credentials credentials)
        {
            try
            {
                _restaurantService.ChangePass(credentials.Mail, credentials.Password);
                return Ok();
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
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
            }
            catch (Exception exception) when (exception is AuthorizationException || exception is EntityNotFoundException)
            {
                return Unauthorized(exception.Message);
            }
        }

        /// <summary>
        /// Retrieves the food served by a restaurant
        /// </summary>
        /// <param name="id">Identifies the restaurant</param>
        /// <param name="foodParameters"></param>
        /// <returns></returns>
        [HttpGet("{id}/food", Name = nameof(GetAllFoodFromRestaurant))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FoodResponse>))]

        public IActionResult GetAllFoodFromRestaurant(string id, [FromQuery] FoodParameters foodParameters)
        {
            try
            {
                InputValidator.ValidateFoodSortOrder(foodParameters.SortOrder);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            var pagedFoodList = _restaurantService.GetAllFoodFromRestaurant(id, foodParameters);

            this.AddPaginationMetadata(pagedFoodList);

            var foodsResp = pagedFoodList.Select(food => food.ToFoodResponse());

            return Ok(foodsResp);
        }

        /// <summary>
        /// Check if the log in credentials are correct
        /// </summary>
        /// <param name="creds">Credentials of the restaurant</param>
        /// <returns>Id of the restaurant that logged in</returns>
        [HttpPost("Login", Name = nameof(Login))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Login([FromBody] Credentials creds)
        {
            if(_restaurantService.Login(creds))
            {
                return Ok(_restaurantService.GetRestaurantDtoFromMail(creds.Mail).Id);
            }
            return Unauthorized("Invalid credentials.");
        }
    }
}
