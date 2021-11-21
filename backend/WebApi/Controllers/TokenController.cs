using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contracts.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using WebApi.Helpers;
using WebApi.Options;

namespace WebApi.Controllers
{
    public class TokenController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITokenHelper _tokenHelper;

        public TokenController(IRestaurantService restaurantService, ITokenHelper tokenHelper)
        {
            _restaurantService = restaurantService;
            _tokenHelper = tokenHelper;
        }

        [Route("/token")]
        [HttpPost]
        public IActionResult Create(Credentials creds)
        {
            if (_restaurantService.Login(creds))
            {
                RestaurantDto restaurantDto = _restaurantService.GetRestaurantDtoFromMail(creds.Mail);
                return new ObjectResult(_tokenHelper.GenerateToken(creds.Mail, restaurantDto));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
