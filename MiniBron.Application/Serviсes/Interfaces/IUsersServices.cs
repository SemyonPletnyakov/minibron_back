using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IUsersServices
    {
        public IEnumerable<UserDTO> GetAllHotelUsers(int hotelId);
        public UserDTO GetHotelUser(int userId, int hotelId);
        public int CreateUser(UserCreateDTO user, int hotelId);
        public bool ChangeUser(UserDTO user, int hotelId);
        public bool DeleteUser(UserDeleteDTO user, int hotelId);
    }
}
