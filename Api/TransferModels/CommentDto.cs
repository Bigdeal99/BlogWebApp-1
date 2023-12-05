namespace api.TransferModels
{
    public class CommentDto
    {
        public string CommenterName { get; set; } // Name of the commenter
        public string Email { get; set; } // Email of the commenter (optional)
        public string Text { get; set; } // Comment text
    }
}