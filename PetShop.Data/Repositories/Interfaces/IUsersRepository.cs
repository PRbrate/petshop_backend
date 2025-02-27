using PetShop.Core.Base.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Data.Repositories.Interfaces
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {
        Task<Users> AuthenticateUser(string RegistrationNumber, string Password, int i);
        Task<Users> GetUserByRegistrationNumber(string RegistrationNumber);
        Task<Users> GetUserByUserName(string userName);
        Task<Users> GetByEmailAsync(string email);
        Task<List<Users>> GetByPhoneNumber(string phoneNumber);
        void UpdatePasswordUser(int userId, string password);
        string GetPasswordById(int userId);
    }
}
