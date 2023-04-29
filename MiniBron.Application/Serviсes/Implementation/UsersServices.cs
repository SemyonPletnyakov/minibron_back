using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class UsersServices : IUsersServices
    {
        IUsersSelects usersSelects;
        public UsersServices()
        {
            usersSelects = new UsersSelects();
        }
        public IEnumerable<UserDTO> GetAllHotelUsers(int hotelId)
        {
            return usersSelects.GetAllHotelUsers(hotelId).Select(u => new UserDTO() 
                        {
                            Id = u.Id,
                            FIO = u.FIO,
                            Role = u.Role,
                            Login = u.Login,
                            Password = u.Password
                        });
        }
        public UserDTO GetHotelUser(int userId, int hotelId)
        {
            User u = usersSelects.GetHotelUsersById(userId, hotelId);
            return new UserDTO()
                    {
                        Id = u.Id,
                        FIO = u.FIO,
                        Role = u.Role,
                        Login = u.Login,
                        Password = u.Password
                    };
        }
        public int CreateUser(UserCreateDTO user, int hotelId)
        {
            return usersSelects.CreateUser(new User()
                    {
                        HotelId = hotelId,
                        FIO = user.FIO,
                        Role = user.Role,
                        Login = user.Login,
                        Password = user.Password
                    });
        }
        public bool ChangeUser(UserDTO user, int hotelId)
        {
            return usersSelects.ChangeUser(new User()
                    {
                        Id = user.Id,
                        HotelId = hotelId,
                        FIO = user.FIO,
                        Role = user.Role,
                        Login = user.Login,
                        Password = user.Password
                    });
        }
        public bool DeleteUser(UserDeleteDTO user, int hotelId)
        {
            return usersSelects.DeleteUser(user.Id, hotelId);
        }
    }
}
