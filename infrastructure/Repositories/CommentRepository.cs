
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CommentRepository
{
    private NpgsqlDataSource _dataSource;

    public CommentRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }
    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        string sql = $@"
SELECT Id, Name, Email, Text, PublicationDate, BlogPostId
FROM blog_schema.Comment
WHERE Id = @id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<Comment>(sql, new { id });
        }
    }

    
}