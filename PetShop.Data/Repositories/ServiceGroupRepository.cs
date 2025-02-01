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
    public class ServiceGroupRepository : RepositoryBase<ServiceGroup>, IServiceGroupRepository
    {
        private readonly PetShopContext _Context;

        public ServiceGroupRepository(PetShopContext context) : base(context)
        {
            _Context = context;
        }
    }
}
