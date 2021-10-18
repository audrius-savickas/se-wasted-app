using Contracts.DTOs;
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
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        /// <summary>
        /// Retrieve all food items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Food>))]
        public IActionResult GetAllFood()
        {
            var foods = _foodService.GetAllFood();

            return Ok(foods);
        }

        /// <summary>
        /// Retrieve food item by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Food>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetFoodById(string id)
        {
            var food = _foodService.GetFoodById(id);

            return food != null ? Ok(food) : NotFound();
        }

        /// <summary>
        /// Retrieve restaurnt to which food item belongs
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/restaurant")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetRestaurantOfFood(string id)
        {
            if (_foodService.GetFoodById(id) == null)
            {
                return NotFound();
            }
            else
            {
                var restaurant = _foodService.GetRestaurantOfFood(id);
                return Ok(restaurant);
            }
        }

        /// <summary>
        /// Retrieve type of food by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/type")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TypeOfFood>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetTypeOfFood(string id)
        {
            if (_foodService.GetFoodById(id) == null)
            {
                return NotFound();
            }
            else
            {
                var type = _foodService.GetTypeOfFood(id);
                return Ok(type);
            }
        }
    }
}
