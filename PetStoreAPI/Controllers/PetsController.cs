using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetStoreAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private PetStoreContext _storeContext;
        
        public PetsController(PetStoreContext storeContext)
        {
            _storeContext = storeContext;
            if (_storeContext.Pets.Count() == 0)
            {
                _storeContext.Pets.Add(new Pet { Name = "Cat", Family = "Cat" });
                _storeContext.SaveChanges();
            }

        }

        [HttpGet]
        public ActionResult<List<Pet>> Get()
        {
            return _storeContext.Pets.ToList();
        }

        [HttpGet("{id}", Name = "GetPet")]
        public ActionResult<Pet> GetById(long id)
        {
            return _storeContext.Pets.Find(id);
        }

        [HttpPost]
        public IActionResult Create(Pet pet)
        {
            _storeContext.Pets.Add(pet);
            _storeContext.SaveChanges();

            return CreatedAtRoute("GetPet", new { id = pet.Id }, pet);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var pet = _storeContext.Pets.Find(id);
            if (pet == null)
            {
                return NotFound();
            }

            _storeContext.Pets.Remove(pet);
            _storeContext.SaveChanges();
            return NoContent();
        }


    }
}
