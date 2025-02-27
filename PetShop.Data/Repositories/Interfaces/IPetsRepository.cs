using PetShop.Core.Base.Interfaces;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface IPetsRepository : IRepositoryBase<Pets>
    {
        Task<List<Pets>> GetByUserId(int id);
        //Task<IQueryable<Pets>> GetBySpecie();
        //Task<List<Pets>> GetByGender(Gender gender);
        //Task<List<Pets>> GetNeedAttention(bool attention);
    }
}
