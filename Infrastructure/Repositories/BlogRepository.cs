using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class BlogRepository
{
    private NpgsqlDataSource _dataSource;

    public BlogRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }

    public IEnumerable<BoxFeedQuery> GetBoxForFeed()
    {
        string sql = $@"
SELECT boxid as {nameof(BoxFeedQuery.BoxId)},
       boxname as {nameof(BoxFeedQuery.BoxName)},
       boxweight as {nameof(BoxFeedQuery.BoxWeight)}
FROM box_factory.boxes;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeedQuery>(sql);
        }
    }


    public Blog UpdateBox(int boxId, string boxName, double boxWeight)
    {
        var sql = $@"
UPDATE box_factory.boxes SET boxname = @boxName,boxweight = @boxWeight
WHERE boxid = @boxId
RETURNING boxid as {nameof(Blog.BlogId)},
       boxname as {nameof(Blog.BlogTitle)},
       boxweight as {nameof(BoxFeedQuery.BoxWeight)}
";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Blog>(sql, new { boxId, boxName, boxWeight });
        }
    }

    public Blog CreateBox(string boxName, double boxWeight)
    {
        var sql = $@"
INSERT INTO box_factory.boxes (boxname, boxweight) 
VALUES (@boxName, @boxWeight)
RETURNING boxid as {nameof(Blog.BlogId)},
       boxname as {nameof(Blog.BlogTitle)},
       boxweight as {nameof(BoxFeedQuery.BoxWeight)}
        
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Blog>(sql, new { boxName, boxWeight });
        }
    }

    public bool DeleteBox(int boxId)
    {
        var sql = @"DELETE FROM box_factory.boxes WHERE boxId = @boxId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { boxId }) == 1;
        }
    }

    public bool DoesBoxtWithNameExist(string boxName)
    {
        var sql = @"SELECT COUNT(*) FROM box_factory.boxes WHERE boxname = @boxName;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { boxName }) == 1;
        }
    }

    public async Task<Blog> GetBoxByIdAsync(int boxId)
    {
        string sql = $@"
SELECT boxid as {nameof(Blog.BlogId)},
       boxname as {nameof(Blog.BlogTitle)},
       boxweight as {nameof(Blog.BoxWeight)}
FROM box_factory.boxes
WHERE boxid = @boxId;
";

        using (var conn = _dataSource.OpenConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<Blog>(sql, new { boxId });
        }
    }
}