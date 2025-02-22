using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetShop.Core.Audit
{
    public class AuditModel
    {
        public AuditModel(string userId, long contaId, string userName, string system, string ip, string operationalSystem, string browser, bool mobile, string action, string description, string model = null)
        {
            UserId = userId;
            UserName = userName;
            System = system;
            Ip = ip;
            OperationalSystem = operationalSystem;
            Browser = browser;
            Mobile = mobile;
            Description = description;
            Model = model;
            OccurrenceDate = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
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
