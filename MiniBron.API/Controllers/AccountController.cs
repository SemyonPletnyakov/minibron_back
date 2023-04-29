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
    public class AccountController : ControllerBase
    {
        IAccountsService _accountsService;
        public AccountController(IAccountsService loginService)
        {
            _accountsService = loginService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            AccountGetDTO result = _accountsService.LoginAccount(accountLoginDTO);
            if (result != null) return Ok(result);
            else return BadRequest();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangeAccount()
        {
            AccounDTO result = _accountsService.GetAccountInfo( this.GetUserIdFromJwtToken(), this.GetHotelIdFromJwtToken());
            if (result != null) return Ok(result);
            else return NotFound();
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeAccount(AccounDTO accountChangeDTO)
        {
            bool result = _accountsService.ChangeAccount(accountChangeDTO, this.GetUserIdFromJwtToken(), this.GetHotelIdFromJwtToken());
            if (result) return Ok(true);
            else return NotFound();
        }
    }
}
