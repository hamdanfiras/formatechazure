using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMovies.Models
{
    public class DB : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MoviePerson>().HasKey(m2m => new { m2m.MovieId, m2m.PersonId });
            
            modelBuilder.Entity<MoviePerson>()
                .HasOne(m2m => m2m.Movie)
                .WithMany(o => o.Persons)
                .HasForeignKey(m2m => m2m.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MoviePerson>()
                .HasOne(m2m => m2m.Person)
                .WithMany(o => o.Movies)
                .HasForeignKey(m2m => m2m.PersonId);

        }
        public DB(DbContextOptions<DB> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
