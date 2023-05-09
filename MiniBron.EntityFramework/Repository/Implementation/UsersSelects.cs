using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Implementation
{
    public class UsersSelects: IUsersSelects
    {
        public User GetUserByHotelLoginPassword(int hotelId, string login, string password)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Users.FirstOrDefault( u => u.HotelId == hotelId && u.Login == login && u.Password == password);
                }
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<User> GetAllHotelUsers(int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Users.Where(u => u.HotelId == hotelId).ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        public User GetHotelUsersById(int userId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Users.FirstOrDefault(u => u.Id == userId && u.HotelId == hotelId);
                }
            }
            catch
            {
                return null;
            }
        }
        public int CreateUser(User user)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (db.Users.FirstOrDefault(u => u.Login == user.Login) != null)
                        return -2;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return user.Id;
                }
            }
            catch
            {
                return -1;
            }
        }
        public bool ChangeUser(User user, bool changeRole)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User mainUser = db.Users.FirstOrDefault(u => u.Id == user.Id && u.Login == user.Login && u.HotelId == user.HotelId);
                    if (mainUser == null) return false;

                    mainUser.FIO = user.FIO;
                    if (changeRole) mainUser.Role = user.Role;
                    mainUser.Login = user.Login;
                    mainUser.Password = user.Password;

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteUser(int userId, int hotelId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User mainUser = db.Users.FirstOrDefault(u => u.Id == userId && u.HotelId == hotelId);
                    if (mainUser == null) return false;

                    db.Remove(mainUser);

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
