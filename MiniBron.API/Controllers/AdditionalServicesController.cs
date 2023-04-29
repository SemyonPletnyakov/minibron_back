using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBron.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalServicesController : ControllerBase
    {
        IAdditionalServicesServices _additionalServicesServices;
        public AdditionalServicesController(IAdditionalServicesServices additionalServicesServices)
        {
            _additionalServicesServices = additionalServicesServices;
        }


        
        [HttpGet]
        public async Task<IActionResult> GetAllHotelServices()
        {
            IEnumerable<AdditionalServiceDTO> result = _additionalServicesServices.GetAllHotelServices(this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateHotelServices(AdditionalServiceCreateDTO additionalServiceCreateDTO)
        {
            int result = _additionalServicesServices.CreateHotelServices(additionalServiceCreateDTO, this.GetHotelIdFromJwtToken());
            if (result <= 0) NotFound();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> ChangeRoom(AdditionalServiceDTO additionalServiceDTO)
        {
            bool result = _additionalServicesServices.ChangeHotelServices(additionalServiceDTO, this.GetHotelIdFromJwtToken());
            if (result) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(AdditionalServiceDeleteDTO additionalServiceDeleteDTO)
        {
            bool result = _additionalServicesServices.DeleteHotelServices(additionalServiceDeleteDTO, this.GetHotelIdFromJwtToken());
            if (result) NotFound();
            return Ok(result);
        }
    }
}
