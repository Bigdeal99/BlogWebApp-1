using infrastructure.DataModels;
using System;
using System.Collections.Generic;

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
    public DateTime PublicationDate { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public string FeaturedImage { get; set; }
}