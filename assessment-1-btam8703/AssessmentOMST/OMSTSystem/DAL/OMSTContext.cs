namespace OMSTSystem.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using OMSTSystem.Entities;

    internal class OMSTContext : DbContext
    {
        public OMSTContext()
            : base("name=OMSTDB")
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Movy> Movies { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<ScreenType> ScreenTypes { get; set; }
        public virtual DbSet<ShowTime> ShowTimes { get; set; }
        public virtual DbSet<Theatre> Theatres { get; set; }
        public virtual DbSet<TicketCategory> TicketCategories { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Movies)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Theatres)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movy>()
                .HasMany(e => e.ShowTimes)
                .WithRequired(e => e.Movy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .HasMany(e => e.Movies)
                .WithRequired(e => e.Rating)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScreenType>()
                .HasMany(e => e.Movies)
                .WithRequired(e => e.ScreenType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShowTime>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.ShowTime)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theatre>()
                .HasMany(e => e.ShowTimes)
                .WithRequired(e => e.Theatre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TicketCategory>()
                .Property(e => e.TicketPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TicketCategory>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.TicketCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.TicketPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.TicketPremium)
                .HasPrecision(19, 4);
        }
    }
}
