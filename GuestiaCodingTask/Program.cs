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
                    Console.WriteLine($" - {guest.FirstName} {guest.LastName}");
                }

            }

            /// <summary>
            /// Retrieves all guests that have not registered yet.
            /// </summary>
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
        }
    }
}
