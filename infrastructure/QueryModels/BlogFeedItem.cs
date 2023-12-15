namespace infrastructure.QueryModels;

public class CommentFeedQuery
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

}

public class BlogFeedQuery
{
    public string BlogTitle { get; set; }
    public string BlogContent { get; set; }

    public IList<CommentFeedQuery> Comments { get; set; }

   

}