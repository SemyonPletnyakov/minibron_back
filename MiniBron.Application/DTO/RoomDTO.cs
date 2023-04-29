﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Capacity { get; set; }
        public decimal Price { get; set; }
        public string PictureName { get; set; }
    }
}
