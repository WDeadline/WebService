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
    public class PicturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PicturesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Picture> Get()
        {
            var pictures = _context.Pictures.ToList();
            return pictures;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var picture = _context.Pictures.FirstOrDefault(p => p.ID == id);
            return Ok(picture);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Picture model)
        {
            _context.Pictures.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetPicture", new { id = model.ID }, model);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Picture model)
        {
            if(id != model.ID)
            {
                return BadRequest();
            }
            var picture = _context.Pictures.FirstOrDefault(p => p.ID == model.ID);
            if (picture == null)
            {
                return NotFound();
            }
            picture.DestinationID = model.DestinationID;
            picture.Content = model.Content;
            _context.Pictures.Update(picture);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var picture = _context.Pictures.FirstOrDefault(p => p.ID == id);
            if (picture == null)
            {
                return NotFound();
            }
            _context.Pictures.Remove(picture);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
