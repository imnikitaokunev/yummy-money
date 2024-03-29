﻿using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService) => _identityService = identityService;

    [HttpGet("users")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await _identityService.GetUsersAsync();
        return Ok(users);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
    {
        var result = await _identityService.SignInAsync(request);
        if (!result.Succeeded)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpPost("signup")]
    public async Task<AuthenticateResponse> SignUpAsync([FromBody] SignUpRequest request)
    {
        var result = await _identityService.SignUpAsync(request);
        return result;
    }
}
