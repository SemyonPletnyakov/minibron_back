using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Domain
{
    public class AdditionalService
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureName { get; set; }
        public List<ServicesForBooking> ServicesForBookings { get; set; }
        public List<ServicesForSession> ServicesForSessions { get; set; }
    }
}
