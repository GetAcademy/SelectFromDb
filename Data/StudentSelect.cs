using System.Collections.Generic;
using System.Data.SqlClient;
using SelectFromDb.DbModels;

namespace SelectFromDb.Data
{
    public class StudentSelect : Select<Student>
    {
        public List<Student> GetStudentsByNamePart(SqlConnection connection, string namePart)
        {
            return GetMultipleWhere(connection, $"LOWER(Navn) like '%{namePart.ToLower()}%'");
        }
    }
}
