using Microsoft.AspNetCore.Http;
using MiniBron.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Interfaces
{
    public interface IPicturesServices
    {
        public IEnumerable<PictureDTO> GetPicturesByRoomId(int roomId);
        public bool UpdatePictures(IEnumerable<PictureDTO> pictures, int hotelId, string webRootPath);
        public bool AddPicture(IFormFile image, int roomId, int hotelId, string webRootPath);
        public bool AddPictures(IFormFileCollection images, int roomId, int hotelId, string webRootPath);
    }
}
