using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SuperMovies.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        public BaseController(ClientInfo clientInfo)
        {
            ClientInfo = clientInfo;
        }

        public ClientInfo ClientInfo { get; }
    }
}