using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MiniBron.Common;

namespace MiniBron.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private ISessionsServices _sessionsServices;
        public SessionsController(ISessionsServices sessionsServices)
        {
            _sessionsServices = sessionsServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSessions()
        {
            IEnumerable<SessionDTO> result = _sessionsServices.GetAllSessions(this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }

        [HttpGet("GetActual")]
        public async Task<IActionResult> GetActualSessions()
        {
            IEnumerable<SessionDTO> result = _sessionsServices.GetActualSessions(this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSessionsById(int sessionId)
        {
            SessionDTO result = _sessionsServices.GetSessionsById(sessionId, this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSession(SessionCreateDTO session)
        {
            int result = _sessionsServices.CreateSession(session, this.GetHotelIdFromJwtToken());
            if (result <= 0) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> ChangeSession(SessionChangeDTO session)
        {
            bool result = _sessionsServices.ChangeSession(session, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSessionById(SessionDeleteDTO sessionDeleteDTO)
        {
            bool result = _sessionsServices.DeleteSessionById(sessionDeleteDTO, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("Service")]
        public async Task<IActionResult> AddServiceInSession(ServicesForSessionCreateDTO servicesForSession)
        {
            int result = _sessionsServices.AddServiceInSession(servicesForSession, this.GetHotelIdFromJwtToken());
            if (result <= 0) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Service")]
        public async Task<IActionResult> DeleteServisForSessionById(ServicesForSessionDeleteDTO servicesForSessionDeleteDTO)
        {
            bool result = _sessionsServices.DeleteServisForSessionById(servicesForSessionDeleteDTO, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(result);
        }
    }
}
