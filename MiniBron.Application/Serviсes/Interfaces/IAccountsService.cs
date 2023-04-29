using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IAccountsService
    {
        public AccountGetDTO LoginAccount(AccountLoginDTO accountLoginDTO);
        public AccounDTO GetAccountInfo(int userId, int hotelId);
        public bool ChangeAccount(AccounDTO accountChangeDTO, int userId, int hotelId);
    }
}
