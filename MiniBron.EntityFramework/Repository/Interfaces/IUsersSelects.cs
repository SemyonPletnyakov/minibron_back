using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface IUsersSelects
    {
        public User GetUserByHotelLoginPassword(int hotelId, string login, string password);
        public IEnumerable<User> GetAllHotelUsers(int hotelId);
        public User GetHotelUsersById(int userId, int hotelId);
        public int CreateUser(User user);
        public bool ChangeUser(User user);
        public bool DeleteUser(int userId, int hotelId);
    }
}
