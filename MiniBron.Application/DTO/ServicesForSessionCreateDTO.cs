using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.DTO
{
    public class ServicesForSessionCreateDTO
    {
        public int SessionsId { get; set; }
        public int AdditionalServiceId { get; set; }
        public decimal ActualPrice { get; set; }
    }
}

