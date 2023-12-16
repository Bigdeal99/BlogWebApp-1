using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CategoryRepository
{
    private NpgsqlDataSource _dataSource;

    public CategoryRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        string sql = $@"
SELECT Id, Name, Description
FROM blog_schema.Category
WHERE Id = @id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<Category>(sql, new { id });
        }
    }

    
}