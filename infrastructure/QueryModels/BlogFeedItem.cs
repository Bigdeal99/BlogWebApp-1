namespace infrastructure.QueryModels;


public class CommentFeedQuery
{
    public string Text { get; set; }
    public string CommenterName { get; set; }

}
public class BlogFeedQuery
{
    public string BlogTitle { get; set; }
    public int BlogContent { get; set; }
    public  IList<CommentFeedQuery> Comments { get; set; } 

}

