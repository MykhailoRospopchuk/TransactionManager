﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Model.RequestModel;
using TransactionManagement.Services.Interface;

namespace TransactionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHandlerService _tokenHandler;
        private readonly IAuthenticatorService _authenticator;

        public AuthController(ITokenHandlerService tokenHandler, IAuthenticatorService authenticator)
        {
            _tokenHandler = tokenHandler;
            _authenticator = authenticator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Index([FromBody] LoginRequest loginRequest)
        {
            var user = await _authenticator.AuthenticateAsync(loginRequest);
            var token = await _tokenHandler.CreateTokenAsync(user);
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(string expiredToken)
        {
            var refreshResponse = await _tokenHandler.Refresh(expiredToken);
            return Ok(refreshResponse);
        }
    }
}
