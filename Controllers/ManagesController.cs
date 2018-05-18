using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService.Data;
using WebService.Models.ManageFromModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ManagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var userName = User.Identity.Name;
            yield return userName;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateInfoFromModel model)
        {
            var applicationUser = _context.ApplicationUsers.FirstOrDefault(p => p.ID == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            applicationUser.Name = model.Name;
            applicationUser.Address = model.Address;
            applicationUser.PhoneNumber = model.PhoneNumber;
            _context.ApplicationUsers.Update(applicationUser);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
