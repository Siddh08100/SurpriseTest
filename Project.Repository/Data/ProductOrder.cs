namespace Project.Repository.Data;

public class ProductOrder
{
    public int Id { get; set; }

    public int? Productsid { get; set; }

    public int? Orderid { get; set; }

    public short Quantity { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public decimal Amount { get; set; }

    public virtual Products? Products { get; set; }

    public virtual Orders? Orders { get; set; }
}
