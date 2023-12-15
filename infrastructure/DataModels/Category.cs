namespace infrastructure.DataModels;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<BlogPost> BlogPosts { get; set; }
}