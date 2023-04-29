using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface IAdditionalServices
    {
        public IEnumerable<AdditionalService> GetAllHotelServices(int hotelId);
        public int CreateHotelServices(AdditionalService additionalService);
        public bool ChangeHotelServices(AdditionalService additionalService);
        public bool DeleteHotelServices(AdditionalService additionalService);

    }
}
