using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProgettoPalestra.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public async Task<DataSet> Query(string query)
        {
            using (var conn = new SqlConnection("Server = DESKTOP-PA76MHC; Database = Courses; Trusted_Connection = True; "))
            {
               await conn.OpenAsync();
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader =await cmd.ExecuteReaderAsync())
                    {
                        var dataset = new DataSet();
                        dataset.EnforceConstraints = false;
                        do
                        {
                            var dataTable = new DataTable();
                            dataset.Tables.Add(dataTable);
                            dataTable.Load(reader);
                        } while (!reader.IsClosed);

                        return dataset;
                    }

                }

            }


            throw new NotImplementedException();
        }
    }
}
