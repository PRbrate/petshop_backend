using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface IServiceRepository : IRepositoryBase<Service>
    {
        Task<Service> GetByName(string name);
    }
}
