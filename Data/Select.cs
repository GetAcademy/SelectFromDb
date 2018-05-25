using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SelectFromDb.Data
{
    public class Select<TDbModel> where TDbModel : new()
    {
        private readonly PropertyInfo[] _properties;
        private readonly Type _dbModelType;

        public Select()
        {
            _dbModelType = typeof(TDbModel);
            _properties = _dbModelType.GetProperties();
        }

        public string GetSelectAllSql()
        {
            return CreateSelectStringBuilder().ToString();
        }

        public string GetSelectByIdSql()
        {
            var sql = CreateSelectStringBuilder();
            sql.Append(" WHERE Id = @id ");
            return sql.ToString();
        }

        protected StringBuilder CreateSelectStringBuilder()
        {
            var sql = new StringBuilder();
            foreach (var property in _properties)
            {
                if (sql.Length > 0) sql.Append(",");
                sql.Append(property.Name);
            }
            sql.Insert(0, "SELECT ");
            sql.Append(" FROM ");
            sql.Append(_dbModelType.Name);
            sql.Append(" ");
            return sql;
        }

        public TDbModel GetOne(SqlConnection connection, int id)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = GetSelectByIdSql();
                command.Parameters.AddWithValue("id", id);
                return ReadToList(command).FirstOrDefault();
            }
        }

        public List<TDbModel> GetMultipleWhere(SqlConnection connection, string whereSql)
        {
            var builder = CreateSelectStringBuilder();
            builder.Append(" WHERE ");
            builder.Append(whereSql);
            return GetMultiple(connection, builder.ToString());
        }

        public List<TDbModel> GetAll(SqlConnection connection)
        {
            return GetMultiple(connection, GetSelectAllSql());
        }

        public List<TDbModel> GetMultiple(SqlConnection connection, string sql)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                return ReadToList(command);
            }
        }

        private List<TDbModel> ReadToList(SqlCommand command)
        {
            var list = new List<TDbModel>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var rowEntity = new TDbModel();
                    ReadAllFields(reader, rowEntity);
                    list.Add(rowEntity);
                }
            }
            return list;
        }

        private void ReadAllFields(SqlDataReader reader, TDbModel rowEntity)
        {
            foreach (var property in _properties)
            {
                property.SetValue(rowEntity, reader[property.Name]);
            }
        }
    }
}
