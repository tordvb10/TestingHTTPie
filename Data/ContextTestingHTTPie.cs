using Microsoft.EntityFrameworkCore;
using TestingHTTPie.Models;

namespace TestingHTTPie.Data
{
    public class ContextTestingHTTPie : DbContext
    {
        public ContextTestingHTTPie(DbContextOptions<ContextTestingHTTPie> options) : base(options) { }

        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<HobbyPerson> HobbyPersons { get; set; }

        public DbSet<FileModel> Files { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Person id-Key
            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);

            // Employee id-Key
            modelBuilder.Entity<Hobby>()
                .HasKey(h => h.Id);


            modelBuilder.Entity<HobbyPerson>(entity =>
            {
                entity.HasKey(hp => new { hp.HobbyId, hp.PersonId });

                entity.HasOne(hp => hp.Hobby)
                      .WithMany(h => h.HobbyPersons)
                      .HasForeignKey(hp => hp.HobbyId);

                entity.HasOne(hp => hp.Person)
                      .WithMany(p => p.HobbyPersons)
                      .HasForeignKey(hp => hp.PersonId);

            });
        }
    }
}
