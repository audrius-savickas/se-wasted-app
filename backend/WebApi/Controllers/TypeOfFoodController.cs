using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfFoodController : ControllerBase
    {
        private readonly ITypeOfFoodService _typeOfFoodService;
        public TypeOfFoodController(ITypeOfFoodService typeOfFoodService)
        {
            _typeOfFoodService = typeOfFoodService;
        }

        /// <summary>
        /// Retrieve all types of food.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TypeOfFood>))]
        public IActionResult GetAll()
        {
            var typesOfFood = _typeOfFoodService.GetAllTypesOfFood();
            typesOfFood ??= new List<TypeOfFood>();

            return Ok(typesOfFood);
        }

        /// <summary>
        /// Retrieve type of food by its id.
        /// </summary>
        /// <param name="id"> Id of type of food. </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TypeOfFood>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(string id)
        {
            var food = _typeOfFoodService.GetTypeOfFoodById(id);

            return food != null ? Ok(food) : NotFound();
        }
    }
}
