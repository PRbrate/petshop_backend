using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetShop.Core.Audit
{
    public class AuditModel
    {
        public AuditModel(int id, DateTime occurrenceDate, string system, string userId, string userName, string ip, string operationalSystem, string browser, bool mobile, string action, string description, string model)
        {
            Id = id;
            OccurrenceDate = DateTime.UtcNow;
            System = system;
            UserId = userId;
            UserName = userName;
            Ip = ip;
            OperationalSystem = operationalSystem;
            Browser = browser;
            Mobile = mobile;
            Action = action;
            Description = description;
            Model = model;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OccurrenceDate { get; private set; }
        public string System { get; private set; }
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public string Ip { get; private set; }
        public string OperationalSystem { get; private set; }
        public string Browser { get; private set; }
        public bool Mobile { get; private set; }
        public string Action { get; private set; }
        public string Description { get; private set; }
        public string Model { get; private set; }

        public string ModelToJson(object model)
        {
            return JsonSerializer.Serialize(model);
        }
    }
}
