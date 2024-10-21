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

            var guestRepository = new GuestRepository();
            var guestFormatter = new GuestFormatter();
            var groupedGuests = guestRepository.GetUnregisteredGuestsGrouped();

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
