namespace API.Entities;

public class AppUser
{
    public int Id { get; set; } //By convetions EF verify if prop name is ID and makes PK, or use DataAnnotations [key]
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new(); //new shorhand to intialize new List<Photo>()

    // public int GetAge()
    // {
    //     return DateOfBirth.CalculageAge();
    // }

}

