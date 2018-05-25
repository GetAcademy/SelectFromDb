﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using SelectFromDb.Data;
using SelectFromDb.DbModels;

namespace SelectFromDb
{
    public class Program
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;

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

            Console.WriteLine("Kjører GetOne():");
            Console.WriteLine(selectStudent.GetOne(connection, 2));
            Console.WriteLine("Kjører GetAll():");
            var students = selectStudent.GetAll(connection);
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("Kjører GetStudentsByNamePart(\"pen\"):");
            students = selectStudent.GetStudentsByNamePart(connection, "PEN");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("Kjører MetadataSelect.GetAllTables():");
            var metadataSelect = new MetadataSelect();
            var tables = metadataSelect.GetAllTables(connection);
            foreach (var table in tables)
            {
                Console.WriteLine(table);
            }
        }
    }
}
