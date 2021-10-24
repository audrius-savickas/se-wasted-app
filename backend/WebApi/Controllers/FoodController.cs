﻿using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
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
        public IActionResult GetAll()
        {
            var foods = _foodService.GetAllFood();
            foods ??= new List<Food>();

            return Ok(foods);
        }

        /// <summary>
        /// Retrieve food item by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Food>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(string id)
        {
            var food = _foodService.GetFoodById(id);

            return food != null ? Ok(food) : NotFound();
        }

        /// <summary>
        /// Retrieve restaurnat to which food item belongs
        /// </summary>
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
        /// Retrieve type of food by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/type")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TypeOfFood>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetType(string id)
        {
            try
            {
                var type = _foodService.GetTypeOfFood(id);
                return Ok(type);
            } 
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        /// <summary>
        /// Register a new Food item
        /// </summary>
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
        /// Delete a food item
        /// </summary>
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
        /// Update a food item
        /// </summary>
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
