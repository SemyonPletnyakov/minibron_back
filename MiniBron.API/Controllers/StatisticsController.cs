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
    public class StatisticsController : ControllerBase
    {
        IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [Authorize]
        [HttpGet("IncomeMonth")]
        public async Task<IActionResult> GetIncomeFromMonthDTO(DateTime startDate, DateTime endDate)
        {
            IEnumerable<StatisticIncomeDTO> result = _statisticsService.GetIncomeFromMonthDTO(startDate, endDate, this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("BookingsMonth")]
        public async Task<IActionResult> GetBookingFromMonthDTO(DateTime startDate, DateTime endDate)
        {
            IEnumerable<StatisticCountBookingDTO> result = _statisticsService.GetBookingFromMonthDTO(startDate, endDate, this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("RoomsMonth")]
        public async Task<IActionResult> GetRoomFromMonthDTO(DateTime startDate, DateTime endDate)
        {
            IEnumerable<StatisticCountRoomDTO> result = _statisticsService.GetRoomFromMonthDTO(startDate, endDate, this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
    }
}
