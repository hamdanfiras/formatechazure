using System;
using System.Collections.Generic;

namespace SuperMovies.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public List<MoviePerson> Movies { get; set; }
    }
}
