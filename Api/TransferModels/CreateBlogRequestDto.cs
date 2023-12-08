using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateBlogRequestDto
{
    
    [Required]
    public string BlogTitle { get; set; }
    
    [Required]
    public string BlogContent { get; set; }
}