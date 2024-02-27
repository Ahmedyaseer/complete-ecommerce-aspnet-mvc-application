using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.ViewModels
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)   
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey( am => new 
            {
                am.ActorId,
                am.MovieId
            });
            //optional
            modelBuilder.Entity<Actor_Movie>().HasOne(a=>a.Actor).WithMany(am=>am.Actors_Movies).HasForeignKey(a=>a.ActorId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m=>m.Movie).WithMany(am=>am.Actors_Movies).HasForeignKey(m=>m.MovieId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Actor>? Actors { get; set; }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Cinema>? Cinemas { get; set; }
        public DbSet<Producer>? Producers { get; set; }  
        public DbSet<Actor_Movie> Actors_Movies { get; set; }  
    }
}
