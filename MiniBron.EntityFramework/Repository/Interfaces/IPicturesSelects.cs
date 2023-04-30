using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository.Interfaces
{
    public interface IPicturesSelects
    {
        public IEnumerable<Picture> GetPicturesByRoomId(int roomId);
        public bool UpdatePictures(IEnumerable<Picture> pictures, int hotelId);
        public bool CheckForAnImage(string pictureName,int roomId, int hotelId);
    }
}
