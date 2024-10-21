using GuestiaCodingTask.Data;
using GuestiaCodingTask.Helpers;
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
            var unregisteredGuests = GetUnregisteredGuests();
            Console.WriteLine($"Number of unregistered guests: {unregisteredGuests.Count}");

        }

        /// <summary>
        /// Retrieves all guests that have not registered yet.
        /// </summary>
        static List<Guest> GetUnregisteredGuests()
        {
            using (var context = new GuestiaContext())
            {
                return context.Guests
                    .Where(g => !g.RegistrationDate.HasValue)
                    .ToList();
            }
        }
    }
}
