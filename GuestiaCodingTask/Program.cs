using GuestiaCodingTask.Data;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace GuestiaCodingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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
                if (guestRepository == null || guestFormatter == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new InvalidOperationException("Failed to resolve dependencies.");
                }

                // Initialise the database
                try
                {
                    DbInitialiser.CreateDb();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error initialising the database: {ex.Message}");
                    Console.ResetColor();
                    return;
                }

                // Use the repository to get unregistered guests
                Dictionary<string, List<Guest>> groupedGuests;
                try
                {
                    groupedGuests = guestRepository.GetUnregisteredGuestsGrouped();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error retrieving unregistered guests: {ex.Message}");
                    Console.ResetColor();
                    return;
                }

                // Output the report
                foreach (var group in groupedGuests)
                {
                    if (group.Key == "Standard")
                        Console.ForegroundColor = ConsoleColor.Green; // Standard group colour
                    else if (group.Key == "VIP")
                        Console.ForegroundColor = ConsoleColor.Yellow; // VIP group colour

                    Console.WriteLine($"\nGuest Group: {group.Key} - Unregistered Guests: {group.Value.Count}");
                    Console.ResetColor();

                    int count = 1; // Counter for numbering guests
                    foreach (var guest in group.Value.OrderBy(g => g.LastName).ThenBy(g => g.FirstName)) // Sort alphabetically       
                    {
                        Console.WriteLine($"    {count}. {guestFormatter.FormatGuestName(guest)}"); // Indentation and numbering for guest names
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nPress any key to exit.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
