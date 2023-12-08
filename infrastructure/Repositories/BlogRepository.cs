using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels; 
using infrastructure.QueryModels;
using Npgsql;


namespace infrastructure.Repositories
{
    public class BlogRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlogRepository(NpgsqlDataSource dataSource)
        {
            _dbConnection = new NpgsqlConnection(dataSource.ConnectionString);
        }
        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            return null;
        }
        public async Task UpdateAdminAsync(Admin admin)
        {
            // Implement logic to update admin in the database
        }
        public IEnumerable<BlogFeedQuery> GetBlogForFeed()
        {
            const string query = "SELECT * " +
                                 "FROM blog_schema.blogs b " +
                                 "JOIN blog_schema.blogscomments bc " +
                                 "ON b.blogid = bc.blogid " +
                                 "JOIN blog_schema.comments c " +
                                 "ON bc.commentid = c.commentid";
            
            var result = _dbConnection.Query<BlogFeedQuery>(query);
            return result;
        }

        public bool DeleteBlog(int blogId)
        {
            const string query = "DELETE FROM Blogs WHERE BlogId = @BlogId";

            try
            {
                _dbConnection.Execute(query, new { BlogId = blogId });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete blog with ID {blogId}. {ex.Message}");
            }

            return false;
        }


        public Blog GetBlogById(int blogId)
        {
            const string query = "SELECT * FROM Blogs WHERE BlogId = @BlogId";
            return _dbConnection.QuerySingleOrDefault<Blog>(query, new { BlogId = blogId });
        }
        

        public void UpdateBlog(Blog updatedBlog)
        {
            const string query = "UPDATE Blogs SET BlogTitle = @BlogTitle, BlogContent = @BlogContent WHERE BlogId = @BlogId";
            _dbConnection.Execute(query, updatedBlog);
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            const string query = "SELECT * FROM Blogs WHERE BlogId = @BlogId";
            return await _dbConnection.QuerySingleOrDefaultAsync<Blog>(query, new { BlogId = blogId });
        }

        public async Task CreateCommentAsync(Comment newComment)
        {
            const string query = "INSERT INTO Comments (CommenterName, Email, Text, PublicationDate) VALUES (@CommenterName, @Email, @Text, @PublicationDate)";
            await _dbConnection.ExecuteAsync(query, newComment);
        }


        public IEnumerable<Blog> GetPostsByCategory(int categoryId)
        {
            const string query = "SELECT * FROM Blogs WHERE CategoryId = @CategoryId";
            return _dbConnection.Query<Blog>(query, new { CategoryId = categoryId });
        }

        public IEnumerable<Blog> SearchBlogPosts(string query)
            
        {
            string searchQuery = "SELECT * FROM Blogs WHERE BlogTitle LIKE @Query OR BlogContent LIKE @Query";
            return _dbConnection.Query<Blog>(searchQuery, new { Query = $"%{query}%" });

        }

        public IEnumerable<Category> GetCategories()
        {
            const string query = "SELECT * FROM Categories";
            return _dbConnection.Query<Category>(query);
        }

        public void CreateBlog(Blog newBlog)
        {
            const string query = "INSERT INTO Blogs (BlogTitle, BlogContent, BlogPublicationDate) VALUES (@BlogTitle, @BlogContent, @BlogPublicationDate)";
            _dbConnection.Execute(query, newBlog);
        }

        public object? GetAboutPageInfo()
        {
            // Your implementation for getting about page info, adjust as needed
            throw new NotImplementedException();
        }
    }

    
}
