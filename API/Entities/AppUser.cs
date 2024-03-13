namespace API.Entities;

public class AppUser
{
    public int Id { get; set; } //By convetions EF verify if prop name is ID and makes PK, or use DataAnnotations [key]
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

}
