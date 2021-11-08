using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Domain.Helpers;
using WebApi.Helpers;
using System;

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
        /// <param name="sortOrder">Optional order by which the restaurants should be sorted</param>
        /// <param name="userCoordinates">Optional coordinates of the user</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll(string sortOrder = null, [FromQuery] Coords userCoordinates = null)
        {
            if(sortOrder != null)
            {
                try
                {
                    InputValidator.ValidateRestaurantSortOrder(sortOrder, userCoordinates);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            var restaurants = _restaurantService.GetAllRestaurants();

            restaurants = EntitySorter.SortRestaurants(restaurants, sortOrder, userCoordinates);
            if (userCoordinates != null)
            {
                restaurants = restaurants.ToList();
                foreach(RestaurantDto restaurant in restaurants)
                {
                    restaurant.DistanceToUser = CoordsHelper.HaversineDistanceKM(userCoordinates, restaurant.Coords);
                }
            }
          
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
                return Ok(restaurant);
            }
            catch(Exception exception)
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
        public IActionResult Post([FromQuery] Credentials creds, [FromBody] RestaurantRegisterRequest restaurantRegisterRequest)
        {
            try
            {
                string id = _restaurantService.Register(creds, restaurantRegisterRequest);
                return CreatedAtAction(nameof(Post), new { id });
            }
            catch (Exception exception)
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
                    ImageURL = restaurantDto.ImageURL
                };

                _restaurantService.UpdateRestaurant(restaurant);

                return Ok();
            }
            catch (Exception exception)
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
            } catch (System.Exception exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        /// <summary>
        /// Retrieves the food served by a restaurant
        /// </summary>
        /// <param name="id">Identifies the restaurant</param>
        /// <param name="sortOrder">Optional order by which the food should be sorted</param>
        /// <returns></returns>
        [HttpGet("{id}/food",Name = nameof(GetAllFoodFromRestaurant))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FoodResponse>))]

        public IActionResult GetAllFoodFromRestaurant(string id, string sortOrder = null)
        {
            if(sortOrder != null)
            {
                try
                {
                    InputValidator.ValidateFoodSortOrder(sortOrder);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            
            var foods = _restaurantService.GetAllFoodFromRestaurant(id).Select(food => FoodResponse.FromEntity(food));

            foods = EntitySorter.SortFoods(foods, sortOrder);

            return Ok(foods);
        }
    }
}
