using Microsoft.EntityFrameworkCore;

namespace GuestiaCodingTask.Data
{
    /// <summary>
    /// Represents the database context for the Guestia application.
    /// </summary>
    public class GuestiaContext : DbContext
    {
        /// <summary>
        /// Gets or sets the Guests DbSet.
        /// </summary>
        public DbSet<Guest> Guests { get; set; }

        /// <summary>
        /// Gets or sets the GuestGroups DbSet.
        /// </summary>
        public DbSet<GuestGroup> GuestGroups { get; set; }

        /// <summary>
        /// Configures the database connection options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the database connection.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=GuestiaDB;Trusted_Connection=True;");
        }
    }
}
