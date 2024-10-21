using System;

namespace GuestiaCodingTask.Data
{
    /// <summary>
    /// Represents a guest in the Guestia application.
    /// </summary>
    public class Guest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the guest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the guest's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the guest's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date the guest registered on the platform. Will return null if the guest has not registered yet.
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the guest group that this guest is assigned to.
        /// </summary>
        public GuestGroup GuestGroup { get; set; }
    }
}