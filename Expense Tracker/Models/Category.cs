using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "*Tittle field is mandatory")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";
        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Expense";
        public int Budget { get; set; }
        [Column(TypeName = "nvarchar(75)")]

        [NotMapped]
        public string? TitleWithIcon 
        {
            get
            {
                return this.Icon + " " + this.Title;
            } 
        
        }

    }
}
