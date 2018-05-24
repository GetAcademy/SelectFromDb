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
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select Id, Navn From Student Where Id = @id";
                command.Parameters.AddWithValue("id", id);
                return ReadToList(command).FirstOrDefault();
            }
        }

        public List<Student> GetAllStudents(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select Id, Navn From Student";
                return ReadToList(command);
            }
        }

        private static List<Student> ReadToList(SqlCommand command)
        {
            var list = new List<Student>();
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
            return list;
        }
    }
}
