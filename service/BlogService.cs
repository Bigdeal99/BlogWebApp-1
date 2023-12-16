using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task<IEnumerable<BlogPostFeedQuery>> GetBlogPostForFeedAsync()
        {
            return _blogRepository.GetBlogPostForFeedAsync();
        }
       
        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _blogRepository.GetBlogPostByIdAsync(id);
        }
       
    }
}
