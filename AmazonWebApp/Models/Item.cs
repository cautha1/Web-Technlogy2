using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.Models;

public partial class Item
{
    [Key]
    [Display(Name = "Item #")]
    public int ItemId { get; set; }

    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "The {0} is mandatory")]
    [MaxLength(150, ErrorMessage = "The {0} must be < 150 characters")]
    public string ItemName { get; set; } = null!;

    [Display(Name = "Product Description")]
    [Required(ErrorMessage = "A {0} is a must!")]
    [DataType(DataType.MultilineText)]
    [MaxLength(5000, ErrorMessage = "The {0} must be < 5000 characters")]
    public string ItemDescription { get; set; } = null!;


    [Display(Name = "Product Cost")]
    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "You must provide the {0}")]
    [Range(0, 99999999, ErrorMessage = "The {0} must be between 0-999,999")]
    public decimal ItemCost { get; set; }

    [Display(Name = "Product Image")]
    [Required(ErrorMessage = "A {0} is required!")]
    [MaxLength(5000, ErrorMessage = "The {0} URL must be < 5000 characters")]
    [DataType(DataType.ImageUrl)]
    public string ItemImage { get; set; } = null!;

    [Display(Name = "Product Category")]
    public int? CategoryId { get; set; }

    public virtual ItemCategory? Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
