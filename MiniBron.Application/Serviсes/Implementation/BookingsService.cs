using EASendMail;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class BookingsService : IBookingsService
    {
        IBookingsSelect bookingsSelect;
        IRoomsSelests roomsSelests;
        public BookingsService()
        {
            bookingsSelect = new BookingsSelect();
            roomsSelests = new RoomsSelests();
        }
        public IEnumerable<BookingDTO> GetAllBokings(int hotelId)
        {
            return bookingsSelect.GetAllBookings(hotelId)?.Select(b=>new BookingDTO() { 
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room.Title,
                PictureName = b.Room.PictureName,
                Price = b.Room.Price,
                StartDateTime = b.StartDateTime,
                EndDateTime = b.EndDateTime,
                FIO = b.FIO,
                Phone = b.Phone,
                Email = b.Email,
                ServicesForBookings = b.ServicesForBookings?.Select(s => new ServicesForBookingDTO()
                    {
                        Id = s.Id,
                        BookingId = s.BookingId,
                        AdditionalServiceId = s.AdditionalServiceId,
                        ServiceName = s.AdditionalService.Title,
                        Price = s.AdditionalService.Price
                    })
                });
        }
        public IEnumerable<BookingDTO> GetActualBookings(int hotelId)
        {
            return bookingsSelect.GetActualBookings(hotelId)?.Select(b => new BookingDTO()
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room.Title,
                PictureName = b.Room.PictureName,
                Price = b.Room.Price,
                StartDateTime = b.StartDateTime,
                EndDateTime = b.EndDateTime,
                FIO = b.FIO,
                Phone = b.Phone,
                Email = b.Email,
                ServicesForBookings = b.ServicesForBookings?.Select(s => new ServicesForBookingDTO()
                {
                    Id = s.Id,
                    BookingId = s.BookingId,
                    AdditionalServiceId = s.AdditionalServiceId,
                    ServiceName = s.AdditionalService.Title,
                    Price = s.AdditionalService.Price
                })
            });
        }
        public BookingDTO GetBookingsById(int bookingId, int hotelId)
        {
            Booking b = bookingsSelect.GetBookingsById(bookingId, hotelId);
            if (b == null) return null;
            return new BookingDTO()
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room.Title,
                    PictureName = b.Room.PictureName,
                    Price = b.Room.Price,
                    StartDateTime = b.StartDateTime,
                    EndDateTime = b.EndDateTime,
                    FIO = b.FIO,
                    Phone = b.Phone,
                    Email = b.Email,
                    ServicesForBookings = b.ServicesForBookings?.Select(s => new ServicesForBookingDTO()
                    {
                        Id = s.Id,
                        BookingId = s.BookingId,
                        AdditionalServiceId = s.AdditionalServiceId,
                        ServiceName = s.AdditionalService.Title,
                        Price = s.AdditionalService.Price
                    })
                };
        }
        public BookingDTO GetBookingsByEmailOrTelepthone(string telephone, string email, int hotelId)
        {
            Booking b = bookingsSelect.GetBookingsByEmailOrTelepthone(telephone, email, hotelId);
            if (b == null) return null;
            return new BookingDTO()
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room.Title,
                PictureName = b.Room.PictureName,
                Price = b.Room.Price,
                StartDateTime = b.StartDateTime,
                EndDateTime = b.EndDateTime,
                FIO = b.FIO,
                Phone = b.Phone,
                Email = b.Email,
                ServicesForBookings = b.ServicesForBookings?.Select(s => new ServicesForBookingDTO()
                {
                    Id = s.Id,
                    BookingId = s.BookingId,
                    AdditionalServiceId = s.AdditionalServiceId,
                    ServiceName = s.AdditionalService.Title,
                    Price = s.AdditionalService.Price
                })
            };
        }
        public int CreateBooking(BookingCreateDTO booking, int hotelId)
        {
            int result = bookingsSelect.CreateBooking(new Booking()
            {
                RoomId = booking.RoomId,
                StartDateTime = booking.StartDateTime,
                EndDateTime = booking.EndDateTime,
                FIO = booking.FIO,
                Phone = booking.Phone,
                Email = booking.Email
            }, hotelId);
            if (result > 0)
            {
                try
                {
                    // кому отправляем
                    Hotel hotel = bookingsSelect.GetEmail(hotelId);
                    Room room = roomsSelests.GetRoomById(booking.RoomId, hotelId);
                    SmtpMail oMail = new SmtpMail("TryIt");
                    oMail.From = "minibron@mail.ru";
                    oMail.To = hotel.OwnersEmail;
                    oMail.Subject = "Бронирование номера";
                    oMail.HtmlBody = "Уважаемый " + hotel.OwnersFIO + ", уведомляем вас о новом бронировании: "+ room.Title + " от " +booking.StartDateTime.ToString("d")+" до " + booking.EndDateTime.ToString("d") +".";
                    SmtpServer oServer = new SmtpServer("smtp.mail.ru");
                    oServer.User = "minibron@mail.ru";
                    oServer.Password = "dBU1M1P4DBiktpbj31iY";
                    // If you want to use direct SSL 465 port,
                    // please add this line, otherwise TLS will be used.
                    // oServer.Port = 465;
                    // set 587 TLS port;
                    oServer.Port = 587;
                    // detect SSL/TLS automatically
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                    EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                    oSmtp.SendMail(oServer, oMail);
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return result;
        }
        public bool ChangeBookings(BookingChangeDTO booking, int hotelId)
        {
            return bookingsSelect.ChangeBookings(new Booking()
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                StartDateTime = booking.StartDateTime,
                EndDateTime = booking.EndDateTime,
                FIO = booking.FIO,
                Phone = booking.Phone,
                Email = booking.Email
            }, hotelId);
        }
        public bool DeleteBookingsById(BookingDeleteDTO bookingDeleteDTO, int hotelId)
        {
            return bookingsSelect.DeleteBookingsById(bookingDeleteDTO.Id, hotelId);
        }
        public bool DeleteBookingsEmailOrTelepthone(BookingDeleteForUserDTO bookingDeleteDTO, int hotelId)
        {
            return bookingsSelect.DeleteBookingsEmailOrTelepthone(bookingDeleteDTO.Telephone, bookingDeleteDTO.Email, hotelId);
        }
        public int AddServiceInBooking(ServicesForBookingCreateDTO servicesForBooking, int hotelId)
        {
            return bookingsSelect.AddServiceInBooking(new ServicesForBooking() {BookingId=servicesForBooking.BookingId, AdditionalServiceId=servicesForBooking.AdditionalServiceId }, hotelId);
        }
        public bool DeleteServisForBookingById(ServicesForBookingDeleteDTO serviceForBooking, int hotelId)
        {
            return bookingsSelect.DeleteServisForBookingById(serviceForBooking.Id, hotelId);
        }
    }
}
