using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService.Data;
using WebService.Helpers;
using WebService.Models;
using WebService.Models.AcountFromModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ChangePasswordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChangePasswordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ApplicationUser Get(int id,[FromBody]string password)
        {
            Helper helper = new Helper();
            var applicationUser = _context.ApplicationUsers.FirstOrDefault(a => a.ID == id && a.Password== helper.GetMD5(password));
            return applicationUser;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ChangePasswordFromModel model)
        {
            Helper helper = new Helper();
            var user = _context.ApplicationUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            if(user.Password == helper.GetMD5(model.OldPassword))
            {
                user.Password = helper.GetMD5(model.NewPassword);
                _context.ApplicationUsers.Update(user);
                _context.SaveChanges();
            }
            return NoContent();
        }
    }
}
