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
    public class RoomsServices : IRoomsServices
    {
        IRoomsSelests roomsSelests;
        public RoomsServices()
        {
            roomsSelests = new RoomsSelests();
        }
        public IEnumerable<RoomDTO> GetFreeHolelRoomsByDataAndCapacity(DateTime startDate, DateTime endDate, int capasity, int hotelId)
        {
            return roomsSelests.GetFreeHolelRoomsByDataAndCapacity(startDate, endDate, capasity, hotelId).
                Select(r => new RoomDTO() {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Capacity = r.Capacity,
                    Price = r.Price,
                    PictureName = r.PictureName
                });
        }
        public IEnumerable<RoomDTO> GetAllHolelRooms(int hotelId)
        {
            return roomsSelests.GetAllHolelRooms(hotelId).
                Select(r => new RoomDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Capacity = r.Capacity,
                    Price = r.Price,
                    PictureName = r.PictureName
                });
        }
        public int CreateRoom(RoomCreateDTO room, int hotelId)
        {
            return roomsSelests.CreateRoom(new Room()
            {
                HotelId = hotelId,
                Title = room.Title,
                Description = room.Description,
                Capacity = room.Capacity,
                Price = room.Price,
                PictureName = room.PictureName
            });
        }
        public bool ChangeRoom(RoomDTO room, int hotelId)
        {
            return roomsSelests.ChangeRoom(new Room()
            {
                Id = room.Id,
                HotelId = hotelId,
                Title = room.Title,
                Description = room.Description,
                Capacity = room.Capacity,
                Price = room.Price,
                PictureName = room.PictureName
            });
        }
        public bool DeleteRoom(RoomDeleteDTO roomDeleteDTO, int hotelId)
        {
            return roomsSelests.DeleteRoom(roomDeleteDTO.Id, hotelId);
        }
    }
}
