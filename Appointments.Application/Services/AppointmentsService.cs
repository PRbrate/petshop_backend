using Appointment.Application.DTO;
using Appointment.Application.MappingsConfig;
using Appointment.Application.Services.Interfaces;
using Appointment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entities;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Appointment.Application.Services
{
    class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IServiceRepository _serviceRepository;

        public AppointmentsService(IAppointmentsRepository appointmentsRepository, IServiceRepository serviceRepository)
        {
            _appointmentsRepository = appointmentsRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Response<AppointmentsDto>> CreateAppointment(AppointmentsDto appointmentsDto)
        {
            var response = new Response<AppointmentsDto>();

            if (appointmentsDto.appointmentDate < DateOnly.FromDateTime(DateTime.Now))
            {
                response.Success = false;
                response.Errors = "the appointment Date cannot be less than today";
                return response;
            }
            var appointments = AutoMapperAppointments.Map(appointmentsDto);

            await _appointmentsRepository.Create(appointments);
            response.Success = true;
            return response;
        }

        public async Task<bool> DeleteService(int id)
        {
            var getAppointment = await _appointmentsRepository.GetAsync(id);
            if (getAppointment == null)
            {
                return false;
            }
            getAppointment.Status = Status.Inactive;
            getAppointment.UpdatedAt = DateTime.Now;
            _appointmentsRepository.Detached(getAppointment);
            await _appointmentsRepository.Delete(getAppointment);
            return true;
        }

        public async Task<Response<PaginationResult<AppointmentsDto>>> GetAll(int pageIndex, int pageSize)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var items = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<AppointmentsDto>();
            foreach (Appointments a in items)
            {
                list.Add(AutoMapperAppointments.Map(a));
            }
            var pag = new PaginationResult<AppointmentsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<AppointmentsDto>>(pag);
            return response;
        }

        public async Task<Response<AppointmentsDto>> GetById(int id)
        {
            var response = new Response<AppointmentsDto>();

            var appointments = await _appointmentsRepository.GetAsync(id);
            if (appointments == null || appointments.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "Pet Not Found";
                return response;
            }
            response.Data = AutoMapperAppointments.Map(appointments);
            return response;
        }

        public async Task<Response<AppointmentsDto>> GetByPet(int petId)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var appointments1 = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active && p.PetId == petId)
                .FirstOrDefaultAsync();

            var appointment = AutoMapperAppointments.Map(appointments1);
            var response = new Response<AppointmentsDto>(appointment);
            return response;
        }

        public async Task<Response<PaginationResult<AppointmentsDto>>> GetByStatus(StatusAppointments status, int pageIndex, int pageSize)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var items = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active && p.StatusAppointment == status)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<AppointmentsDto>();
            foreach (Appointments a in items)
            {
                list.Add(AutoMapperAppointments.Map(a));
            }
            var pag = new PaginationResult<AppointmentsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<AppointmentsDto>>(pag);
            return response;
        }

        public async Task<Response<PaginationResult<AppointmentsDto>>> GetByUser(int userId, int pageIndex, int pageSize)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var items = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active && p.UserId == userId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<AppointmentsDto>();
            foreach (Appointments a in items)
            {
                list.Add(AutoMapperAppointments.Map(a));
            }
            var pag = new PaginationResult<AppointmentsDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<AppointmentsDto>>(pag);
            return response;
        }

        public Task<Response<AppointmentsDto>> UpdateService(int id, AppointmentsDto appointmentsDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddServiceAppointment(int appointmentId, List<int> serviceId)
        {
            var appointment = await _appointmentsRepository.GetAsync(appointmentId);

            var services = _serviceRepository
            var IdExists =
        }
    }
}
