namespace infrastructure.DataModels;

public class Blog
{
    public string BlogTitle { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; }
    public DateTime BlogPublicationDate { get; set; } 
    public List<string> BlogCategories { get; set; } 
    public List<Comment> Comments { get; set; }
}