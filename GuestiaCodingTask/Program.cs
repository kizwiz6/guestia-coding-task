using GuestiaCodingTask.Data;
using GuestiaCodingTask.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuestiaCodingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DbInitialiser.CreateDb();
            var groupedGuests = GetUnregisteredGuestsGrouped();

            foreach (var group in groupedGuests)
            {
                Console.WriteLine($"Guest Group: {group.Key} - Unregistered Guests: {group.Value.Count}");
                foreach (var guest in group.Value)
                {
                    Console.WriteLine($" - {FormatGuestName(guest)}");
                }
            }
        }

        /// <summary>
        /// Retrieves all guests that have not registered yet and groups them by their guest group name.
        /// </summary>
        /// <returns>A dictionary where the key is the guest group name and the value is a list of unregistered guests in that group.</returns>
        static Dictionary<string, List<Guest>> GetUnregisteredGuestsGrouped()
        {
            using (var context = new GuestiaContext())
            {
                // First, retrieve guests who haven't registered
                var unregisteredGuests = context.Guests
                    .Where(g => !g.RegistrationDate.HasValue)
                    .Include(g => g.GuestGroup) // Make sure to include GuestGroup to access its properties
                    .ToList(); // Execute the query and load the data into memory

                // Now group by GuestGroup name in memory
                return unregisteredGuests
                    .GroupBy(g => g.GuestGroup.Name)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
        }

        /// <summary>
        /// Formats the guest's name based on the display format specified in the GuestGroup.
        /// </summary>
        /// <param name="guest">The guest whose name needs to be formatted.</param>
        /// <returns>A formatted string representing the guest's name.</returns>
        static string FormatGuestName(Guest guest)
        {
            var formatType = guest.GuestGroup?.NameDisplayFormat;

            return formatType switch
            {
                NameDisplayFormatType.UpperCaseLastNameSpaceFirstName => $"{guest.LastName.ToUpper()} {guest.FirstName}",
                NameDisplayFormatType.LastNameCommaFirstNameInitial => $"{guest.LastName}, {guest.FirstName[0]}.",
                _ => $"{guest.FirstName} {guest.LastName}" // Default case
            };
        }
    }
}
