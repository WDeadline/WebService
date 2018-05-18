using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService.Data;
using WebService.Helpers;
using WebService.Models;
using WebService.Models.UserFromModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Authorize(Policy = "UserIsManager")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            var applicationUsers = _context.ApplicationUsers.ToList();
            return applicationUsers;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var applicationUser = _context.ApplicationUsers.FirstOrDefault(a => a.ID == id);
            return Ok(applicationUser);
        }

        // GET api/<controller>/example@gmail.com
        [HttpGet("{email}")]
        public ApplicationUser Get(string email)
        {
            var applicationUser = _context.ApplicationUsers.FirstOrDefault(a => a.Email == email);
            return applicationUser;
        }
        
        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = "UserIsAdministrator")]
        public IActionResult Post([FromBody]ApplicationUser model)
        {
            Helper helper = new Helper();
            model.Password = helper.GetMD5(model.Password);
            _context.ApplicationUsers.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetAccount", new { id = model.ID }, model);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "UserIsAdministrator")]
        public IActionResult Put(int id, [FromBody]InformationFromModel model)
        {
            var user = _context.ApplicationUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = model.Name;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.Role = model.Role;
            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "UserIsAdministrator")]
        public IActionResult Put(int id, [FromBody]PasswordFromModel model)
        {
            Helper helper = new Helper();
            var user = _context.ApplicationUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Password = helper.GetMD5(model.Password);
            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "UserIsAdministrator")]
        public IActionResult Delete(int id)
        {
            var user = _context.ApplicationUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.ApplicationUsers.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
