using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMovies.Models
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
