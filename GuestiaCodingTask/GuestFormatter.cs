using GuestiaCodingTask.Data;
using GuestiaCodingTask.Helpers;

namespace GuestiaCodingTask
{
    /// <summary>
    /// Handles the formatting of guest names based on their group specification.
    /// </summary>
    public class GuestFormatter : IGuestFormatter
    {
        /// <summary>
        /// Formats the guest's name based on the display format specified in the GuestGroup.
        /// </summary>
        /// <param name="guest">The guest whose name needs to be formatted.</param>
        /// <returns>A formatted string representing the guest's name.</returns>
        public string FormatGuestName(Guest guest)
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
