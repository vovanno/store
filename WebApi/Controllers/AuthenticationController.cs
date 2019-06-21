using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.VIewDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticateService userService, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _authService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserViewDto user)
        {
            var temp = _mapper.Map<CustomUser>(user);
            var result = await _authService.CreateUser(temp, user.Password);
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
        public async Task<IActionResult> Login(UserViewDto user)
        {
            var temp = _mapper.Map<CustomUser>(user);
            var userClaims = await _authService.LoginUser(temp, user.Password);
            if (userClaims == null) return StatusCode((int)HttpStatusCode.Unauthorized);
            var token = GenerateJwt(userClaims);
            var response = new
            {
                access_token = token,
                userEmail = user.Email
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
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
}