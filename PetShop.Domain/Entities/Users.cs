using PetShop.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Domain.Entities
{
    public class Users
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public int CompanyId { get; set; }
        public Companies Companies { get; set; }


        public string FullName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public string Address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public ICollection<Pets> Pets { get; set; }
        public ICollection<Appointments> Appointments { get; set; }


    }
}
