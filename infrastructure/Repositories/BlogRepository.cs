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
        
        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            string sql = $@"
SELECT Id, Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage
FROM blog_schema.BlogPost
WHERE Id = @id;";

            using (var conn = _dataSource.OpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<BlogPost>(sql, new { id });
            }
        }


        
        
}
}
