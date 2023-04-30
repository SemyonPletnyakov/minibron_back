using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.DTO
{
    public class PictureDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int NumberOnTheList { get; set; }
    }
}
