using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SelectFromDb.Data
{
    public class MetadataSelect : Select<Table>
    {
        public IEnumerable<string> GetAllTables(SqlConnection connection)
        {
            return GetMultiple(connection,
                "SELECT TABLE_NAME as Name FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = \'BASE TABLE\' AND TABLE_CATALOG =\'geitdemodb\'")
                .Select(t=>t.Name);
        }
    }

    public class Table
    {
        public string Name { get; set; }
    }
}
