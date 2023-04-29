using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.DTO
{
    public class ServicesForBookingDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int AdditionalServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }
}
