using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMovies.Models;

namespace SuperMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        private readonly DB db;
        private readonly Dummy dummy;
       

        public MoviesController(DB db, Dummy dummy, ClientInfo client) : base(client)
        {
            this.db = db;
            this.dummy = dummy;
           
        }

        [HttpGet("")]
        public ActionResult Get()
        {

            return NoContent();
        }

        [HttpGet("list")]
       
        public ActionResult List(bool loadDirector = false)
        {

            var user = this.User?.Identity?.Name;
            //var moviesQuery = db.Movies.AsNoTracking();
            //if (loadDirector)
            //{
            //    moviesQuery = moviesQuery.Include()
            //}

            //var movies = db.Movies.AsNoTracking()
            //    .Select(m => new { id = m.Id, title = m.Title })
            //    .ToList()
            //    .Select(m => new Movie { Id = m.id, Title = m.title })
            //    .ToList();


            var movies = db.Movies.AsNoTracking()
                .Include(m => m.Persons).ThenInclude(p => p.Person)
                .ToList();
            return Ok(movies);
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(Movie movie)
        {
            //var movie = new Movie
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "Starwars Return of the Jedi",
            //    Director = new Person
            //    {
            //        Id = Guid.NewGuid(),
            //        FullName = "Georges Lucas",
            //    },
            //    Duration = 120,
            //    Year = 1981
            //};

            //var dbDirector = db.Persons.FirstOrDefault(p => p.Id == movie.Director.Id);
            //if (dbDirector != null)
            //{
            //    movie.Director = dbDirector;
            //}

            //if (db.Persons.Any(p => p.Id == movie.Director.Id))
            //{
            //    if (movie.Director != null)
            //    {
            //        db.Entry(movie.Director).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            //    }
            //}

            db.Movies.Add(movie);

            await db.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("foo")]
        public async Task<ActionResult> Foo(string lang)
        {
            var user = this.User?.Identity?.Name;
            var bar = new Bar { FirstName =  this.ClientInfo.Culture == "ar-LB" ? "Abou Zelof" : "Firas", TheTime = TimeSpan.FromDays(1) };
            return Ok(bar);
        }
    }

    public class Bar
    {
        public string FirstName { get; set; }
        public TimeSpan? TheTime { get; set; }
    }
}