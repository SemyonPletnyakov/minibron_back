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
    public class RoomsController : ControllerBase
    {
        IRoomsServices _roomsServices;
        public RoomsController(IRoomsServices roomsServices)
        {
            _roomsServices = roomsServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetFreeHolelRoomsByDataAndCapacity(DateTime startDate, DateTime endDate, int capasity, int hotelId)
        {
            IEnumerable<RoomDTO> result = _roomsServices.GetFreeHolelRoomsByDataAndCapacity(startDate, endDate, capasity, hotelId);
            if (result == null) NotFound();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllHolelRooms()
        {
            IEnumerable<RoomDTO> result = _roomsServices.GetAllHolelRooms(this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRoom(RoomCreateDTO roomCreateDTO)
        {
            int result = _roomsServices.CreateRoom(roomCreateDTO, this.GetHotelIdFromJwtToken());
            if (result <= 0) NotFound();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> ChangeRoom(RoomDTO room)
        {
            bool result = _roomsServices.ChangeRoom(room, this.GetHotelIdFromJwtToken());
            if (result) NotFound();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(RoomDeleteDTO roomDeleteDTO)
        {
            bool result = _roomsServices.DeleteRoom(roomDeleteDTO, this.GetHotelIdFromJwtToken());
            if (result) NotFound();
            return Ok(result);
        }
    }
}
