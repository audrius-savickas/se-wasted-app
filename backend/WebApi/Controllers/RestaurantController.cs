using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Domain.Helpers;

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
        /// <param name="sortOrder">Optional Order by which the restaurants should be sorted</param>
        /// <param name="userCoordinates">Optional coordinates of the user</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll(string sortOrder = null, [FromQuery] Coords userCoordinates = null)
        {
            try
            {
                NewFolder.InputValidator.ValidateSortOrder(sortOrder, userCoordinates);
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }

            var restaurants = _restaurantService.GetAllRestaurants();

            switch (sortOrder)
            {
                case "name":
                    restaurants = restaurants.OrderBy(r => r.Name);
                    break;
                case "name_desc":
                    restaurants = restaurants.OrderByDescending(r => r.Name);
                    break;
                case "dist":
                    restaurants = restaurants.OrderBy(r => CoordsHelper.HaversineDistanceKM(userCoordinates, r.Coords));
                    break;
                case "dist_desc":
                    restaurants = restaurants.OrderByDescending(r => CoordsHelper.HaversineDistanceKM(userCoordinates, r.Coords));
                    break;
                default:
                    break;
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
            catch(System.Exception exception)
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
            catch (System.Exception exception)
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
                    Credentials = new Credentials()
                };

                _restaurantService.UpdateRestaurant(restaurant);

                return Ok();
            }
            catch (System.Exception exception)
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
        /// <returns></returns>
        [HttpGet("{id}/food",Name = nameof(GetAllFoodFromRestaurant))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FoodResponse>))]
        public IActionResult GetAllFoodFromRestaurant(string id)
        {
            var foods = _restaurantService.GetAllFoodFromRestaurant(id).Select(food => FoodResponse.FromEntity(food));

            return Ok(foods);
        }
    }
}
