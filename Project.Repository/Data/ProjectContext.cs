using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Project.Repository.Data;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Products> Products { get; set; }
    public virtual DbSet<Orders> Orders { get; set; }
    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=Project;Username=postgres;Password=Tatva@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "account_email_key").IsUnique();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");

            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");


            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstname");

            entity.Property(e => e.LastName)
            .HasMaxLength(50)
            .HasColumnName("lastname");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("users_roleid_fkey");

        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Products_pkey");
            entity.ToTable("products");

            entity.HasIndex(e => e.Name, "Products_name_key").IsUnique();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.Property(e => e.Description)
            .HasMaxLength(250)
            .HasColumnName("description");

            entity.Property(e => e.Category)
            .HasMaxLength(50)
            .HasColumnName("category");

            entity.Property(e => e.Price)
                .HasPrecision(18, 2)
                .HasColumnName("price");

            entity.Property(e => e.Stock).HasColumnName("quantity");

            entity.Property(e => e.Createdby)
            .HasMaxLength(50)
            .HasColumnName("createdby");

            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");

            entity.Property(e => e.Updatedby)
            .HasMaxLength(50)
            .HasColumnName("updatedby");

            entity.Property(e => e.Updateddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updateddate");

        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");
            entity.ToTable("orders");

            entity.Property(e => e.Totalamount)
                .HasPrecision(18, 2)
                .HasColumnName("price");

            entity.Property(e => e.Createdby)
            .HasMaxLength(50)
            .HasColumnName("createdby");

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");

            entity.Property(e => e.Updatedby)
            .HasMaxLength(50)
            .HasColumnName("updatedby");

            entity.Property(e => e.Updateddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updateddate");

        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Productorders_pkey");
            entity.ToTable("productOrder");

            entity.Property(e => e.Amount)
                .HasPrecision(18, 2)
                .HasColumnName("price");

            entity.Property(e => e.Productsid).HasColumnName("Productid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.Property(e => e.Rate)
                .HasPrecision(18, 2)
                .HasColumnName("rate");

            entity.HasOne(d => d.Products).WithMany(p => p.ProductOrders)
            .HasForeignKey(d => d.Productsid)
            .HasConstraintName("ProductOrders_ProductOrderId_fkey");

            entity.HasOne(d => d.Orders).WithMany(p => p.Productitems)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("ProductOrders_orderid_fkey");

        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
