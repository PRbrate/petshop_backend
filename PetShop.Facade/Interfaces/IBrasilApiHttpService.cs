using PetShop.Core.Entities;

namespace PetShop.Facade.Interfaces
{
    public interface IBrasilApiHttpService
    {
        Task<Response<CnpjResponse>> GetCnpj(string cnpj);
        Task<Response<CepResponse>> GetCep(string cep);

    }
}
