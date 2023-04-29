using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : ControllerBase
    {
        IUsersServices _usersService;
        public UsersController(IUsersServices usersServices)
        {
            _usersService = usersServices;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<UserDTO> result = _usersService.GetAllHotelUsers(this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int userId)
        {
            UserDTO result = _usersService.GetHotelUser(userId, this.GetHotelIdFromJwtToken());
            if (result == null) NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO user)
        {
            int result = _usersService.CreateUser(user, this.GetHotelIdFromJwtToken());
            if (result <= 0) NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> ChangeUser(UserDTO user)
        {
            bool result = _usersService.ChangeUser(user, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(true);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UserDeleteDTO user)
        {
            bool result = _usersService.DeleteUser(user, this.GetHotelIdFromJwtToken());
            if (!result) NotFound();
            return Ok(true);
        }
    }
}
