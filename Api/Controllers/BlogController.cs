using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using service;

namespace library.Controllers
{
    
    public class BlogController : ControllerBase
    
    {
        private readonly ILogger<BlogController> _logger;
        private readonly BlogService _blogService;

        public BlogController(ILogger<BlogController> logger, BlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }
        
        [HttpGet]
        [Route("/api/blog")]
        public ResponseDto Get()
        {
            HttpContext.Response.StatusCode = 200;
            return new ResponseDto()
            {
                MessageToClient = "Successfully fetched",
                ResponseData = _blogService.GetBlogPostForFeedAsync()
            };
        }
        
        [HttpGet]
        [Route("/api/blog/{Id}")]
        public async Task<ResponseDto> GetBlogPostByIdAsync([FromRoute] int id)
        {
            var blogPost = await _blogService.GetBlogPostByIdAsync(id);
    
            if (blogPost == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new ResponseDto()
                {
                    MessageToClient = "Blog not found"
                };
            }

            return new ResponseDto()
            {
                MessageToClient = "Successfully fetched blog",
                ResponseData = blogPost
            };
        }
        

    }

}
