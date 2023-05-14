using MiniBron.Application.DTO;
using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IStatisticsService
    {
        public IEnumerable<StatisticIncomeDTO> GetIncomeFromMonthDTO(DateTime startDate, DateTime endDate, int hotelId);
        public IEnumerable<StatisticCountBookingDTO> GetBookingFromMonthDTO(DateTime startDate, DateTime endDate, int hotelId);
        public IEnumerable<StatisticCountRoomDTO> GetRoomFromMonthDTO(DateTime startDate, DateTime endDate, int hotelId);
    }
}
