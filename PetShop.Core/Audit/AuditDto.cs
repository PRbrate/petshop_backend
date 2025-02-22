using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetShop.Core.Audit
{
    class AuditDto
    {
        public AuditDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime OccurrenceDate { get; set; }
        public string System { get; set; }
        public long ContaId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public string OperationalSystem { get; set; }
        public string Browser { get; set; }
        public bool Mobile { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }

        public string ModelToJson(object model)
        {
            return JsonSerializer.Serialize(model);
        }

        public static AuditDto AuditoriaToViewModel(AuditModel audit)
        {
            if (audit == null) return null;

            return new AuditDto
            {
                Id = audit.Id,
                OccurrenceDate = audit.OccurrenceDate,
                System = audit.System,
                UserId = audit.UserId,
                UserName = audit.UserName,
                Ip = audit.Ip,
                OperationalSystem = audit.OperationalSystem,
                Browser = audit.Browser,
                Mobile = audit.Mobile,
                Action = audit.Action,
                Description = audit.Description,
                Model = audit.Model,
            };
        }

        public static AuditModel ViewModelToAuditoria(AuditDto viewModel)
        {
            if (viewModel == null) return null;

            return new AuditModel(
                userId: viewModel.UserId,
                contaId: viewModel.ContaId,
                userName: viewModel.UserName,
                system: viewModel.System,
                ip: viewModel.Ip,
                operationalSystem: viewModel.OperationalSystem,
                browser: viewModel.Browser,
                mobile: viewModel.Mobile,
                action: viewModel.Action,
                description: viewModel.Description,
                model: viewModel.Model
            );
        }
    }
}

