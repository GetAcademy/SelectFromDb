using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SelectFromDb.DbModels;

namespace SelectFromDb.Data
{
    public class SelectStudent
    {
        public Student GetStudent(SqlConnection connection, int id)
        {
            var list = new List<Student>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select Id, Navn From Student Where Id = @id";
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Student()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Navn = reader["Navn"].ToString()
                        });
                    }
                }
            }
            return list.FirstOrDefault();
        }
    }
}
