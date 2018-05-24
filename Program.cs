using System;
using System.Configuration;
using System.Data.SqlClient;
using SelectFromDb.Data;

namespace SelectFromDb
{
    class Program
    {
        static string ConnectionString => ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;

        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var selectStudent = new Select();
                var student = selectStudent.GetStudent(connection, 2);
                Console.WriteLine(student);
                Console.ReadKey();
            }
        }
    }
}
