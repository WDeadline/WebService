using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService.Data;
using WebService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Authorize(Policy = "UserIsManager")]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            var locations = _context.Locations.ToList();
            return locations;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var location = _context.Locations.FirstOrDefault(l => l.DestinationID == id);
            return Ok(location);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Location model)
        {
            var location = _context.Locations.FirstOrDefault(l => l.DestinationID == model.DestinationID);
            if(location != null)
            {
                location.Latitude = model.Latitude;
                location.Longitude = model.Longitude;
                _context.Locations.Update(location);
                _context.SaveChanges();
                return CreatedAtRoute("GetLocation", new { id = location.DestinationID }, location);
            }
            _context.Locations.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetLocation", new { id = model.DestinationID }, model);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Location model)
        {
            if(id != model.DestinationID)
            {
                return BadRequest();
            }
            var location = _context.Locations.FirstOrDefault(l => l.DestinationID == model.DestinationID);
            if (location == null)
            {
                return NotFound();
            }
            location.Latitude = model.Latitude;
            location.Longitude = model.Longitude;
            _context.Locations.Update(location);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var location = _context.Locations.FirstOrDefault(l => l.DestinationID == id);
            if (location == null)
            {
                return NotFound();
            }
            _context.Locations.Remove(location);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
