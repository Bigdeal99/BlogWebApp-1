using System;
using System.Collections.Generic;
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
    [Route("api/blog")]
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
        public IActionResult Get()
        {
            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched",
                ResponseData = _blogService.GetBlogForFeed()
            });
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<ResponseDto>> GetBlogByIdAsync([FromRoute] int blogId)
        {
            var blog = await _blogService.GetBlogByIdAsync(blogId);

            if (blog == null)
            {
                return NotFound(new ResponseDto { MessageToClient = "Blog not found" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully fetched blog",
                ResponseData = blog
            });
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult<ResponseDto> Post([FromBody] CreateBlogRequestDto dto)
        {
            try
            {
                var createdBlog = _blogService.CreateBlog(dto.BlogTitle, dto.BlogContent);


                return StatusCode(StatusCodes.Status201Created, new ResponseDto
                {
                    MessageToClient = "Successfully created a blog",
                    ResponseData = createdBlog
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    MessageToClient = "Failed to create a blog",
                    ResponseData = ex.Message
                });
            }
        }


        [HttpPut("{blogId}")]
        [ValidateModel]
        public ActionResult<ResponseDto> Put([FromRoute] int blogId, [FromBody] UpdateBlogRequestDto dto)
        {
            var updatedBlog = _blogService.UpdateBlog(blogId, dto.BlogTitle, dto.BlogContent);

            if (updatedBlog == null)
            {
                return NotFound(new ResponseDto { MessageToClient = "Blog not found" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully updated",
                ResponseData = updatedBlog
            });
        }


        [HttpDelete("{blogId}")]
        public ActionResult<ResponseDto> Delete([FromRoute] int blogId)
        {
            _blogService.DeleteBlog(blogId);
            return Ok(new ResponseDto { MessageToClient = "Successfully deleted" });
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _blogService.GetCategories();

            if (categories == null || !categories.Any())
            {
                return NotFound(new ResponseDto { MessageToClient = "No blog categories found" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully retrieved blog categories",
                ResponseData = categories
            });
        }


        [HttpGet("category/{categoryId}")]
        public IActionResult GetPostsByCategory(int categoryId)
        {
            var posts = _blogService.GetPostsByCategory(categoryId);

            if (posts == null || !posts.Any())
            {
                return NotFound(new ResponseDto { MessageToClient = "No blog posts found for the specified category" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully retrieved blog posts by category",
                ResponseData = posts
            });
        }


        [HttpPost("comment")]
        public IActionResult PostComment([FromBody] CommentDto commentDto)
        {
            var createdComment = _blogService.CreateCommentAsync(commentDto.CommenterName, commentDto.Email, commentDto.Text);

            return CreatedAtAction(nameof(PostComment), new ResponseDto
            {
                MessageToClient = "Successfully created a comment",
                ResponseData = createdComment
            });
        }


        [HttpGet("search")]
        public IActionResult SearchBlogPosts([FromQuery] string query)
        {
            var searchResults = _blogService.SearchBlogPosts(query);

            if (searchResults == null || !searchResults.Any())
            {
                return NotFound(new ResponseDto { MessageToClient = "No blog posts found for the specified query" });
            }

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully retrieved search results",
                ResponseData = searchResults
            });
        }


        [HttpGet("about")]
        public IActionResult GetAboutPage()
        {
            object? aboutInfo = _blogService.GetAboutPageInfo();

            return Ok(new ResponseDto
            {
                MessageToClient = "Successfully retrieved information about the blog",
                ResponseData = aboutInfo
            });
        }

    }
}
