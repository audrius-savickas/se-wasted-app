using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Retrieve all food items.
        /// </summary>
        /// <param name="sortOrder">Optional order by which the food should be sorted</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FoodResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll(string sortOrder = null)
        {
            try
            {
                NewFolder.InputValidator.ValidateFoodSortOrder(sortOrder);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            var foodsResp = _foodService.GetAllFood().Select(food => FoodResponse.FromEntity(food)).ToList();

            foodsResp ??= new List<FoodResponse>();

            switch (sortOrder)
            {
                case "name":
                    foodsResp = foodsResp.OrderBy(f => f.Name).ToList();
                    break;
                case "name_desc":
                    foodsResp = foodsResp.OrderByDescending(f => f.Name).ToList();
                    break;
                case "price":
                    foodsResp = foodsResp.OrderBy(f => f.CurrentPrice).ToList();
                    break;
                case "price_desc":
                    foodsResp = foodsResp.OrderByDescending(f => f.CurrentPrice).ToList();
                    break;
                case "time":
                    foodsResp = foodsResp.OrderBy(f => (DateTime.Now - f.CreatedAt)).ToList();
                    break;
                case "time_desc":
                    foodsResp = foodsResp.OrderByDescending(f => (DateTime.Now - f.CreatedAt)).ToList();
                    break;
                default:
                    break;
            }

            return Ok(foodsResp);
        }

        /// <summary>
        /// Retrieve food item by its id.
        /// </summary>
        /// <param name="id"> Id of food item. </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FoodResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(string id)
        {
            var foodResp = FoodResponse.FromEntity(_foodService.GetFoodById(id));

            return foodResp != null ? Ok(foodResp) : NotFound();
        }

        /// <summary>
        /// Retrieve the restaurant to which food item belongs.
        /// </summary>
        /// <param name="id"> Id of food item. </param>
        /// <returns></returns>
        [HttpGet("{id}/restaurant")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetRestaurant(string id)
        {
            try
            {
                var restaurant = _foodService.GetRestaurantOfFood(id);
                return Ok(restaurant);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Retrieve the types of food by food's id.
        /// </summary>
        /// <param name="id"> Id of food item. </param>
        /// <returns></returns>
        [HttpGet("{id}/type")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TypeOfFood>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetTypes(string id)
        {
            try
            {
                var type = _foodService.GetTypesOfFood(id);
                return Ok(type);
            } 
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        /// <summary>
        /// Register a new Food item.
        /// </summary>
        /// <param name="food"> New food item to be added. </param>
        /// <returns> Id, which is the Identifier for the new food item. </returns>
        [HttpPost(Name = nameof(RegisterFood))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult RegisterFood(Food food)
        {
            try
            {
                string id = _foodService.RegisterFood(food);
                return CreatedAtAction(nameof(RegisterFood), new { id });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Delete a food item.
        /// </summary>
        /// <param name="id"> Id of food item. </param>
        /// <param name="restaurantId"> Id of restaurant from which to delete food item. </param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteFood))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeleteFood(string id, [FromQuery] string restaurantId)
        {
            try
            {
                if (_foodService.GetFoodById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    _foodService.DeleteFood(id, restaurantId);
                    return Ok();
                }
            }
            catch (Exception exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        // TODO: Check if restaurant id and typeOfFood id is valid.
        /// <summary>
        /// Update a food item.
        /// </summary>
        /// <param name="id"> Id of food item. </param>
        /// <param name="updatedFood"> Updated food item (id is not used). </param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateFood))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult UpdateFood(string id, [FromBody] Food updatedFood)
        {
            try
            {
                updatedFood.Id = id;  // Only care about id provided in url, not in body.
                _foodService.UpdateFood(updatedFood);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
