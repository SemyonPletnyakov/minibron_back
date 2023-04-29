using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Domain
{
    public class ServicesForBooking
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int AdditionalServiceId { get; set; }
        public AdditionalService AdditionalService { get; set; }
    }
}
