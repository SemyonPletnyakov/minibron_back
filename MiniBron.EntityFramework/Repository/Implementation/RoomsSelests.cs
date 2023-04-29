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
    public class RoomsSelests : IRoomsSelests
    {
        public IEnumerable<Room> GetFreeHolelRoomsByDataAndCapacity(DateTime startDate, DateTime endDate, int capasity, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    /*List<Room> result = db.Bookings.Where(b => b.StartDateTime > endDate || b.EndDateTime < startDate).
                                                            Include(b => b.Room).
                                                            Select(b => b.Room).
                                      Intersect(db.Sessions.Where(b => b.EndDateTime < startDate || (b.EndDateTime == null && DateTime.Now.AddDays(15) < startDate)).
                                                            Include(b => b.Room).
                                                            Select(b => b.Room)).
                                                            Where(r => r.HotelId == hotelId && r.Capacity == capasity).
                                                            ToList();*/
                    List<Room> result = db.Rooms.Where(r => r.HotelId == hotelId && r.Capacity == capasity).
                                                 Except(db.Bookings.Where(b => b.Room.HotelId == hotelId && (b.StartDateTime > startDate && b.EndDateTime < startDate || b.StartDateTime > endDate && b.EndDateTime < endDate)).
                                                                    Include(b => b.Room).
                                                                    Select(b => b.Room)).
                                                 Except(db.Sessions.Where(b => b.Room.HotelId == hotelId && (b.EndDateTime < startDate || (b.EndDateTime == null && DateTime.Now.AddDays(15) < startDate))).
                                                                    Include(b => b.Room).
                                                                    Select(b => b.Room)).
                                                                    ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Room> GetAllHolelRooms(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    List<Room> result = db.Rooms.Where(r => r.HotelId == hotelId).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        public int CreateRoom(Room room)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    db.Rooms.Add(room);
                    db.SaveChanges();
                    return room.Id;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool ChangeRoom(Room room)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Room mainRoom = db.Rooms.FirstOrDefault(r=>r.Id == room.Id && r.HotelId == room.HotelId);
                    if(mainRoom == null) return false;
                    mainRoom.Title = room.Title;
                    mainRoom.Description = room.Description;
                    mainRoom.Capacity = room.Capacity;
                    mainRoom.Price = room.Price;
                    mainRoom.PictureName = room.PictureName;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteRoom(int roomId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    Room mainRoom = db.Rooms.FirstOrDefault(r => r.Id == roomId && r.HotelId == hotelId);
                    if (mainRoom == null) return false;
                    db.Remove(mainRoom);

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
