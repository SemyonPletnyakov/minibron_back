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
    public class BookingsController : ControllerBase
    {
        IBookingsService _bookingsService;
        public BookingsController(IBookingsService bookingsService)
        {
            _bookingsService = bookingsService;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBokings()
        {
            IEnumerable<BookingDTO> result = _bookingsService.GetAllBokings(this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetAllHolelRooms(int bookingId)
        {
            BookingDTO result = _bookingsService.GetBookingsById(bookingId,this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetBookingsByEmailOrTelepthone(string telephone, string email, int hotelId)
        {
            BookingDTO result = _bookingsService.GetBookingsByEmailOrTelepthone(telephone, email, hotelId);
            if (result == null) NotFound();
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingCreateDTO booking, int hotelId)
        {
            int result = _bookingsService.CreateBooking(booking, hotelId);
            if (result <= 0) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> ChangeBookings(BookingChangeDTO booking)
        {
            bool result = _bookingsService.ChangeBookings(booking, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("BookingById")]
        public async Task<IActionResult> DeleteBookingsById(BookingDeleteDTO bookingDeleteDTO)
        {
            bool result = _bookingsService.DeleteBookingsById(bookingDeleteDTO, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBookingsEmailOrTelepthone(BookingDeleteForUserDTO bookingDeleteDTO, int hotelId)
        {
            bool result = _bookingsService.DeleteBookingsEmailOrTelepthone(bookingDeleteDTO, hotelId);
            if (!result) NotFound();
            return Ok(result);
        }
        [HttpPost("Service")]
        public async Task<IActionResult> AddServiceInBooking(ServicesForBookingCreateDTO servicesForBooking, int hotelId)
        {
            int result = _bookingsService.AddServiceInBooking(servicesForBooking, hotelId);
            if (result <= 0) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("Service")]
        public async Task<IActionResult> DeleteServisForBookingById(ServicesForBookingDeleteDTO serviceForBooking)
        {
            bool result = _bookingsService.DeleteServisForBookingById(serviceForBooking, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(result);
        }
    }
}
