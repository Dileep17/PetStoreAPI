using Microsoft.EntityFrameworkCore;

namespace PetStoreAPI.Models
{
    public class PetStoreContext : DbContext
    {
        public PetStoreContext(DbContextOptions<PetStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }

}