using Microsoft.EntityFrameworkCore;
using TestingHTTPie.Models;
using TestingHTTPie.Models.Base;

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
            ApplyBaseConfigurations<Person>(modelBuilder);
            ApplyBaseConfigurations<Hobby>(modelBuilder);
            ApplyBaseConfigurations<FileModel>(modelBuilder);
            ApplyBaseConfigurations<HobbyPerson>(modelBuilder);
            modelBuilder.Entity<HobbyPerson>(entity =>
            {
                entity.HasOne(hp => hp.Hobby)
                      .WithMany(h => h.HobbyPersons)
                      .HasForeignKey(hp => hp.HobbyId);

                entity.HasOne(hp => hp.Person)
                      .WithMany(p => p.HobbyPersons)
                      .HasForeignKey(hp => hp.PersonId);

            });

            base.OnModelCreating(modelBuilder);
        }
        private void ApplyBaseConfigurations<TEntity>(ModelBuilder modelBuilder) 
            where TEntity : class, ICommonProperties
        {
            modelBuilder.Entity<TEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RowVersion)
                .IsRowVersion();
            });
        }
    }
}
