namespace Project.Repository.Data;

public class Products
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public short Stock { get; set; }
    public string Category { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime? Createddate { get; set; }
    public string? Createdby { get; set; }
    public DateTime? Updateddate { get; set; }
    public string? Updatedby { get; set; }
    public virtual Orders? Order { get; set; }
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
