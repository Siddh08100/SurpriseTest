namespace Project.Repository.Data;

public class Orders
{
    public int Id { get; set; }
    public decimal Totalamount { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? Createddate { get; set; }
    public string? Createdby { get; set; }
    public DateTime? Updateddate { get; set; }
    public string? Updatedby { get; set; }
    public virtual ICollection<ProductOrder> Productitems { get; set; } = new List<ProductOrder>();

}
