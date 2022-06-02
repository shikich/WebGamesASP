using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGamesCRUD.Models;

namespace WebGamesCRUD.Repository
{
    public partial class GamesDbContext : DbContext
    {
        public GamesDbContext()
        {
        }
        public GamesDbContext(DbContextOptions<GamesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<ListGenre> ListGenres { get; set; }
        public object Genre { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("PasteYourConnectingString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListGenre>(entity =>
            {
                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListGenre_Genre");
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ListGenre>().HasKey(
                t => new { t.IdListGenre }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
