
using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;
namespace service;

public class BlogService
{
    private readonly BlogRepository _blogRepository;
    
    public BlogService(BlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }
    public IEnumerable<BoxFeedQuery> GetBlogForFeed()
    {
        return _blogRepository.GetBlogForFeed();
    }

    public void DeleteBlog(int blogId)
    {
        throw new NotImplementedException();
    }

    public object? UpdateBlog(int blogId, object blogTitle, object blogContent)
    {
        throw new NotImplementedException();
    }

  

    public async Task<object?> GetBlogByIdAsync(int blogId)
    {
        throw new NotImplementedException();
    }


    public object? CreateComment(string commentDtoCommenterName, string commentDtoEmail, string commentDtoText)
    {
        throw new NotImplementedException();
    }

    public object? GetPostsByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public object? SearchBlogPosts(string query)
    {
        throw new NotImplementedException();
    }

    public object? GetAboutPageInfo()
    {
        throw new NotImplementedException();
    }

    public object? GetCategories()
    {
        throw new NotImplementedException();
    }

    public object? CreateBlog(object blogTitle, object blogContent)
    {
        throw new NotImplementedException();
    }
}