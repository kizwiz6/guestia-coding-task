using GuestiaCodingTask.Data;
using GuestiaCodingTask.Helpers;
using System.Runtime.CompilerServices;

namespace GuestiaCodingTask.Tests
{
    public class GuestFormatterTests
    {
        private readonly GuestFormatter _guestFormatter;

        /// <summary>
        /// Initialises a new instance of the <see cref="GuestFormatterTests"/> class.
        /// This constructor creates a new instance of the GuestFormatter for testing.
        /// </summary>
        public GuestFormatterTests()
        {
            _guestFormatter = new GuestFormatter();
        }

        /// <summary>
        /// Tests the FormatGuestName method when a null guest is provided.
        /// It should return "Unknown Guest".
        /// </summary>
        [Fact]
        public void FormatGuestName_WithNullGuest_ReturnsUnknownGuest()
        {
            // Arrange
            Guest guest = null;

            // Act
            var result = _guestFormatter.FormatGuestName(guest);

            // Assert
            Assert.Equal("Unknown Guest", result);
        }

        /// <summary>
        /// Tests the FormatGuestName method when the first name of the guest is null.
        /// It should return the last name formatted with "K." as the first initial.
        /// </summary>
        [Fact]
        public void FormatGuestName_WithNullFirstName_ReturnsUnknownForFirstName()
        {
            // Arrange
            var guest = new Guest
            {
                FirstName = null,
                LastName = "Emery",
                GuestGroup = new GuestGroup { NameDisplayFormat = NameDisplayFormatType.LastNameCommaFirstNameInitial }
            };

            // Act
            var result = _guestFormatter.FormatGuestName(guest);

            // Assert
            Assert.Equal("Emery, N/A.", result); // Assuming you expect "U." for Unknown
        }
    }
}