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

        public IEnumerable<BoxFeedQuery> GetBlogForFeed()
        {
            const string query = "SELECT * FROM YourBoxFeedQueryTable";
            return _dbConnection.Query<BoxFeedQuery>(query);
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

        public void CreateComment(Comment newComment)
        {
            const string query = "INSERT INTO Comments (CommenterName, Email, Text, PublicationDate) VALUES (@CommenterName, @Email, @Text, @PublicationDate)";
            _dbConnection.Execute(query, newComment);
        }

        public IEnumerable<Blog> GetPostsByCategory(int categoryId)
        {
            const string query = "SELECT * FROM Blogs WHERE CategoryId = @CategoryId";
            return _dbConnection.Query<Blog>(query, new { CategoryId = categoryId });
        }

        public IEnumerable<Blog> SearchBlogPosts(string query)
        {
            string searchQuery = $"SELECT * FROM Blogs WHERE BlogTitle LIKE '%{query}%' OR BlogContent LIKE '%{query}%'";
            return _dbConnection.Query<Blog>(searchQuery);
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
