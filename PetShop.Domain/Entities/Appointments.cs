﻿using PetShop.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PetShop.Domain.Entities
{
    public class Appointments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        public int UserId { get; set; }
        public Users Users { get; set; }

        public int PetId { get; set; }
        public Pets Pets { get; set; }


        public DateTime AppointmentDate { get; set; }
        public StatusAppointments StatusAppointment { get; set; }
        public double TotalPrice { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Notes { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public ICollection<ServiceGroup> ServiceGroups { get; set; }
    }
}
