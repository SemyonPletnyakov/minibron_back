using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface IBookingsSelect
    {
        public IEnumerable<Booking> GetAllBokings(int hotelId);
        public Booking GetBokingsById(int bookingId, int hotelId);
    }
}
