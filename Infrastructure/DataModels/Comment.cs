namespace infrastructure.DataModels;

public class Comment
{
    public int Id { get; set; } // Unique identifier for the comment
    public string CommenterName { get; set; } // Name of the commenter
    public string Email { get; set; } // Email of the commenter (optional)
    public string Text { get; set; } // Comment text
    public DateTime PublicationDate { get; set; }
}