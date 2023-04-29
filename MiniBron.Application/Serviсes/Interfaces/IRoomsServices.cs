using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IRoomsServices
    {
        public IEnumerable<RoomDTO> GetFreeHolelRoomsByDataAndCapacity(DateTime startDate, DateTime endDate, int capasity, int hotelId);
        public IEnumerable<RoomDTO> GetAllHolelRooms(int hotelId);
        public int CreateRoom(RoomCreateDTO room, int hotelId);
        public bool ChangeRoom(RoomDTO room, int hotelId);
        public bool DeleteRoom(RoomDeleteDTO roomDeleteDTO, int hotelId);
    }
}
