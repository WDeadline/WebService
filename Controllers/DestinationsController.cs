using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class DestinationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestinationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Destination> Get()
        {
            var destinations = _context.Destinations.Include(d => d.Location).Include(d => d.Pictures).ToList();
            return destinations;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var destination = _context.Destinations.Include(d => d.Location).Include(d => d.Pictures).FirstOrDefault(d => d.ID == id);
            return Ok(destination);
        }
        [Authorize(Policy = "UserIsManager")]
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Destination model)
        {
            _context.Destinations.Add(model);
            //var destination = _context.Destinations.Include(d => d.Location).Include(d => d.Pictures).FirstOrDefault(d => d.ID == model.ID);
            _context.SaveChanges();
            return CreatedAtRoute("GetDestination", new { id = model.ID }, model);
        }
        [Authorize(Policy = "UserIsManager")]
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Destination model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            var destination = _context.Destinations.FirstOrDefault(p => p.ID == model.ID);
            if (destination == null)
            {
                return NotFound();
            }
            destination.Name = model.Name;
            destination.Address = model.Address;
            destination.WebsiteUri = model.WebsiteUri;
            destination.Attributions = model.Attributions;
            destination.Location = model.Location;
            _context.Destinations.Update(destination);
            _context.SaveChanges();
            return NoContent();
        }
        [Authorize(Policy = "UserIsManager")]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var destination = _context.Destinations.FirstOrDefault(p => p.ID == id);
            if (destination == null)
            {
                return NotFound();
            }
            _context.Destinations.Remove(destination);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
