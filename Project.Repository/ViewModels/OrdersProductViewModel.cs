namespace Project.Repository.ViewModels;

public class OrdersProductViewModel
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public decimal Rate { get; set; }

    public decimal Quantity { get; set; }

    public decimal Amount { get; set; }

    public string? Date { get; set; }
}
