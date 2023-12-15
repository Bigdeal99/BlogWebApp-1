
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
    private readonly IDbConnection _dbConnection;

    public CommentRepository(string connectionString)
    {
        _dbConnection = new NpgsqlConnection(connectionString);
    }
    
}