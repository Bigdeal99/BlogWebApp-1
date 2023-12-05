using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateBlogRequestDto
{
    [Length(4,6)]
    [Required]
    public string blogTitle { get; set; }
    
    [Required]
    public double blogContent { get; set; }
}