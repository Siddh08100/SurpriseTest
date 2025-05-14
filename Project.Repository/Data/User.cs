namespace Project.Repository.Data;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Roleid { get; set; }
    public virtual Role? Role { get; set; }

}
