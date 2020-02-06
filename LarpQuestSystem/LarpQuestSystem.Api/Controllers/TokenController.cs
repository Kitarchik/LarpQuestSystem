﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LarpQuestSystem.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace LarpQuestSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public TokenController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResult>> Create(LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

            var user = await _signInManager.UserManager.FindByEmailAsync(login.Email);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login.Email) };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsSuperDuperSecretLarpQuestSystemKey")),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}