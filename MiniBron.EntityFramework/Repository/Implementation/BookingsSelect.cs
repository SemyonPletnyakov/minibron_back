using Microsoft.EntityFrameworkCore;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Implementation
{
    public class BookingsSelect : IBookingsSelect
    {
        public IEnumerable<Booking> GetAllBookings(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<Booking> result = db.Bookings.Where(r => r.Room.HotelId == hotelId).Include(r=>r.Room).Include(r => r.ServicesForBookings).ThenInclude(s=>s.AdditionalService).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Booking> GetActualBookings(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<Booking> result = db.Bookings.Where(r => r.Room.HotelId == hotelId && r.StartDateTime > DateTime.Now).Include(r => r.Room).Include(r => r.ServicesForBookings).ThenInclude(s => s.AdditionalService).ToList();
                    System.Diagnostics.Debug.WriteLine(result.Count);
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public Booking GetBookingsById(int bookingId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Booking result = db.Bookings.Where(r => r.Id==bookingId && r.Room.HotelId == hotelId).Include(r => r.Room).Include(r => r.ServicesForBookings).ThenInclude(s => s.AdditionalService).FirstOrDefault();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public Booking GetBookingsByEmailOrTelepthone(string telephone, string email, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (telephone != null && telephone != "")
                    {
                        Booking result = db.Bookings.Where(r => r.Phone == telephone && r.Room.HotelId == hotelId).Include(r => r.Room).Include(r => r.ServicesForBookings).ThenInclude(s => s.AdditionalService).FirstOrDefault();
                        return result;
                    }
                    if (email != null && email != "")
                    {
                        Booking result = db.Bookings.Where(r => r.Email == email && r.Room.HotelId == hotelId).Include(r => r.Room).Include(r => r.ServicesForBookings).ThenInclude(s => s.AdditionalService).FirstOrDefault();
                        return result;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public int CreateBooking(Booking booking, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Room room = db.Rooms.FirstOrDefault(b => b.Id == booking.RoomId);
                    if (room == null || room.HotelId != hotelId) return -3;
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return booking.Id;
                }
            }
            catch
            {
                return -1;
            }
        }

        public bool ChangeBookings(Booking booking, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Booking mainBooking = db.Bookings.Include(b => b.Room).FirstOrDefault(b=>b.Id == booking.Id && b.Room.Hotel.Id == hotelId);
                    if (mainBooking == null) return false;
                    mainBooking.RoomId = booking.RoomId;
                    mainBooking.StartDateTime = booking.StartDateTime;
                    mainBooking.EndDateTime = booking.EndDateTime;
                    mainBooking.FIO = booking.FIO;
                    mainBooking.Phone = booking.Phone;
                    mainBooking.Email = booking.Email;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteBookingsById(int bookingId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Booking mainBooking = db.Bookings.FirstOrDefault(b => b.Id == bookingId && b.Room.Hotel.Id == hotelId);
                    if (mainBooking == null) return false;
                    db.Remove(mainBooking);

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteBookingsEmailOrTelepthone(string telephone, string email, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    

                    if (telephone != null && telephone != "")
                    {
                        Booking mainBooking = db.Bookings.FirstOrDefault(b => b.Phone == telephone && b.Room.Hotel.Id == hotelId);
                        if (mainBooking == null) return false;
                        db.Remove(mainBooking);

                        db.SaveChanges();
                        return true;
                    }
                    if (email != null && email != "")
                    {
                        Booking mainBooking = db.Bookings.FirstOrDefault(b => b.Email == email && b.Room.Hotel.Id == hotelId);
                        if (mainBooking == null) return false;
                        db.Remove(mainBooking);

                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public int AddServiceInBooking(ServicesForBooking servicesForBooking, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Booking booking = db.Bookings.Where(b => b.Id == servicesForBooking.BookingId).Include(b=>b.Room).FirstOrDefault();
                    if (booking == null || booking.Room == null || booking.Room.HotelId != hotelId) return -3;

                    db.ServicesForBookings.Add(servicesForBooking);
                    db.SaveChanges();
                    return servicesForBooking.Id;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool DeleteServisForBookingById(int serviceForBookingId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    ServicesForBooking service = db.ServicesForBookings.Where(b => b.Id == serviceForBookingId && b.Booking.Room.HotelId == hotelId).Include(s=>s.Booking).ThenInclude(s=>s.Room).FirstOrDefault();
                    if (service == null) return false;
                    db.Remove(service);

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public Hotel GetEmail(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Hotels.FirstOrDefault(x => x.Id == hotelId);
                }
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Booking> GetBookingsByDate(DateTime startDate, DateTime endDate, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    return db.Bookings.Where(s => s.EndDateTime >= startDate && 
                                                 (s.EndDateTime <= endDate) && 
                                                 s.Room.HotelId == hotelId).
                                                 Include(s => s.Room).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
