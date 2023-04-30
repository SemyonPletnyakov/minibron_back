using Microsoft.AspNetCore.Http;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Domain;
using MiniBron.EntityFramework.Repository.Implementation;
using MiniBron.EntityFramework.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.Application.Serviсes.Implementation
{
    public class PicturesServices : IPicturesServices
    {
        IPicturesSelects picturesSelects;
        IAdditionalServicesSelects additionalServicesSelects;
        public PicturesServices()
        {
            picturesSelects = new PicturesSelects();
            additionalServicesSelects = new AdditionalServicesSelects();
        }
        public IEnumerable<PictureDTO> GetPicturesByRoomId(int roomId)
        {
            return picturesSelects.GetPicturesByRoomId(roomId)?.Select(p => new PictureDTO()
            {
                Id = p.Id,
                RoomId = p.RoomId,
                Name = p.Name,
                NumberOnTheList = p.NumberOnTheList,
            });
        }
        public bool UpdatePictures(IEnumerable<PictureDTO> pictures, int hotelId, string webRootPath)
        {
            bool result = picturesSelects.UpdatePictures(pictures.Select(p => new Picture()
            {
                Id = p.Id,
                RoomId = p.RoomId,
                Name = p.Name,
                NumberOnTheList = p.NumberOnTheList,
            }).ToList(), hotelId);
            try
            {
                int roomId = pictures.FirstOrDefault().RoomId;
                if (roomId != 0)
                {
                    var pictureList = picturesSelects.GetPicturesByRoomId(roomId);

                    string filePath = webRootPath + "\\" + hotelId.ToString() + "\\" + roomId.ToString();
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    foreach (FileInfo file in di.GetFiles())
                    {
                        var checkValue = pictureList.Where(p => p.Name == file.Name).FirstOrDefault();
                        if (checkValue == null) file.Delete();
                    }
                }
            }
            catch { }

            return result;
        }
        public bool AddPicture(IFormFile image, int roomId, int hotelId, string webRootPath)
        {
            try
            {
                if(roomId!=0)//картинки сервисов будут находится в папке 0
                    if (!picturesSelects.CheckForAnImage(image.Name, roomId, hotelId)) 
                        return false;
                string filePath = webRootPath + "\\" + hotelId.ToString() + "\\" + roomId.ToString();
                DirectoryInfo di = Directory.CreateDirectory(filePath);
                using (FileStream fileStream = System.IO.File.Create(filePath + "\\" + image.FileName))
                {
                    image.CopyTo(fileStream);
                    fileStream.Flush();
                }
                //удаление старых картинок сервисов
                if (roomId == 0)
                {
                    try
                    {
                        var serviceList = additionalServicesSelects.GetAllHotelServices(hotelId);

                        di = new DirectoryInfo(filePath);
                        foreach (FileInfo file in di.GetFiles())
                        {
                            var checkValue = serviceList.Where(p => p.PictureName == file.Name).FirstOrDefault();
                            if (checkValue == null) file.Delete();
                        }
                    }
                    catch { }
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public bool AddPictures(IFormFileCollection images, int roomId, int hotelId, string webRootPath)
        {
            foreach(IFormFile image in images)
            {
                if (!AddPicture(image, roomId, hotelId, webRootPath)) return false;
            }
            return true;
        }
    }
}
