using Appointment.Application.DTO;
using Appointment.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.Application.DTO;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Audit;
using PetShop.Domain.Entities.Enums;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/appointment")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IAuditHelper _audit;
        private readonly IAuditService _auditService;

        public AppointmentController(IAppointmentsService appointmentsService, IAuditHelper audit, IAuditService auditService)
        {
            _appointmentsService = appointmentsService;
            _audit = audit;
            _auditService = auditService;
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointments(AppointmentsDto apt)
        {
            try
            {
                var response = await _appointmentsService.CreateAppointment(apt);

                if (!response.Success)
                {
                    await RegisterLog("Appointment", $"Create appointment fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("Appointment", $"Create appointment", new { response.Success, response.Data });
                return Ok();
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Create appointment fail", new { ex.Message });
                return BadRequest(ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var apt = await _appointmentsService.DeleteAppointment(id);
                if (!apt)
                {
                    await RegisterLog("Appointment", $"Delete appointment fail");
                    return UnprocessableEntity(apt);
                }

                await RegisterLog("Appoitment", $"Delete appointment", new { apt });
                return Ok();
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointmnet", $"Delete appointment fail", new { ex.Message });
                return BadRequest(ex.Message);
            }
        }

        #region gets
        [HttpGet("GetAllServices/")]
        [Authorize]
        public async Task<IActionResult> GetAllServices(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _appointmentsService.GetAll(pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("Appointment", $"Get appointment fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("Appointment", $"Get appointment", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Get appointment fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAppointmentById/{id}")]
        public async Task<IActionResult> GetAppointmentsById(int id)
        {
            try
            {
                var response = await _appointmentsService.GetById(id);

                if (!response.Success)
                {
                    await RegisterLog("Appointment", $"Get appointment by id fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("Appointment", $"Get appointment by id", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Get appointment by id fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAppointmentByUser/{id}")]
        public async Task<IActionResult> GetAppointmentsByUser(int userId, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _appointmentsService.GetByUser(userId, pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("Appointment", $"Get appointment by user fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("Appointment", $"Get appointment user id", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Get appointment by user fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAppointmentByPet/{id}")]
        public async Task<IActionResult> GetAppointmentsByPet(int petId)
        {
            try
            {
                var response = await _appointmentsService.GetByPet(petId);

                if (!response.Success)
                {
                    await RegisterLog("Appointment", $"Get appointment by pet fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("Appointment", $"Get appointment pet id", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Get appointment by pet fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAppointmentByStatus/{id}")]
        public async Task<IActionResult> GetAppointmentsByStatus(StatusAppointments statusAppointments, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _appointmentsService.GetByStatus(statusAppointments, pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("Appointment", $"Get appointment by status fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("Appointment", $"Get appointment status id", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Get appointment by status fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpPatch("AddServicesAppointments/")]
        public async Task<IActionResult> AddServiceAppointments(int aptId, List<int> servicesId)
        {
            try
            {
                var response = await _appointmentsService.AddServiceAppointment(aptId, servicesId);

                if (!response)
                {
                    await RegisterLog("Appointment", $"Add services in appointment fail", new { response });
                    return UnprocessableEntity(response);
                }

                await RegisterLog("Appointment", $"Add services in appointment ", new { response });
                return Ok(response);
            }
            catch (Exception ex)
            {
                await RegisterLog("Appointment", $"Add services in appointment fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        #endregion

        protected AuditModel LogAudit(string module, string description, string model)
        {
            return _audit.RegisterLog(HttpContext, module, description, model);
        }

        private async Task RegisterLog(string module, string description, object objectModel = null)
        {
            try
            {
                var modelJson = string.Empty;

                //convert object in json
                if (objectModel != null) modelJson = JsonConvert.SerializeObject(objectModel);

                var log = LogAudit(module, description, modelJson);
                await _auditService.RegisterLog(log);
            }
            catch
            {
                throw;
            }
        }
    }
}
