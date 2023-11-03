using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "*Please enter the old password!")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*Please enter the new password!")]
        public string NewPassword { get; set; }
    }
}
