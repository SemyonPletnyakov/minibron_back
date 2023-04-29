using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBron.Application.DTO;
using MiniBron.Application.Serviсes.Interfaces;
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
        IAccountService _loginService;
        public AccountController(IAccountService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            AccountGetDTO result = _loginService.LoginAccount(accountLoginDTO);
            if (result != null) return Ok(result);
            else return BadRequest();
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ChangeMenuItem()
        {
            return Ok(2);
        }
    }
}
