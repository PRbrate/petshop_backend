using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {
        Task<Users> AuthenticateUser(string RegistrationNumber, string Password);
    }
}
