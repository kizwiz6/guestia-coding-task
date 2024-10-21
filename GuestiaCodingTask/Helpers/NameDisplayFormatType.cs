using System.ComponentModel.DataAnnotations;

namespace GuestiaCodingTask.Helpers
{
    /// <summary>
    /// Specifies how the name of guests in a group should be displayed depending on their group.
    /// </summary>
    public enum NameDisplayFormatType
    {
        [Display(Name = "LASTNAME FirstName")]
        UpperCaseLastNameSpaceFirstName,
        [Display(Name = "LastName, F(irstName)")]
        LastNameCommaFirstNameInitial
    }
}
