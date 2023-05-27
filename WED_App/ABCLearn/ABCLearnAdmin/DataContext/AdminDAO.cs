using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class AdminDAO
    {
        private static AdminDAO instance;
        public static AdminDAO Instence()
        {
            if (instance == null)
            {
                instance = new AdminDAO();
            }
            return instance;
        }

        public bool Login(AccountLogin acc)
        {
            string query = "SELECT * FROM tblAdmin WHERE Email = @Email AND Password = @Password";
            DataTable dataTable = null;
            dataTable = ConectionData.ExecuteQuery(query, new object[] { acc.Email, acc.Password });
            if (dataTable.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
