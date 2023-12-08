using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.QueryModels;
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

        public IEnumerable<BlogFeedQuery> GetBlogForFeed()
        {
            return _blogRepository.GetBlogForFeed();
        }

        public void DeleteBlog(int blogId)
        {
            var result = _blogRepository.DeleteBlog(blogId);

            if (!result)
            {
                throw new Exception("Could not delete blog");
            }
        }



        public Blog UpdateBlog(int blogId, string blogTitle, string blogContent)
        {
            // Implement blog update logic
            var updatedBlog = _blogRepository.GetBlogById(blogId);

            if (string.IsNullOrWhiteSpace(blogTitle) || string.IsNullOrWhiteSpace(blogContent))
            {
                throw new ArgumentException("Blog title and content cannot be empty.");
            }


            return updatedBlog;
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            // Implement asynchronous blog retrieval logic
            
            return await _blogRepository.GetBlogByIdAsync(blogId);
        }

        public async Task<Comment> CreateCommentAsync(string commenterName, string email, string text)
        {
            var newComment = new Comment
            {
                CommenterName = commenterName,
                Email = email,
                Text = text,
                PublicationDate = DateTime.UtcNow
            };

            await _blogRepository.CreateCommentAsync(newComment);

            return newComment;
        }


        public IEnumerable<Blog> GetPostsByCategory(int categoryId)
        {
            // Assuming _blogRepository.GetPostsByCategory returns a collection of blog posts
            var posts = _blogRepository.GetPostsByCategory(categoryId);

            if (posts == null || !posts.Any())
            {
                // Optionally, you can throw an exception, log, or return an empty list based on your requirements
                return Enumerable.Empty<Blog>();
            }

            return posts;
        }


        public IEnumerable<Blog> SearchBlogPosts(string query)
        {
            // Assuming _blogRepository.SearchBlogPosts returns a collection of blog posts matching the search query
            var searchResults = _blogRepository.SearchBlogPosts(query);

            if (searchResults == null || !searchResults.Any())
            {
                // Optionally, you can throw an exception, log, or return an empty list based on your requirements
                return Enumerable.Empty<Blog>();
            }

            return searchResults;
        }


        public object? GetAboutPageInfo()
        {
            // Implement logic to retrieve information for the about page
            
            return _blogRepository.GetAboutPageInfo();
        }

        public IEnumerable<Category> GetCategories()
        {
            // Assuming _blogRepository.GetCategories returns a collection of blog categories
            var categories = _blogRepository.GetCategories();

            if (categories == null || !categories.Any())
            {
                // Optionally, you can throw an exception, log, or return an empty list based on your requirements
                return Enumerable.Empty<Category>();
            }

            return categories;
        }


        public Blog CreateBlog(string blogTitle, string blogContent)
        {
            // Assuming _blogRepository.CreateBlog returns the created blog
            var newBlog = new Blog
            {
                BlogTitle = blogTitle,
                BlogContent = blogContent,
                BlogPublicationDate = DateTime.UtcNow,
                BlogCategories = new List<string>(), 
                BlogComments = new List<Comment>()   
            };

            _blogRepository.CreateBlog(newBlog);

            return newBlog;
        }

    }
}
