using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ABCLearn.DataContext
{
    public class ConectionData
    {
        //static string connectionString = "Data Source=SQL8001.site4now.net,1433;Initial Catalog=db_a99c55_thong123;User Id=db_a99c55_thong123_admin;Password=thongngu0189";
        static string connectionString = "Data Source=THONG-LAPTOP;Initial Catalog=ABCLearn;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";
        public static DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    if (parameter != null)
                    {
                        string[] temp = query.Split(' ');
                        List<string> listParameter = new List<string>();

                        foreach (string item in temp)
                        {
                            if (item.StartsWith("@"))
                            {
                                listParameter.Add(item.ToString());
                            }
                        }

                        for (int i = 0; i < parameter.Length; i++)
                        {
                            sqlCommand.Parameters.AddWithValue(listParameter[i], parameter[i]);
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {

                        adapter.Fill(data);
                    }
                }

                connection.Close();
            }
            return data;
        }

        public static bool ExecuteUpdate(string query, object[] parameter = null)
        {
            bool result = false;
            int value = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    if (parameter != null)
                    {
                        string[] temp = query.Split(' ');
                        List<string> listParameter = new List<string>();

                        foreach (string item in temp)
                        {
                            if (item.StartsWith("@"))
                            {
                                listParameter.Add(item.ToString());
                            }
                        }

                        for (int i = 0; i < parameter.Length; i++)
                        {
                            sqlCommand.Parameters.AddWithValue(listParameter[i], parameter[i]);
                        }
                    }
                    value = sqlCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
            return result = value > 0 ? true : false;
        }

    }
}
