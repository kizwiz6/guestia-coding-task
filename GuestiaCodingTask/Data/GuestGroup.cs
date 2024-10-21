using GuestiaCodingTask.Helpers;

namespace GuestiaCodingTask.Data
{
    /// <summary>
    /// Represents a group of guests.
    /// </summary>
    public class GuestGroup
    {
        /// <summary>
        /// Gets or sets the unique identifier for the guest group.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the guest group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the format for displaying the names of guests in this group.
        /// </summary>
        public NameDisplayFormatType NameDisplayFormat { get; set; }
    }
}


