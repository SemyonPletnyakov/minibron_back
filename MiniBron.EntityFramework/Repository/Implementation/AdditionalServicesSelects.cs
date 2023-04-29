using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Implementation
{
    public class AdditionalServicesSelects : IAdditionalServicesSelects
    {
        public IEnumerable<AdditionalService> GetAllHotelServices(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<AdditionalService> result = db.AdditionalServices.Where(r => r.HotelId == hotelId).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public int CreateHotelServices(AdditionalService additionalService)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    db.AdditionalServices.Add(additionalService);
                    db.SaveChanges();
                    return additionalService.Id;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool ChangeHotelServices(AdditionalService additionalService)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    AdditionalService mainService = db.AdditionalServices.FirstOrDefault(r => r.Id == additionalService.Id && r.HotelId == additionalService.HotelId);
                    if (mainService == null) return false;
                    mainService.Title = additionalService.Title;
                    mainService.Description = additionalService.Description;
                    mainService.Price = additionalService.Price;
                    mainService.PictureName = additionalService.PictureName;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteHotelServices(int serviceId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    AdditionalService mainService = db.AdditionalServices.FirstOrDefault(r => r.Id == serviceId && r.HotelId == hotelId);
                    if (mainService == null) return false;
                    db.Remove(mainService);

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
