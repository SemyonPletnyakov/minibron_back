using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IAccountService
    {
        public AccountGetDTO LoginAccount(AccountLoginDTO accountLoginDTO);
    }
}
