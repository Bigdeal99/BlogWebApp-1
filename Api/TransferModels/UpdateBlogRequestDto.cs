using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class UpdateBlogRequestDto
{
        [Length(4,6)]
        [Required]
        public string BlogTitle { get; set; }
    
        [Required]
        public string BlogContent { get; set; }
    
}