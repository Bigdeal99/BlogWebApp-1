using infrastructure.DataModels;

namespace infrastructure.QueryModels;

public class BlogPostFeedQuery
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

public class CommentFeedQuery
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime PublicationDate { get; set; }
    public int BlogPostId { get; set; }
    public virtual BlogPost BlogPost { get; set; }
}
public class CategoryFeedQuery
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<BlogPost> BlogPosts { get; set; }
}
