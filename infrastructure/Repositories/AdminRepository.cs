using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class AdminRepository
{
    private NpgsqlDataSource _dataSource;

    public AdminRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }
    public async Task<Admin> GetAdminByIdAsync(int id)
    {
        string sql = $@"
SELECT Id, Username, PasswordHash, FailedLoginAttempts
FROM blog_schema.User
WHERE Id = @id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<Admin>(sql, new { id });
        }
    }

    
}