using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class StatisticsService : IStatisticsService
    {
        ISessionsSelects sessionsSelects;
        IBookingsSelect bookingsSelect;
        IRoomsSelests roomsSelests;
        public StatisticsService()
        {
            sessionsSelects = new SessionsSelects();
            bookingsSelect = new BookingsSelect();
            roomsSelests = new RoomsSelests();
        }
        public IEnumerable<StatisticIncomeDTO> GetIncomeFromMonthDTO(DateTime startDate, DateTime endDate, int hotelId)
        {
            return sessionsSelects.GetSessionsByDate(startDate, endDate, hotelId)
                                  .OrderBy(s=>s.EndDateTime)
                                  .Select(s => new { MonthAndYear = (s.EndDateTime ?? DateTime.Now).ToString("Y"), TotalPrice = s.TotalPrice })
                                  .GroupBy(s => s.MonthAndYear)
                                  .Select(s => new StatisticIncomeDTO() { MonthAndYear = s.Key, Income = s.Sum(t => t.TotalPrice) });
        }
        public IEnumerable<StatisticCountBookingDTO> GetBookingFromMonthDTO(DateTime startDate, DateTime endDate, int hotelId)
        {
            return bookingsSelect.GetBookingsByDate(startDate, endDate, hotelId)
                                  .OrderBy(s => s.EndDateTime)
                                  .Select(s => new { MonthAndYear = s.EndDateTime.ToString("Y"), Booking = s })
                                  .GroupBy(s => s.MonthAndYear)
                                  .Select(s => new StatisticCountBookingDTO() { MonthAndYear = s.Key, Count = s.Count() });
        }
        public IEnumerable<StatisticCountRoomDTO> GetRoomFromMonthDTO(DateTime startDate, DateTime endDate, int hotelId)
        {
            var bookingsCount = bookingsSelect.GetBookingsByDate(startDate, endDate, hotelId)
                                  .Select(s => new { RoomId = s.RoomId, Booking = s })
                                  .GroupBy(s => s.RoomId)
                                  .Select(s => new { RoomId = s.Key, Count = s.Count() })
                                  .ToList();

            var allRooms = roomsSelests.GetAllHolelRooms(hotelId);

            var result = allRooms.GroupJoin(bookingsCount, r => r.Id, b => b.RoomId, (r, masB) => new { room = r, masBookingCount = masB })
                                 .Select(r => new StatisticCountRoomDTO() { RoomName = r.room.Title, Count = r.masBookingCount.FirstOrDefault()?.Count ?? 0 })
                                 .OrderBy(r=>r.RoomName)
                                 .ToList();
            return result;
        }
    }
}
