using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API;

[Table("Photos")] //Override table name, we wont add this table in DataContext because we wont be querying for photos, just 1 user can have many photos (none can add for him, etc).
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }

    //Relation
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
