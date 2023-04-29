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
        public User GetUserByHotelLoginPassword(int id, string login, string password)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Users.FirstOrDefault( u => u.Id == id && u.Login == login && u.Password == password);
                }
            }
            catch
            {
                return null;
            }
        }
    }

}
