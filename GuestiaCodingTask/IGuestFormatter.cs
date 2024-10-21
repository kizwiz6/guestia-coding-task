using GuestiaCodingTask.Data;

namespace GuestiaCodingTask
{
    /// <summary>
    /// Defines methods for formatting guest information.
    /// </summary>
    public interface IGuestFormatter
    {
        /// <summary>
        /// Formats the guest's name based on the display format specified in the GuestGroup.
        /// </summary>
        /// <param name="guest">The guest whose name needs to be formatted.</param>
        /// <returns>A formatted string representing the guest's name.</returns>
        string FormatGuestName(Guest guest);
    }
}
