using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Domain
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnersTelephone { get; set; }
        public string OwnersEmail { get; set; }
        public string OwnersFIO { get; set; }
        public List<User> Users { get; set; }
        public List<Room> Rooms { get; set; }
        public List<AdditionalService> AdditionalServices { get; set; }
    }
}
