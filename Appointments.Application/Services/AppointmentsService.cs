using Appointment.Application.DTO;
using Appointment.Application.MappingsConfig;
using Appointment.Application.Services.Interfaces;
using Appointment.Data.Repositories;
using Appointment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entities;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;

namespace Appointment.Application.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceGroupRepository _serviceGroupRepository;

        public AppointmentsService(IAppointmentsRepository appointmentsRepository, IServiceRepository serviceRepository, IServiceGroupRepository serviceGroupRepository)
        {
            _appointmentsRepository = appointmentsRepository;
            _serviceRepository = serviceRepository;
            _serviceGroupRepository = serviceGroupRepository;
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

            appointments.Status = Status.Active;
            appointments.StatusAppointment = StatusAppointments.inProgress;
            await _appointmentsRepository.Create(appointments);
            response.Success = true;
            return response;
        }
        public async Task<bool> DeleteAppointment(int id)
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

        #region gets
        public async Task<Response<PaginationResult<AppointmentsReturnDto>>> GetAll(int pageIndex, int pageSize)
        {
            var appointments = _appointmentsRepository.GetAllAsync();
            
            var items = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active)
                .Include(p => p.ServiceGroups)
                .ThenInclude(sg => sg.Services)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<AppointmentsReturnDto>();
            foreach (Appointments a in items)
            {
                list.Add(AutoMapperAppointments.MapReturn(a));
            }
            var pag = new PaginationResult<AppointmentsReturnDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<AppointmentsReturnDto>>(pag);
            return response;
        }
        public async Task<Response<AppointmentsReturnDto>> GetById(int id)
        {
            var response = new Response<AppointmentsReturnDto>();

            var appointments = await _appointmentsRepository.getAppointment(id);
            if (appointments == null || appointments.Status == Status.Inactive)
            {
                response.Success = false;
                response.Errors = "Pet Not Found";
                return response;
            }
            response.Data = AutoMapperAppointments.MapReturn(appointments);
            return response;
        }
        public async Task<Response<AppointmentsReturnDto>> GetByPet(int petId)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var appointments1 = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active && p.PetId == petId)
                .Include(p => p.ServiceGroups)
                .ThenInclude(sg => sg.Services)
                .FirstOrDefaultAsync();

            var appointment = AutoMapperAppointments.MapReturn(appointments1);
            var response = new Response<AppointmentsReturnDto>(appointment);
            return response;
        }
        public async Task<Response<PaginationResult<AppointmentsReturnDto>>> GetByStatus(StatusAppointments status, int pageIndex, int pageSize)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var items = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active && p.StatusAppointment == status)
                .Include(p => p.ServiceGroups)
                .ThenInclude(sg => sg.Services)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<AppointmentsReturnDto>();
            foreach (Appointments a in items)
            {
                list.Add(AutoMapperAppointments.MapReturn(a));
            }
            var pag = new PaginationResult<AppointmentsReturnDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<AppointmentsReturnDto>>(pag);
            return response;
        }
        public async Task<Response<PaginationResult<AppointmentsReturnDto>>> GetByUser(int userId, int pageIndex, int pageSize)
        {
            var appointments = _appointmentsRepository.GetAllAsync();

            var items = await appointments
                .OrderBy(p => p.AppointmentDate)
                .Where(p => p.Status == Status.Active && p.UserId == userId)
                .Include(p => p.ServiceGroups)
                .ThenInclude(sg => sg.Services)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var totalCount = items.Count();
            var list = new List<AppointmentsReturnDto>();
            foreach (Appointments a in items)
            {
                list.Add(AutoMapperAppointments.MapReturn(a));
            }
            var pag = new PaginationResult<AppointmentsReturnDto>(list, totalCount, pageIndex, pageSize);
            var response = new Response<PaginationResult<AppointmentsReturnDto>>(pag);
            return response;
        }
        #endregion

        public Task<Response<AppointmentsDto>> UpdateService(int id, AppointmentsDto appointmentsDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddServiceAppointment(int appointmentId, List<int> servicesId)
        {
            var appointment = await _appointmentsRepository.GetAsync(appointmentId);

            var services = await _serviceRepository.GetIdExist(servicesId);

            double totValue = 0;

            var sgps = new List<ServiceGroup>(); 

            foreach (int i in services)
            {
                var sg = new ServiceGroup();
                sg.ServiceId = i;
                sg.AppointmentId = appointmentId;
                sgps.Add(sg);
                totValue += await _serviceRepository.GetValue(i);
            }

            await _serviceGroupRepository.AddServiceGroup(sgps);
            await _appointmentsRepository.AddTotValue(appointment, totValue);

            return true;

        }
    }
}
