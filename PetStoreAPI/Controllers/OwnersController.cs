using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStoreAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {

        private PetStoreContext _storeContext;

        public OwnersController(PetStoreContext context)
        {
            _storeContext = context;
            if (_storeContext.Owners.Count() == 0)
            {
                _storeContext.Owners.Add(new Owner {Name = "Dileep"});
                _storeContext.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Owner>> GetAllOwners()
        {
            return _storeContext.Owners.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetOwner")]
        public ActionResult<Owner> Get(int id)
        {
            var owner = _storeContext.Owners.Find(id);
            if (owner == null)
            {
                NotFound();
            }

            return owner;
        }

        [HttpPost]
        public IActionResult Create(Owner owner)
        {
            _storeContext.Owners.Add(owner);
            _storeContext.SaveChanges();

            return CreatedAtRoute("GetOwner", new { id = owner.Id }, owner);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var owner = _storeContext.Owners.Find(id);
            if (owner == null)
            {
                NotFound();
            }

            _storeContext.Owners.Remove(owner);
            _storeContext.SaveChanges();
            return NoContent();
        }
    }
}
