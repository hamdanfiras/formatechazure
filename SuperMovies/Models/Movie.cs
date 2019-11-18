using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperMovies.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }

        
        public List<MoviePerson> Persons { get; set; }
    }
        
    public class MoviePerson
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }


}
