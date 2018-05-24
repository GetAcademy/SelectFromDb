using System;
using System.Configuration;
using System.Data.SqlClient;
using SelectFromDb.Data;
using SelectFromDb.DbModels;

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
                RunDemo(connection);
                Console.ReadKey();
            }
        }

        private static void RunDemo(SqlConnection connection)
        {
            var selectStudent = new SelectStudent();

            Console.WriteLine("Kjører GetStudent():");
            Console.WriteLine(selectStudent.GetStudent(connection, 2));
            Console.WriteLine("Kjører GetAllStudents():");
            var students = selectStudent.GetAllStudents(connection);
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }
}
