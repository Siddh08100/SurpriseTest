namespace Project.Repository.Data;

public class Orders
{
    public int Id { get; set; }
    public decimal Totalamount { get; set; }
    public virtual ICollection<ProductOrder> Productitems { get; set; } = new List<ProductOrder>();
    
}
