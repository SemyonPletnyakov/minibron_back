using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Capacity { get; set; }
        public decimal Price { get; set; }
        public string PictureName { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
