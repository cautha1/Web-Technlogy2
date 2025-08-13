using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models.CodeFirst
{
   
    public class ExampleItem

    {
        [Key]
        public int itemID { get; set; }

        [Required]
        [Display(Name ="Item Name")]
        [StringLength(100, ErrorMessage ="The item musct be less than 100 characters")]
        public string  itemName { get; set; } = string.Empty;

        
        [Range(1,100000, ErrorMessage ="The {0} must be between {1} and {2}")]
        [Display(Name ="Price")]
        [Required(ErrorMessage ="You must provide an item price")]
        [DataType(DataType.Currency)]
        [Column(TypeName ="decimal(8, 2)")]
        public decimal itemPrice { get; set; }

    }
}
