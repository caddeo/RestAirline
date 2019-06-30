using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RestAirline.ReadModel.EntityFramework.Booking;

namespace RestAirline.ReadModel.EntityFramework.DBContext
{
    public class ReadModelDbContext : DbContext
    {
        public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options)
        {
        }

        public DbSet<BookingReadModel> Bookings { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Journey> Journeys { get; set; }

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingReadModel>()
                .HasMany<Passenger>()
                .WithOne(p => p.BookingReadModel)
                ;

            modelBuilder.Entity<BookingReadModel>()
                .HasMany<Journey>()
                .WithOne(j => j.BookingReadModel)
                ;

            modelBuilder.Entity<Journey>()
                .HasOne(j => j.Flight)
                .WithOne(f => f.Journey)
                .HasForeignKey<Flight>(f => f.JourneyKey)
                ;
        }
    }

    public class ReadModelDbContextDesignFactory : IDesignTimeDbContextFactory<ReadModelDbContext>
    {
        public ReadModelDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadModelDbContext>()
                .UseSqlServer("Server=localhost;Database=RestAirlineRead;User Id=sa;Password=RestAirline123");

            return new ReadModelDbContext(optionsBuilder.Options);
        }
    }
}