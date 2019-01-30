using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using testschool.DB;

namespace testschool.Models
{
    public class SchoolInfo
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public SchoolInfo(AppDb db = null)
        {
            Db = db;
        }
        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `School` (id, `SchoolName`) VALUES (@id, @schoolName);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `School` SET `SchoolName` = @schoolName WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `School` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@schoolName",
                DbType = DbType.String,
                Value = SchoolName,
            });
        }

    }
}
