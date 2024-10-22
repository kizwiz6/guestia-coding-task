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
            // Check if guest is null
            if (guest == null)
            {
                return "Unknown Guest";
            }

            // Handle potential null values in FirstName and LastName
            var firstName = string.IsNullOrWhiteSpace(guest.FirstName) ? "N/A." : $"{guest.FirstName[0]}.";
            var lastName = string.IsNullOrWhiteSpace(guest.LastName) ? "UNKNOWN" : guest.LastName;

            // Get the display format type, using a safe fallback if null
            var formatType = guest.GuestGroup?.NameDisplayFormat;

            return formatType switch
            {
                NameDisplayFormatType.UpperCaseLastNameSpaceFirstName => $"{lastName.ToUpper()} {guest.FirstName ?? "UNKNOWN"}",
                NameDisplayFormatType.LastNameCommaFirstNameInitial => $"{lastName}, {firstName}",
                _ => $"{guest.FirstName ?? "UNKNOWN"} {lastName}" // Default case if format is not recognised
            };
        }
    }
}
