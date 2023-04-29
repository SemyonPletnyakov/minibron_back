using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IAdditionalServicesServices
    {
        public IEnumerable<AdditionalServiceDTO> GetAllHotelServices(int hotelId);
        public int CreateHotelServices(AdditionalServiceCreateDTO additionalService, int hotelId);
        public bool ChangeHotelServices(AdditionalServiceDTO additionalService, int hotelId);
        public bool DeleteHotelServices(AdditionalServiceDeleteDTO additionalServiceDeleteDTO, int hotelId);

    }
}
