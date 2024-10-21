using GuestiaCodingTask.Data;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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
                if (group.Key == "Standard")
                    Console.ForegroundColor = ConsoleColor.Green; // Standard group color
                else if (group.Key == "VIP")
                    Console.ForegroundColor = ConsoleColor.Yellow; // VIP group color

                Console.WriteLine($"\nGuest Group: {group.Key} - Unregistered Guests: {group.Value.Count}");
                Console.ResetColor(); // Reset color for subsequent text

                foreach (var guest in group.Value.OrderBy(g => g.LastName).ThenBy(g => g.FirstName)) // Sort alphabetically       
                {
                    Console.WriteLine($" - {guestFormatter.FormatGuestName(guest)}");
                }
            }

            Console.ResetColor(); // Reset colour to default at the end
        }
    }
}
