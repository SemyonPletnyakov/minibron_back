using MiniBron.Domain;
using System;
using System.Collections.Generic;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface IBookingsSelect
    {
        public IEnumerable<Booking> GetAllBookings(int hotelId);
        public IEnumerable<Booking> GetActualBookings(int hotelId);
        public Booking GetBookingsById(int bookingId, int hotelId);
        public Booking GetBookingsByEmailOrTelepthone(string telephone, string email, int hotelId);
        public int CreateBooking(Booking booking, int hotelId);
        public bool ChangeBookings(Booking booking, int hotelId);
        public bool DeleteBookingsById(int bookingId, int hotelId);
        public bool DeleteBookingsEmailOrTelepthone(string telephone, string email, int hotelId);
        public int AddServiceInBooking(ServicesForBooking servicesForBooking, int hotelId);
        public bool DeleteServisForBookingById(int serviceForBookingId, int hotelId);
        public Hotel GetEmail(int hotelId);
        public IEnumerable<Booking> GetBookingsByDate(DateTime startDate, DateTime endDate, int hotelId);
    }
}
