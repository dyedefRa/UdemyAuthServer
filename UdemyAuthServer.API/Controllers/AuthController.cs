﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UdemyAuthServer.Core.Dtos;
using UdemyAuthServer.Core.Services;

namespace UdemyAuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenAsync(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDto);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshTokenAsync(refreshTokenDto.Token);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token);
            return ActionResultInstance(result);
        }
    }
}
