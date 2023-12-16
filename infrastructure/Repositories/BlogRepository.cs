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
        private NpgsqlDataSource _dataSource;

        public BlogRepository(NpgsqlDataSource datasource)
        {
            _dataSource = datasource;
        }
        
        
        public async Task<IEnumerable<BlogPostFeedQuery>> GetBlogPostForFeedAsync()
        {
            string sql = $@"
SELECT Id as {nameof(BlogPostFeedQuery.Id)},
       Title as {nameof(BlogPostFeedQuery.Title)},
       Summary as {nameof(BlogPostFeedQuery.Summary)},
       Content as {nameof(BlogPostFeedQuery.Content)},
       PublicationDate as {nameof(BlogPostFeedQuery.PublicationDate)},
       CategoryId as {nameof(BlogPostFeedQuery.CategoryId)},
       FeaturedImage as {nameof(BlogPostFeedQuery.FeaturedImage)}
FROM blog_schema.BlogPost;
";
            using (var conn = _dataSource.OpenConnection())
            {
                return await conn.QueryAsync<BlogPostFeedQuery>(sql);
            }
        }

        
        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            string sql = $@"
SELECT Id as {nameof(BlogPost.Id)},
       Title as {nameof(BlogPost.Title)},
              Summary as {nameof(BlogPost.Summary)},
       Content as {nameof(BlogPost.Content)},
       PublicationDate as {nameof(BlogPost.PublicationDate)},
       CategoryId as {nameof(BlogPost.CategoryId)},
       FeaturedImage as {nameof(BlogPost.FeaturedImage)},

FROM blog_schema.BlogPost
WHERE Id = @Id;
";

            using (var conn = _dataSource.OpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<BlogPost>(sql, new { id });
            }
        }


        
    }
}
