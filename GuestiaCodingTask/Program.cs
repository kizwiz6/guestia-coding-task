using GuestiaCodingTask.Data;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace GuestiaCodingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dependency injection
            var serviceProvider = new ServiceCollection()
                .AddTransient<IGuestRepository, GuestRepository>()
                .AddTransient<IGuestFormatter, GuestFormatter>()
                .BuildServiceProvider();

            // Resolve the services
            var guestRepository = serviceProvider.GetService<IGuestRepository>();
            var guestFormatter = serviceProvider.GetService<IGuestFormatter>();

            // Use the repository to get unregistered guests
            var groupedGuests = guestRepository.GetUnregisteredGuestsGrouped();

            // Initialise the database
            DbInitialiser.CreateDb();

            // Output the report
            foreach (var group in groupedGuests)
            {
                Console.WriteLine($"Guest Group: {group.Key} - Unregistered Guests: {group.Value.Count}");
                foreach (var guest in group.Value)
                {
                    Console.WriteLine($" - {guestFormatter.FormatGuestName(guest)}");
                }
            }
        }
    }
}
