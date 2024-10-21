using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Api.DAL
{
    public class DataBaseHelper
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["WebApp"].ConnectionString;
        private string connectionString = "Data Source=SETU37\\SQLEXPRESS;Initial Catalog=All_Projects;Integrated Security=True;Encrypt=False";
        public DataSet GetDataSet(string storedProcName, Dictionary<string, object> parameters = null)// SqlParameter[] parameters = null)
        {
            DataSet result = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {

                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                        //cmd.Parameters.AddRange(parameters);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
            }
            return result;
        }

    }
}