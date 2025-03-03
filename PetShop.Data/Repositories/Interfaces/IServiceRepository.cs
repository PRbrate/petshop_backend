using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface IServiceRepository : IRepositoryBase<Service>
    {
        Task<Service> GetByName(string name);
        Task<List<int>> GetIdExist(List<int> ids);
        Task<double> GetValue(int id);
    }
}
