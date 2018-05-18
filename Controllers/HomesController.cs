using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Data;
using WebService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class HomesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomesController(ApplicationDbContext context)
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

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Destination> Get(double latitude, double longitude)
        {
            var destinations = _context.Destinations.Include(d => d.Location).Include(d => d.Pictures).ToList();
            return destinations;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Appreciation model)
        {
            _context.Appreciations.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetAppreciation", new { applicationUserID = model.ApplicationUserID, destinationID = model.DestinationID }, model);
        }
    }
}
