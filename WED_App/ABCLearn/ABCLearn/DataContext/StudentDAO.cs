using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataContext
{
    public class StudentDAO
    {
        private static StudentDAO instance;
        public static StudentDAO Instance => instance ?? (instance = new StudentDAO());

        public DataTable login(AccountLogin acc)
        {
            string query = "";
            return ConectionData.ExecuteQuery(query, new object[] { acc.Email, acc.Password });
        }
    }
}
