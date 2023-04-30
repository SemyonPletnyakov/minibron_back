using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
using MiniBron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBron.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IPicturesServices _picturesServices;
        public PicturesController(IPicturesServices picturesServices, IWebHostEnvironment webHostEnvironment)
        {
            _picturesServices = picturesServices;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("GetData")]
        public async Task<IActionResult> GetPicturesByRoomId(int roomId)
        {
            IEnumerable<PictureDTO> result = _picturesServices.GetPicturesByRoomId(roomId);
            if (result == null) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("SetData")]
        public async Task<IActionResult> UpdatePictures(IEnumerable<PictureDTO> pictures)
        {
            bool result = _picturesServices.UpdatePictures(pictures, this.GetHotelIdFromJwtToken(), _webHostEnvironment.WebRootPath);
            if (!result) NotFound();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPicturesByRoomId(int hotelId, int roomId, string name)
        {
            try
            {
                Response.StatusCode = 200;
                return File(hotelId.ToString() + "\\" + roomId.ToString() + "\\" + name, "image/*");
            }
            catch
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile image, int roomId)
        {
            bool result = _picturesServices.AddPicture(image, roomId, this.GetHotelIdFromJwtToken(), _webHostEnvironment.WebRootPath);
            if (!result) NotFound();
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("SomeImages")]
        public async Task<IActionResult> AddPictures(IFormFileCollection images, int roomId)
        {
            bool result = _picturesServices.AddPictures(images, roomId, this.GetHotelIdFromJwtToken(), _webHostEnvironment.WebRootPath);
            if (!result) NotFound();
            return Ok(result);
        }
    }
}
