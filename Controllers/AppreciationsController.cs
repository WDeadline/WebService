using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService.Data;
using WebService.Models;
using WebService.Models.AppreciationFromModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Authorize(Policy = "UserIsManager")]
    [Route("api/[controller]")]
    public class AppreciationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppreciationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Appreciation> Get()
        {
            var appreciations = _context.Appreciations.ToList();
            return appreciations;
        }

        // GET api/<controller>/5
        [HttpGet("{applicationUserID,destinationID}")]
        public IActionResult Get(AppreciationKeyFromModel model)
        {
            var appreciation = _context.Appreciations.FirstOrDefault(a => a.ApplicationUserID == model.ApplicationUserID && a.DestinationID == model.DestinationID);
            return Ok(appreciation);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Appreciation model)
        {
            var appreciation = _context.Appreciations.FirstOrDefault(a => a.ApplicationUserID == model.ApplicationUserID && a.DestinationID == model.DestinationID);
            if(appreciation != null)
            {
                appreciation.Content = model.Content;
                appreciation.CreateDate = model.CreateDate;
                appreciation.Rating = model.Rating;

                _context.Appreciations.Update(appreciation);
                _context.SaveChanges();
                return CreatedAtRoute("GetAppreciation", new { userID = appreciation.ApplicationUserID, destinationID = appreciation.DestinationID }, appreciation);
            }
            _context.Appreciations.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetAppreciation", new { userID = model.ApplicationUserID, destinationID = model.DestinationID }, model);
        }

        // PUT api/<controller>/5
        [HttpPut("{applicationUserID,destinationID}")]
        public IActionResult Put(AppreciationKeyFromModel key, [FromBody]Appreciation model)
        {
            if(key.ApplicationUserID != model.ApplicationUserID || key.DestinationID != model.DestinationID)
            {
                return BadRequest();
            }
            var appreciation = _context.Appreciations.FirstOrDefault(a => a.ApplicationUserID == model.ApplicationUserID && a.DestinationID == model.DestinationID);
            if (appreciation == null)
            {
                return NotFound();
            }
            appreciation.Content = model.Content;
            appreciation.Rating = model.Rating;
            appreciation.CreateDate = model.CreateDate;
            _context.Appreciations.Update(appreciation);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{applicationUserID,destinationID}")]
        public IActionResult Delete(AppreciationKeyFromModel model)
        {
            var appreciation = _context.Appreciations.FirstOrDefault(a => a.ApplicationUserID == model.ApplicationUserID && a.DestinationID == model.DestinationID);
            if (appreciation == null)
            {
                return NotFound();
            }
            _context.Appreciations.Remove(appreciation);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
