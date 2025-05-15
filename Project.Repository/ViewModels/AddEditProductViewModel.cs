using System.ComponentModel.DataAnnotations;

namespace Project.Repository.ViewModels;

public class AddEditProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Stock is required")]
    public short Stock { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; }
    public int? TotalAmount { get; set; }
}
