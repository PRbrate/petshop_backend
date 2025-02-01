using PetShop.Core.Base;
using PetShop.Data.Context;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories
{
    public class AppointmentsRepository : RepositoryBase<Appointments>, IAppointmentsRepository
    {
        private readonly PetShopContext _context;

        public AppointmentsRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }
    }
}
