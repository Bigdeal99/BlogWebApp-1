using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service
{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;

        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogForFeedAsync()
        {
            // Adjust the repository method to be async
            return await _blogRepository.GetBlogsForFeedAsync();
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            return await _blogRepository.DeleteBlogAsync(blogId);
        }

        public async Task<BlogPost> UpdateBlogAsync(int blogId, string blogTitle, string blogContent)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                throw new KeyNotFoundException($"No blog found with ID {blogId}");
            }

            if (string.IsNullOrWhiteSpace(blogTitle) || string.IsNullOrWhiteSpace(blogContent))
            {
                throw new ArgumentException("BlogPost title and content cannot be empty.");
            }

            blog.BlogTitle = blogTitle;
            blog.BlogContent = blogContent;

            // The repository method should return a boolean indicating success or failure
            var success = await _blogRepository.UpdateBlogAsync(blog);
            if (!success)
            {
                throw new Exception("Unable to update blog post.");
            }

            return blog;
        }

        public async Task<BlogPost> GetBlogByIdAsync(int blogId)
        {
            return await _blogRepository.GetBlogByIdAsync(blogId);
        }

        public async Task<Comment> CreateCommentAsync(string commenterName, string email, string text, int blogId)
        {
            var newComment = new Comment
            {
                CommenterName = commenterName,
                Email = email,
                Text = text,
                PublicationDate = DateTime.UtcNow
            };

            // Assuming that CreateCommentAsync now returns the ID of the created comment
            var commentId = await _blogRepository.CreateCommentAsync(newComment, blogId);
            newComment.Id = commentId;

            return newComment;
        }


        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(int categoryId)
        {
            return await _blogRepository.GetPostsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<BlogPost>> SearchBlogPostsAsync(string searchTerm)
        {
            return await _blogRepository.SearchBlogPostsAsync(searchTerm);
        }

        public async Task<object> GetAboutPageInfoAsync()
        {
            return await _blogRepository.GetAboutPageInfoAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _blogRepository.GetCategoriesAsync();
        }

        public async Task<BlogPost> CreateBlogAsync(string blogTitle, string blogContent)
        {
            var newBlog = new BlogPost
            {
                BlogTitle = blogTitle,
                BlogContent = blogContent,
                BlogPublicationDate = DateTime.UtcNow
                // Categories and Comments are typically added separately, so they might start empty
            };

            // Assuming that CreateBlogAsync now returns the ID of the created blog
            var blogId = await _blogRepository.CreateBlogAsync(newBlog);
            newBlog.BlogId = blogId;

            return newBlog;
        }
    }
}
