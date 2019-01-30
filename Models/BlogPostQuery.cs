using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using testschool.DB;

namespace testschool.Models
{
    public class BlogPostQuery
    {
        public readonly AppDb Db;
        public BlogPostQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<SchoolInfo> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `BlogPost` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<SchoolInfo>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `schoolName` FROM `School` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        //public async Task DeleteAllAsync()
        //{
        //    var txn = await Db.Connection.BeginTransactionAsync();
        //    try
        //    {
        //        var cmd = Db.Connection.CreateCommand();
        //        cmd.CommandText = @"DELETE FROM `BlogPost`";
        //        await cmd.ExecuteNonQueryAsync();
        //        await txn.CommitAsync();
        //    }
        //    catch
        //    {
        //        await txn.RollbackAsync();
        //        throw;
        //    }
        //}

        private async Task<List<SchoolInfo>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<SchoolInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new SchoolInfo(Db)
                    {
                        Id = await reader.GetFieldValueAsync<int>(0),
                        SchoolName = await reader.GetFieldValueAsync<string>(1)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

    }
}
