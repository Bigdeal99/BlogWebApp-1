using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;
using System;
using System.Collections.Generic;

namespace infrastructure.Repositories
{
    public class BlogRepository
    {
        private NpgsqlDataSource _dataSource;

        public BlogRepository(NpgsqlDataSource datasource)
        {
            _dataSource = datasource;
        }

        public IEnumerable<BlogFeedQuery> GetBlogsForFeed()
        {
            string sql = $@"
SELECT blogid as {nameof(BlogFeedQuery.BlogId)},
       title as {nameof(BlogFeedQuery.Title)},
       summary as {nameof(BlogFeedQuery.Summary)},
       publicationdate as {nameof(BlogFeedQuery.PublicationDate)}
FROM your_blog_table_name;
";
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.Query<BlogFeedQuery>(sql);
            }
        }

        public Blog GetBlogById(int blogId)
        {
            string sql = $@"
SELECT blogid as {nameof(Blog.BlogId)},
       title as {nameof(Blog.Title)},
       content as {nameof(Blog.Content)},
       publicationdate as {nameof(Blog.PublicationDate)}
FROM your_blog_table_name
WHERE blogid = @blogId;
";

            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirstOrDefault<Blog>(sql, new { blogId });
            }
        }

        public IEnumerable<string> GetCategories()
        {
            string sql = "SELECT DISTINCT category FROM your_category_table_name;";
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.Query<string>(sql);
            }
        }

        public Blog CreateBlog(string title, string content, DateTime publicationDate, List<string> categories)
        {
            // Add logic to insert the blog into the database and return the created blog
        }

        public bool UpdateBlog(int blogId, string title, string content, List<string> categories)
        {
            // Add logic to update the blog in the database and return true if successful
        }

        public bool DeleteBlog(int blogId)
        {
            // Add logic to delete the blog from the database and return true if successful
        }

        public IEnumerable<Comment> GetCommentsForBlog(int blogId)
        {
            // Add logic to retrieve comments for a specific blog from the database
        }

        public Comment AddCommentToBlog(int blogId, string commenterName, string email, string text)
        {
            // Add logic to insert a comment for a specific blog into the database and return the created comment
        }

        // Other methods for additional functionalities can be added here
    }
}
