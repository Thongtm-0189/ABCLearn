using ABCLearn.DataAccess;
using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class AdminDAO : IUserDAO<AdminDAO>
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

        public bool AddNew(Profile obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int index)
        {
            throw new NotImplementedException();
        }

        public void GetAll()
        {
            throw new NotImplementedException();
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

        public void SaveChange()
        {
            throw new NotImplementedException();
        }

        public bool Update(Profile obj)
        {
            throw new NotImplementedException();
        }
    }
}
