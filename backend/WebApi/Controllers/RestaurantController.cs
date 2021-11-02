using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace WebApi.Controllers
{
    public class RestaurantRequest
    {
        public IFormFile FormFile { get; set; }
        public Restaurant Restaurant { get; set; }
    }

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
        /// <param name="restaurantDto">Representation of the restaurant</param>
        /// <returns>id, which is the Identifier for the new restaurant</returns>
        [HttpPost(Name = nameof(Post))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] Credentials creds, [FromForm] RestaurantDto restaurantDto)
        {
            try
            {
                string id = _restaurantService.Register(creds, restaurantDto);
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
        /// <param name="restaurant">Representation of the restaurant</param>
        /// <param name="formFile">Representation of the photo</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(Put))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Put(string id, [FromForm] IFormFile formFile, [FromQuery] Restaurant restaurant)
        {
            try
            {
                Image image = null;

                /*if (formFile != null)
                {
                    var extension = Path.GetExtension(formFile.FileName);
                    var filename = Path.Combine(restaurant.Name + extension);
                    var stream = formFile.OpenReadStream();

                    image = new Image(stream, filename);
                }*/

                _restaurantService.UpdateRestaurant(restaurant, image);

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Food>))]
        public IActionResult GetAllFoodFromRestaurant(string id)
        {
            var foods = _restaurantService.GetAllFoodFromRestaurant(id);

            return Ok(foods);
        }
    }
}
