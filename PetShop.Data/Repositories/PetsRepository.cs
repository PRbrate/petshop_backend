using PetShop.Core.Base;
using PetShop.Core.Base.Interfaces;
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
    public class PetsRepository : RepositoryBase<Pets>, IPetsRepository
    {
        private readonly PetShopContext _context;

        public PetsRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }
    }
}
