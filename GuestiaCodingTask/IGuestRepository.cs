using GuestiaCodingTask.Data;
using System.Collections.Generic;

namespace GuestiaCodingTask
{
    /// <summary>
    /// Defines methods for accessing and manipulating guest data.
    /// </summary>
    public interface IGuestRepository
    {
        /// <summary>
        /// Retrieves all guests that have not registered yet and groups them by their guest group name.
        /// </summary>
        /// <returns>A dictionary where the key is the guest group name and the value is a list of unregistered guests in that group.</returns>
        Dictionary<string, List<Guest>> GetUnregisteredGuestsGrouped();
    }
}
