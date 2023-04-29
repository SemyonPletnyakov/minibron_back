using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface IRoomsSelests
    {
        public IEnumerable<Room> GetFreeHolelRoomsByDataAndCapacity(DateTime startDate, DateTime endDate, int capasity, int hotelId);
        public IEnumerable<Room> GetAllHolelRooms(int hotelId);
        public int CreateRoom(Room room);
        public bool ChangeRoom(Room room);
        public bool DeleteRoom(Room room);
    }
}
