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

        public int UserId { get; set; }
        public Users User { get; set; }

        public string FullName { get; set; }
        public Species Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public bool NeedAttention { get; set; } = false;
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public ICollection<Appointments> Appointments { get; set; }

        public void setAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - Birthdate.Year;
            if (Birthdate.Date > today.AddYears(-age))
            {
                age--;
            }
            Age = age;
        }

    }
}
