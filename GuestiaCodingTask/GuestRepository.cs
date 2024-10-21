using GuestiaCodingTask.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GuestiaCodingTask
{
    /// <summary>
    /// Handles database interactions, such as fetching unregistered guests.
    /// </summary>
    public class GuestRepository : IGuestRepository
    {
        /// <summary>
        /// Retrieves all guests that have not registered yet and groups them by their guest group name.
        /// </summary>
        /// <returns>A dictionary where the key is the guest group name and the value is a list of unregistered guests in that group.</returns>
        public Dictionary<string, List<Guest>> GetUnregisteredGuestsGrouped()
        {
            using (var context = new GuestiaContext())
            {
                // Retrieve guests who haven't registered
                var unregisteredGuests = context.Guests
                    .Where(g => !g.RegistrationDate.HasValue)
                    .Include(g => g.GuestGroup) // Access GuestGroup properties
                    .ToList(); // Execute the query and load the data into memory

                // Group by GuestGroup name in memory
                return unregisteredGuests
                    .GroupBy(g => g.GuestGroup.Name)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
        }
    }
}
