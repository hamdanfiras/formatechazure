using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMovies.Models;

namespace SuperMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DB db;
        private readonly Dummy dummy;

        public MoviesController(DB db, Dummy dummy)
        {
            this.db = db;
            this.dummy = dummy;
        }

        public ActionResult Get()
        {
            return Ok();
        }
    }
}