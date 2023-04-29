using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.DTO
{
    public class AccountLoginDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int HostelId { get; set; }
    }
}
