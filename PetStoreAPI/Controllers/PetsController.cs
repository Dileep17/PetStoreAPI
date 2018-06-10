using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetStoreAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private PetContext _context;

        public PetsController(PetContext context)
        {
            _context = context;
            if (_context.Pets.Count() == 0)
            {
                _context.Pets.Add(new Pet { Name = "Cat", Owner = "CatWomen", Family = "CatWomen" });
                _context.SaveChanges();
            }

        }



        [HttpGet]
        public ActionResult<List<Pet>> GetAllPets()
        {
            return _context.Pets.ToList();

        }
    }
}
