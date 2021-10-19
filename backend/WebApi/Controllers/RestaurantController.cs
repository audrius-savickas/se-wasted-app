using Contracts.DTOs;
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
        [HttpGet(Name = nameof(GetAllRestaurants))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        public IActionResult GetAllRestaurants()
        {
            var restaurants = _restaurantService.GetAllRestaurants();

            return Ok(restaurants);
        }
    }
}
