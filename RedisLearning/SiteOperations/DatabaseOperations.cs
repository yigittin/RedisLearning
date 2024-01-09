using System.Data;
using System.Data.SqlClient;

namespace RedisLearning.SiteOperations
{
    public class DatabaseOperations
    {
        public async Task<bool> InsertBulkRecords<T>(List<T> records,string tableName)
        {
            bool inserted = false;
            DataTable dt = GetTable(records);
            using(SqlConnection cn = GetConnection())
            {
                SqlBulkCopy bcp = new SqlBulkCopy(cn);
                bcp.DestinationTableName = tableName;
                cn.Open();
                await bcp.WriteToServerAsync(dt);
                inserted = true;
            }

            return inserted;
        }
        public async Task<bool> TruncateTable(string tableName)
        {
            using (SqlConnection cn = GetConnection())
            {
                try
                {
                    await cn.OpenAsync();
                    string sSQL = @$"TRUNCATE TABLE {tableName}";

                    SqlCommand command = new SqlCommand(sSQL, cn);

                    await command.ExecuteNonQueryAsync();
                    await cn.CloseAsync();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return true;
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection("Server=.; Database=Cocoin; Trusted_Connection=True;");
        }

        public DataTable GetTable<T>(List<T> records)
        {
            var type=typeof(T); 
            DataTable dt = new DataTable();
            for(int i=0; i<records.Count(); i++)
            {
                dt.Rows.Add(dt.NewRow());
            }

            foreach(var prop in type.GetProperties())
            {
                DataColumn c1 = new DataColumn(prop.Name);
                c1.DataType = prop.PropertyType;
                dt.Columns.Add(c1);

                int rowIndex = 0;

                foreach(var item in records)
                {
                    DataRow dr = dt.Rows[rowIndex++];
                    dr[prop.Name] = prop.GetValue(item);
                }
            }

            return dt;
        }
    }
}
