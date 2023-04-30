using Microsoft.EntityFrameworkCore;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Implementation
{
    public class PicturesSelects : IPicturesSelects
    {
        public IEnumerable<Picture> GetPicturesByRoomId(int roomId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<Picture> result = db.Pictures.Where(r => r.RoomId == roomId).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool UpdatePictures(IEnumerable<Picture> pictures, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    int roomId = pictures.FirstOrDefault()?.RoomId ?? 0;
                    Room room = db.Rooms.FirstOrDefault(r => r.Id == roomId && r.HotelId == hotelId);
                    if (room==null) return false;
                    List<Picture> mainPictures = db.Pictures.Where(r => r.RoomId == roomId).ToList();
                    
                    foreach(Picture p in pictures)
                    {
                        Picture mainPicture = mainPictures.FirstOrDefault(m => m.Id == p.Id);
                        if (mainPicture != null)
                        {
                            mainPicture.Name = p.Name;
                            mainPicture.NumberOnTheList = p.NumberOnTheList;
                        }
                    }
                    db.SaveChanges();

                    foreach (Picture p in pictures)
                    {
                        if (p.Id == 0) db.Pictures.Add(p);
                    }
                    db.SaveChanges();
                    
                    foreach (Picture mainPicture in mainPictures)
                    {
                        Picture p = pictures.FirstOrDefault(m => m.Id == mainPicture.Id);
                        if (p == null) db.Remove(mainPicture);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool CheckForAnImage(string pictureName, int roomId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<Picture> result = db.Pictures.Where(r => r.RoomId == roomId && r.Room.HotelId == hotelId && r.Name == pictureName).Include(p=>p.Room).ToList();

                    return (result!=null);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
