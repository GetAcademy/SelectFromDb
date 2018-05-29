using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SelectFromDb.Data
{
    public class MetadataSelect : Select<TableField>
    {
        public IEnumerable<string> GetAllTables(SqlConnection connection)
        {
            return GetMultiple(connection,
                    "SELECT TABLE_NAME as [Table], '' FieldName, '' FieldType FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = \'BASE TABLE\' AND TABLE_CATALOG =\'geitdemodb\'")
                .Select(tf=>tf.Table);
        }

        public List<TableField> GetAllTablesAndFields(SqlConnection connection)
        {
            return GetMultiple(connection,
                "SELECT Table_Name [Table], Column_Name FieldName, DATA_TYPE FieldType FROM INFORMATION_SCHEMA.COLUMNS");
        }
    }

    public class TableField
    {
        private string _fieldType;
        public string Table { get; set; }
        public string FieldName { get; set; }

        public string FieldType
        {
            get => _fieldType;
            set => _fieldType = 
                value == "date" ? "DateTime " :
                value == "nvarchar" ? "string" :
                value;
        }
    }
}
