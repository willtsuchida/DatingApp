﻿using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API;

[Table("Photos")]
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
