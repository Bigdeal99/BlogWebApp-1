namespace infrastructure.DataModels;

public class Comment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime PublicationDate { get; set; }
    public int BlogPostId { get; set; }
    public virtual BlogPost BlogPost { get; set; }
}