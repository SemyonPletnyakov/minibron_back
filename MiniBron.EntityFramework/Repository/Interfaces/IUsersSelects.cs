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
        public User GetUserByHotelLoginPassword(int id, string login, string password);
    }
}
