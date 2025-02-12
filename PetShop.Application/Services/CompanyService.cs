using PetShop.Application.DTO;
using PetShop.Application.Filters;
using PetShop.Application.MappingsConfig;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using PetShop.Domain.Entities.Validations.Services;
using PetShop.Facade.Interfaces;

namespace PetShop.Application.Services
{
    public class CompanyService : ICompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IBrasilApiHttpService _brasilApiHttpService;

        public CompanyService(ICompaniesRepository companiesRepository, IBrasilApiHttpService brasilApiHttpService)
        {
            _companiesRepository = companiesRepository;
            _brasilApiHttpService = brasilApiHttpService;
        }
        public async Task<bool> DeleteCompany(int id)
        {
            var getCompany = await _companiesRepository.GetAsync(id);
            if (getCompany == null)
            {
                return false;
            }
            await _companiesRepository.Delete(getCompany);
            return true;
        }

        public async Task<InternalResponse<List<CompaniesDto>>> GetAllCompanies()
        {
            var response = new InternalResponse<List<CompaniesDto>>();

            var companies = await _companiesRepository.GetAllAsync();
            var allcompanies = new List<CompaniesDto>();

            foreach (var item in companies)
            {
                allcompanies.Add(AutoMapperCompanies.ToCompaniesDto(item));
            }

            response.Data = allcompanies;
            return response;

        }
        public async Task<InternalResponse<CompaniesDto>> GetCompaniesByRegisterNumber(string registrationNumber)
        {
            var response = new InternalResponse<CompaniesDto>();
            registrationNumber = new string(registrationNumber.Where(char.IsDigit).ToArray());
            if (!CnpjValidatorService.ValidarCnpj(registrationNumber))
            {
                response.Success = false;
                response.Errors = "Invalid CNPJ";
                return response;
            }

            var getCompany = await _companiesRepository.GetByRegistrationNumberAsync(registrationNumber);

            if (getCompany == null)
            {
                response.Success = false;
                response.Errors = "Companies Not Found";
                return response;
            }

            response.Data = AutoMapperCompanies.ToCompaniesDto(getCompany);

            return response;
        }

        public async Task<InternalResponse<CompaniesDto>> GetCompany(int id)
        {

            var response = new InternalResponse<CompaniesDto>();

            var getCompany = await _companiesRepository.GetAsync(id);

            if (getCompany == null)
            {
                response.Success = false;
                response.Errors = "Companies Not Found";
                return response;
            }

            response.Data = AutoMapperCompanies.ToCompaniesDto(getCompany);

            return response;
        }

        public async Task<InternalResponse<CompaniesDto>> CreateCompany(CompaniesDto companiesDto)
        {

            var response = new InternalResponse<CompaniesDto>();

            if (!CnpjValidatorService.ValidarCnpj(companiesDto.RegistrationNumber))
            {
                response.Success = false;
                response.Errors = "Invalid CNPJ";
                return response;
            }
            if (!EmailValidatorService.VerifyEmail(companiesDto.Email))
            {
                response.Success = false;
                response.Errors = "Invalid Email";
                return response;
            }
            if (!PhoneNumberValidatorService.VerifyPhoneNumber(companiesDto.PhoneNumber))
            {
                response.Success = false;
                response.Errors = "Invalid PhoneNumber";
                return response;
            }
            var company = AutoMapperCompanies.ToCompanies(companiesDto);


            var companyGet = await _brasilApiHttpService.GetCnpj(companiesDto.RegistrationNumber);

            if (companyGet.Data.descricao_situacao_cadastral != "ATIVA")
            {
                response.Success = false;
                response.Errors = "We can only register companies with active status";
                return response;
            }
            if (await _companiesRepository.GetByEmailAsync(companiesDto.Email) != null)
            {
                response.Success = false;
                response.Errors = "this Email already exists";
                return response;
            }

            if (await _companiesRepository.GetByRegistrationNumberAsync(companiesDto.RegistrationNumber) != null)
            {
                response.Success = false;
                response.Errors = "this RegistrationNumber already exists";
                return response;
            }

            company.Status = Status.Active;

            await _companiesRepository.Create(company);

            response.Data = AutoMapperCompanies.ToCompaniesDto(company);
            return response;

        }

        public async Task<InternalResponse<CompaniesDto>> UpdateCompany(int id, CompaniesUpdateDto companiesDto)
        {
            var companyByIdData = await _companiesRepository.GetAsync(id);
            var response = new InternalResponse<CompaniesDto>();

            if (companyByIdData == null)
            {
                response.Success = false;
                response.Errors = "this Company not found";
                return response;
            }
            if (!EmailValidatorService.VerifyEmail(companiesDto.Email))
            {
                response.Success = false;
                response.Errors = "Invalid Email";
                return response;
            }
            if (!PhoneNumberValidatorService.VerifyPhoneNumber(companiesDto.PhoneNumber))
            {
                response.Success = false;
                response.Errors = "Invalid PhoneNumber";
                return response;
            }
            if (await _companiesRepository.GetByEmailAsync(companiesDto.Email) != null && companiesDto.Email != companyByIdData.Email)
            {
                response.Success = false;
                response.Errors = "this Email already exists";
                return response;
            }

            AutoMapperCompanies.ToCompanies(companyByIdData, companiesDto);
            companyByIdData.UpdatedAt = DateTime.Now;
            _companiesRepository.Detached(companyByIdData);
            await _companiesRepository.Update(companyByIdData);

            return response;

        }


    }
}
