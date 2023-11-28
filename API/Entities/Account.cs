using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("accounts")]
public class Account
{
    [Column("id")]
    public int Id { get; private set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("passwordhash")]
    public string PasswordHash { get; set; }
}