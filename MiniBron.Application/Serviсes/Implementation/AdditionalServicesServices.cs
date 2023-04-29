using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;
using MiniBron.Domain;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class AdditionalServicesServices : IAdditionalServicesServices
    {
        IAdditionalServicesSelects additionalServicesSelects;
        public AdditionalServicesServices()
        {
            additionalServicesSelects = new AdditionalServicesSelects();
        }
        public IEnumerable<AdditionalServiceDTO> GetAllHotelServices(int hotelId)
        {
            return additionalServicesSelects.GetAllHotelServices(hotelId)?.Select(r => new AdditionalServiceDTO()
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                Price = r.Price,
                PictureName = r.PictureName
            });
        }
        public int CreateHotelServices(AdditionalServiceCreateDTO additionalService, int hotelId)
        {
            return additionalServicesSelects.CreateHotelServices(new AdditionalService()
            {
                HotelId = hotelId,
                Title = additionalService.Title,
                Description = additionalService.Description,
                Price = additionalService.Price,
                PictureName = additionalService.PictureName
            });
        }
        public bool ChangeHotelServices(AdditionalServiceDTO additionalService, int hotelId)
        {
            return additionalServicesSelects.ChangeHotelServices(new AdditionalService()
            {
                Id = additionalService.Id,
                HotelId = hotelId,
                Title = additionalService.Title,
                Description = additionalService.Description,
                Price = additionalService.Price,
                PictureName = additionalService.PictureName
            });
        }
        public bool DeleteHotelServices(AdditionalServiceDeleteDTO additionalServiceDeleteDTO, int hotelId)
        {
            return additionalServicesSelects.DeleteHotelServices(additionalServiceDeleteDTO.Id, hotelId);
        }

    }
}
