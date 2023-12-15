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
        public async Task<IActionResult> Get()
        {
            try
            {
                var blogs = await _blogService.GetBlogForFeedAsync();
                return Ok(new ResponseDto { MessageToClient = "Successfully fetched", ResponseData = blogs });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching blogs");
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { MessageToClient = "An error occurred while fetching blogs" });
            }
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<ResponseDto>> GetBlogByIdAsync([FromRoute] int blogId)
        {
            try
            {
                var blog = await _blogService.GetBlogByIdAsync(blogId);

                if (blog == null)
                {
                    return NotFound(new ResponseDto { MessageToClient = "Blog not found" });
                }

                return Ok(new ResponseDto { MessageToClient = "Successfully fetched blog", ResponseData = blog });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blog with ID {BlogId}", blogId);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { MessageToClient = "An error occurred while retrieving the blog", ResponseData = ex.Message });
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> Post([FromBody] CreateBlogRequestDto dto)
        {
            try
            {
                var createdBlog = await _blogService.CreateBlogAsync(dto.BlogTitle, dto.BlogContent);
                return StatusCode(StatusCodes.Status201Created, new ResponseDto { MessageToClient = "Successfully created a blog", ResponseData = createdBlog });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating blog");
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { MessageToClient = "Failed to create a blog", ResponseData = ex.Message });
            }
        }

        [HttpPut("{blogId}")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> Put([FromRoute] int blogId, [FromBody] UpdateBlogRequestDto dto)
        {
            try
            {
                var updatedBlog = await _blogService.UpdateBlogAsync(blogId, dto.BlogTitle, dto.BlogContent);
                return Ok(new ResponseDto { MessageToClient = "Successfully updated", ResponseData = updatedBlog });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ResponseDto { MessageToClient = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating blog with ID {BlogId}", blogId);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { MessageToClient = "Failed to update blog", ResponseData = ex.Message });
            }
        }

        [HttpDelete("{blogId}")]
        public async Task<IActionResult> Delete([FromRoute] int blogId)
        {
            try
            {
                await _blogService.DeleteBlogAsync(blogId);
                return Ok(new ResponseDto { MessageToClient = "Successfully deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting blog with ID {BlogId}", blogId);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { MessageToClient = "Failed to delete blog", ResponseData = ex.Message });
            }
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _blogService.GetCategoriesAsync();

            return Ok(new ResponseDto { MessageToClient = "Successfully retrieved blog categories", ResponseData = categories });
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetPostsByCategory([FromRoute] int categoryId)
        {
            var posts = await _blogService.GetPostsByCategoryAsync(categoryId);
            return Ok(new ResponseDto { MessageToClient = "Successfully retrieved blog posts by category", ResponseData = posts });
        }

        [HttpPost("comment")]
        public async Task<IActionResult> PostComment([FromBody] CommentDto commentDto, [FromQuery] int blogId)
        {
            try
            {
                var createdComment = await _blogService.CreateCommentAsync(commentDto.CommenterName, commentDto.Email, commentDto.Text, blogId);
                return CreatedAtAction(nameof(PostComment), new ResponseDto { MessageToClient = "Successfully created a comment", ResponseData = createdComment });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating comment");
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto { MessageToClient = "Failed to create a comment", ResponseData = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBlogPosts([FromQuery] string query)
        {
            var searchResults = await _blogService.SearchBlogPostsAsync(query);
            return Ok(new ResponseDto { MessageToClient = "Successfully retrieved search results", ResponseData = searchResults });
        }

        [HttpGet("about")]
        public async Task<IActionResult> GetAboutPage()
        {
            var aboutInfo = await _blogService.GetAboutPageInfoAsync();
            return Ok(new ResponseDto { MessageToClient = "Successfully retrieved information about the blog", ResponseData = aboutInfo });
        }
    }
}
