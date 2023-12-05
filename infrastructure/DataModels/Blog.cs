
using infrastructure.DataModels;
public class Blog
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; } 
    public string BlogContent { get; set; }
    public DateTime BlogPublicationDate { get; set; } 
    public List<string> BlogCategories { get; set; } 
    public List<Comment> BlogComments { get; set; }
}