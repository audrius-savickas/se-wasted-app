using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using WebApi.Helpers;
using WebApi.Options;

namespace WebApi.Controllers
{
    public class TokenController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITokenHelper _tokenHelper;
        private readonly GoogleOptions _googleOptions;

        public TokenController
        (
            IRestaurantService restaurantService,
            ITokenHelper tokenHelper,
            IOptions<GoogleOptions> googleOptions
        )
        {
            _restaurantService = restaurantService;
            _tokenHelper = tokenHelper;
            _googleOptions = googleOptions.Value;
        }

        [Route("/token")]
        [HttpPost]
        public IActionResult Create(Credentials creds)
        {
            if (_restaurantService.Login(creds))
            {
                return new ObjectResult(_tokenHelper.GenerateToken(creds.Mail));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/token/google")]
        public IActionResult Authenticate([FromQuery] string idToken)
        {
            GoogleJsonWebSignature.ValidationSettings settings =
                new GoogleJsonWebSignature.ValidationSettings();

            // Change this to your google client ID
            settings.Audience = new List<string>() { _googleOptions.ClientId };

            try
            {
                GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(idToken, settings).Result;
                return new ObjectResult(_tokenHelper.GenerateToken(new Mail(payload.Email)));
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }
    }
}
