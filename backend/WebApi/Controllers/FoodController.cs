using Contracts.DTOs;
using Domain.Models;
using Domain.Models.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using Services.Interfaces;
using Services.Mappers;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApi.Helpers;

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
        /// <param name="foodParameters">Page number and size</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<FoodResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll([FromQuery] FoodParameters foodParameters)
        {
            try
            {
                InputValidator.ValidateFoodSortOrder(foodParameters.SortOrder);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            var pagedFoodList = _foodService.GetAllFood(foodParameters);

            this.AddPaginationMetadata(pagedFoodList);

            var foodsResp = pagedFoodList.Select(food => food.ToFoodResponse());

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
            var foodResp = _foodService.GetFoodById(id).ToFoodResponse();

            return foodResp != null ? Ok(foodResp) : NotFound();
        }

        /// <summary>
        /// Retrieve the restaurant to which food item belongs.
        /// </summary>
        /// <param name="id"> Id of food item. </param>
        /// <param name="coords"></param>
        /// <returns></returns>
        [HttpGet("{id}/restaurant")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetRestaurant(string id, [FromQuery] Coords coords)
        {
            try
            {
                var restaurant = _foodService.GetRestaurantOfFood(id, coords);
                return Ok(restaurant);
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
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
            catch (EntityNotFoundException exception)
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
            catch(Exception exception) when (exception is EntityNotFoundException || exception is ArgumentException)
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
            catch (AuthorizationException exception)
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
            catch (EntityNotFoundException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
