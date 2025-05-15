namespace Project.Repository.ViewModels;

public class OrderDetailsViewModel
{
    public int OrderId { get; set; }
    public string? Status { get; set; }
    public int? TotalAmount { get; set; }
    public List<string>? ProductNames {get;set;} 
    public List<AddEditProductViewModel>? Items { get; set; }
    // public List<OrdersProductViewModel>? ProductItems { get; set; }
}
