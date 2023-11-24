namespace infrastructure.DataModels;

public class Blog
{
    public string BlogTitle { get; set; }
    public int BlogId { get; set; }
    public string Content { get; set; }
    public DateTime PublicationDate { get; set; } 
    public List<string> Categories { get; set; } 
    public List<Comment> Comments { get; set; }
}