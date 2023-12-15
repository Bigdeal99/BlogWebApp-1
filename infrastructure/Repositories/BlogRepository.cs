using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using Npgsql;


namespace infrastructure.Repositories
{
    public class BlogRepository
    {
        private readonly IDbConnection _dbConnection;

        public BlogRepository(string connectionString)
        {
            _dbConnection = new NpgsqlConnection(connectionString);
        }

        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            // Implement actual query to get admin by username
            return null;
        }

        public async Task<bool> UpdateAdminAsync(Admin admin)
        {
            // Implement actual query to update admin
            return false;
        }

        public async Task<IEnumerable<Blog>> GetBlogsForFeedAsync()
        {
            var query = @"
                SELECT b.*, c.* 
                FROM blog_schema." + nameof(Blog) + @" b 
                LEFT JOIN blog_schema.Comments c ON b." + nameof(Blog.BlogId) + @" = c." + nameof(Comment.BlogId);
            
            var blogDictionary = new Dictionary<int, Blog>();

            var blogs = await _dbConnection.QueryAsync<Blog, Comment, Blog>(
                query,
                (blog, comment) =>
                {
                    if (!blogDictionary.TryGetValue(blog.BlogId, out var currentBlog))
                    {
                        currentBlog = blog;
                        blogDictionary.Add(currentBlog.BlogId, currentBlog);
                    }

                    if (comment != null)
                    {
                        currentBlog.Comments.Add(comment);
                    }

                    return currentBlog;
                },
                splitOn: "Id");

            return blogDictionary.Values;
        }
        

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            var query = "DELETE FROM blog_schema.Blogs WHERE BlogId = @BlogId";
            var result = await _dbConnection.ExecuteAsync(query, new { BlogId = blogId });
            return result > 0;
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            var query = @"SELECT * FROM blog_schema." + nameof(Blog) + @" WHERE " + nameof(Blog.BlogId) + @" = @BlogId";
            return await _dbConnection.QuerySingleOrDefaultAsync<Blog>(query, new { BlogId = blogId });
        }

        public async Task<bool> UpdateBlogAsync(Blog updatedBlog)
        {
            var query = @"
        UPDATE blog_schema.Blogs 
        SET BlogTitle = @BlogTitle, BlogContent = @BlogContent, CategoryId = @CategoryId
        WHERE BlogId = @BlogId";
            var result = await _dbConnection.ExecuteAsync(query, updatedBlog);
            return result > 0;
        }


        public async Task<int> CreateCommentAsync(Comment newComment, int blogId)
        {
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var commentQuery = @"
            INSERT INTO blog_schema.Comments (CommenterName, Email, Text, PublicationDate) 
            VALUES (@CommenterName, @Email, @Text, @PublicationDate)
            RETURNING CommentId";
                var commentId = await _dbConnection.ExecuteScalarAsync<int>(commentQuery, newComment, transaction: transaction);

                var blogsCommentsQuery = @"
            INSERT INTO blog_schema.BlogsComments (BlogId, CommentId) 
            VALUES (@BlogId, @CommentId)";
                await _dbConnection.ExecuteAsync(blogsCommentsQuery, new { BlogId = blogId, CommentId = commentId }, transaction: transaction);

                transaction.Commit();

                return commentId;
            }
        }

        public async Task<IEnumerable<Blog>> GetPostsByCategoryAsync(int categoryId)
        {
            var query = @"SELECT * FROM blog_schema." + nameof(Blog) + @" WHERE CategoryId = @CategoryId";
            return await _dbConnection.QueryAsync<Blog>(query, new { CategoryId = categoryId });
        }

        public async Task<IEnumerable<Blog>> SearchBlogPostsAsync(string searchTerm)
        {
            var query = @"
                SELECT * FROM blog_schema." + nameof(Blog) + @"
                WHERE " + nameof(Blog.BlogTitle) + @" ILIKE @SearchTerm OR " + nameof(Blog.BlogContent) + @" ILIKE @SearchTerm";
            return await _dbConnection.QueryAsync<Blog>(query, new { SearchTerm = $"%{searchTerm}%" });
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var query = @"SELECT * FROM blog_schema." + nameof(Category);
            return await _dbConnection.QueryAsync<Category>(query);
        }

        public async Task<int> CreateBlogAsync(Blog newBlog)
        {
            var query = @"
                INSERT INTO blog_schema.Blogs (BlogTitle, BlogContent, BlogPublicationDate) 
                VALUES (@BlogTitle, @BlogContent, @BlogPublicationDate)
                RETURNING " + nameof(Blog.BlogId);
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, newBlog);
            return id;
        }

        public async Task<object> GetAboutPageInfoAsync()
        {
            // Implement actual query to get about page information
            throw new NotImplementedException();
        }
    }
}
