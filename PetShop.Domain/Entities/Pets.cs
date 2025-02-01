using PetShop.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain.Entities
{
    public class Pets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PetId { get; set; }

        public int CompanyId { get; set; }
        public Companies Companies { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }


        public string FullName { get; set; }
        public Species Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }


        public ICollection<Appointments> Appointments { get; set; }

    }
}
