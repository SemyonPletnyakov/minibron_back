using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IBookingsService
    {
        public IEnumerable<BookingDTO> GetAllBokings(int hotelId);

        public BookingDTO GetBookingsById(int bookingId, int hotelId);
        public BookingDTO GetBookingsByEmailOrTelepthone(string telephone, string email, int hotelId);
        public int CreateBooking(BookingCreateDTO booking, int hotelId);
        public bool ChangeBookings(BookingChangeDTO booking, int hotelId);
        public bool DeleteBookingsById(BookingDeleteDTO bookingDeleteDTO, int hotelId);
        public bool DeleteBookingsEmailOrTelepthone(BookingDeleteForUserDTO bookingDeleteDTO, int hotelId);
        public int AddServiceInBooking(ServicesForBookingCreateDTO servicesForBooking, int hotelId);
        public bool DeleteServisForBookingById(ServicesForBookingDeleteDTO serviceForBooking, int hotelId);
    }
}
