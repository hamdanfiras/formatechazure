using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMovies.Info;

namespace SuperMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly JWT jwt;

        public AccountController(ClientInfo clientInfo, JWT jwt) : base(clientInfo)
        {
            this.jwt = jwt;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public ActionResult Login(LoginInfo loginInfo)
        {
            if (loginInfo == null)
            {
                return BadRequest();
            }
            if (loginInfo.Email == "bill@microsoft.com" && loginInfo.Password == "jess")
            {
                var token = jwt.CreateToken(loginInfo.Email);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}