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
                var serviceProvider = ConfigureServices();
                var guestRepository = serviceProvider.GetService<IGuestRepository>();
                var guestFormatter = serviceProvider.GetService<IGuestFormatter>();

                // Check if services were resolved successfully
                ValidateServices(guestRepository, guestFormatter);

                // Initialise the database
                InitialiseDatabase();

                // Retrieve and output the report
                var groupedGuests = GetUnregisteredGuests(guestRepository);
                OutputReport(groupedGuests, guestFormatter);
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

        /// <summary>
        /// Configures the services required by the application.
        /// </summary>
        /// <returns>A service provider with the configured services.</returns>
        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddTransient<IGuestRepository, GuestRepository>()
                .AddTransient<IGuestFormatter, GuestFormatter>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Validates that the required services were resolved successfully.
        /// </summary>
        /// <param name="guestRepository">The guest repository service.</param>
        /// <param name="guestFormatter">The guest formatter service.</param>
        private static void ValidateServices(IGuestRepository guestRepository, IGuestFormatter guestFormatter)
        {
            if (guestRepository == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error resolving IGuestRepository service.");
                Console.ResetColor();
                Environment.Exit(1);
            }

            if (guestFormatter == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error resolving IGuestFormatter service.");
                Console.ResetColor();
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Initialises the database by creating the necessary tables and data.
        /// </summary>
        private static void InitialiseDatabase()
        {
            try
            {
                DbInitialiser.CreateDb();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error initialising the database: {ex.Message}");
                Console.ResetColor();
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Retrieves all unregistered guests grouped by their guest group name.
        /// </summary>
        /// <param name="guestRepository">The guest repository service.</param>
        /// <returns>A dictionary where the key is the guest group name and the value is a list of unregistered guests in that group.</returns>
        private static Dictionary<string, List<Guest>> GetUnregisteredGuests(IGuestRepository guestRepository)
        {
            try
            {
                return guestRepository.GetUnregisteredGuestsGrouped();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error retrieving unregistered guests: {ex.Message}");
                Console.ResetColor();
                Environment.Exit(1);
                return null; // Unreachable, but included for compiler satisfaction
            }
        }

        /// <summary>
        /// Outputs a report of unregistered guests grouped by their guest group name.
        /// </summary>
        /// <param name="groupedGuests">A dictionary where the key is the guest group name and the value is a list of unregistered guests in that group.</param>
        /// <param name="guestFormatter">The guest formatter service.</param>
        private static void OutputReport(Dictionary<string, List<Guest>> groupedGuests, IGuestFormatter guestFormatter)
        {
            foreach (var group in groupedGuests)
            {
                Console.ForegroundColor = group.Key == "Standard" ? ConsoleColor.Green : ConsoleColor.Yellow;
                Console.WriteLine($"\nGuest Group: {group.Key} - Unregistered Guests: {group.Value.Count}");
                Console.ResetColor();

                int count = 1;
                foreach (var guest in group.Value.OrderBy(g => g.LastName).ThenBy(g => g.FirstName))
                {
                    Console.WriteLine($"    {count}. {guestFormatter.FormatGuestName(guest)}");
                    count++;
                }
            }
        }
    }
}
