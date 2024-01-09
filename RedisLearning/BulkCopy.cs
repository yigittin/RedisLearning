using RedisLearning.SiteOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    public class BulkCopy
    {
        public async Task<bool> Copy<T>(List<T> records,string tableName)
        {
            return await new DatabaseOperations().InsertBulkRecords(records, tableName);
        }
    }
}
