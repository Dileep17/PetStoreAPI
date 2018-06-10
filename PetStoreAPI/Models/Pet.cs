
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetStoreAPI.Models
{
    public class Pet
    {
        [Key]
        public long Id { get; set; }
        public string Name { set; get; }
        public string Family { set; get; }
        public string Owner { set; get; }

    }
}
