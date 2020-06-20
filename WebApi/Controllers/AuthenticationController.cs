using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineStoreApi.AuthApi;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateService _authService;

        public AuthenticationController(IAuthenticateService userService, IConfiguration configuration)
        {
            _configuration = configuration;
            _authService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var userProfile = request.ToUserProfile();
            var result = await _authService.CreateUser(userProfile, request.Password);
            return result.Succeeded ? StatusCode((int)HttpStatusCode.Created) : GetErrorResult(result);
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return BadRequest();
            if (result.Errors == null) return BadRequest();
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Exception Message",error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login([FromBody]LoginUserRequest request)
        {
            var userProfile = request.ToUserProfile();

            var userClaims = await _authService.LoginUser(userProfile, request.Password);
            if (userClaims == null) return StatusCode((int)HttpStatusCode.Unauthorized);
            var token = GenerateJwt(userClaims);
            var response = new
            {
                access_token = token
            };
            var result = JsonConvert.SerializeObject(response);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateUserProfile([FromBody] UpdateUserProfileRequest request)
        {
            var userId = Request.Headers.GetEmailFromDecodedToken();
            var updatedClaims = await _authService.UpdateUserProfile(userId, request);
            var token = GenerateJwt(updatedClaims);
            var response = new
            {
                access_token = token
            };
            var result = JsonConvert.SerializeObject(response);
            return Ok(result);
        }

        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                notBefore: DateTime.Now,
                claims: claims,
                expires: DateTime.Now.AddMinutes(20).ToLocalTime(),
                signingCredentials: credentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
}