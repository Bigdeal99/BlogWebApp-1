using infrastructure.DataModels;
using System;
using System.Collections.Generic;

public class Blog
{
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; } // Mark as nullable
    public string? BlogContent { get; set; } // Mark as nullable
    public DateTime BlogPublicationDate { get; set; } = DateTime.UtcNow;
    
    // Assuming Category is a complex type with more than just a name
    public List<Category> Categories { get; set; } = new List<Category>();

    public List<Comment> Comments { get; set; } = new List<Comment>();
}