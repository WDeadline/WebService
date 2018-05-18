using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebService.Data;
using WebService.Helpers;
using WebService.Models;
using WebService.Models.AccountFromModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public RegisterController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]RegisterFromModel model)
        {
            IActionResult response = Unauthorized();
            var haveUser = _context.ApplicationUsers.FirstOrDefault(a => a.Email == model.Email);
            if(haveUser == null)
            {
                var user = Register(model);

                if (user != null)
                {
                    var tokenString = BuildToken(user);
                    response = Ok(new { token = tokenString });
                }

                return response;
            }
            return response;
        }

        private string BuildToken(ApplicationUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserHavePermission", user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:JwtIssuer"],
              audience: _configuration["Jwt:JwtIssuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ApplicationUser Register(RegisterFromModel model)
        {
            Helper helper = new Helper();
            var user = new ApplicationUser { Email = model.Email,Name = model.Name, Password = helper.GetMD5(model.Password), Role = Role.Owner };
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
