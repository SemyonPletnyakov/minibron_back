using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Domain
{
    public class ServicesForSession
    {
        public int Id { get; set; }
        public int SessionsId { get; set; }
        public Session Sessions { get; set; }
        public int AdditionalServiceId { get; set; }
        public AdditionalService AdditionalService { get; set; }
        public decimal ActualPriceForService { get; set; }
    }
}
