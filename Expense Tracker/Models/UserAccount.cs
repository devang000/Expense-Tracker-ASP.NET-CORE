using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class UserAccount
    {
        [Key]
        public int idUser { get; set; }
        
        [Required (ErrorMessage = "*First Name is Required")]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "*Last Name is Required")]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "*Email Name is Required")]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "*Please enter valid email")]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Username is Required")]
        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*Password is Required")]
        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "*Please Confirm your password.")]
        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(255)")]
        public string ConfirmPasssword { get; set; }
    }
}
