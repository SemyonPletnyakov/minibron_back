﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.DTO
{
    public class SessionChangeDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal ActualPriceForRoom { get; set; }
    }
}
