namespace PetShop.Domain.Entities
{
    public class ServiceGroup
    {
        public int AppointmentId { get; set; }
        public Appointments Appointments { get; set; }

        public int ServiceId { get; set; }
        public Service Services { get; set; }

    }
}
