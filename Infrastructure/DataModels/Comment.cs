namespace infrastructure.DataModels;

public class Comment
{
    public int Id { get; set; } 
    public string CommenterName { get; set; } 
    public string Email { get; set; } 
    public string Text { get; set; } 
    public DateTime PublicationDate { get; set; }
}