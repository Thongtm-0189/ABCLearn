using ABCLearn.DataContext;
using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataAccess
{
    public class TransactionDAO
    {
        private static TransactionDAO _instance;
        private static List<Transaction> _transactions = new List<Transaction>();

        public static TransactionDAO Instanse
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransactionDAO();
                }
                return _instance;
            }
        }
        public List<Transaction> Transactions()
        {

            if (_transactions.Count == 0)
            {
                getStransactions();
            }
            return _transactions;

        }

        public void getStransactions()
        {
            _transactions.Clear();
            string query = "SELECT * FROM tblTransactionHistory";
            DataTable tb = ConectionData.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Transaction obj = new Transaction()
                {
                    Id = Int32.Parse(row["IDHistory"].ToString().Trim()),
                    IdStudent = Int32.Parse(row["IDStudent"].ToString().Trim()),
                    CourseTitle = row["CourseTitle"].ToString().Trim(),
                    Price = Double.Parse(row["Price"].ToString().Trim()),
                    Created = DateTime.Parse(row["Date"].ToString().Trim()),
                    Method = row["Method"].ToString().Trim(),
                    OrderID = row["OrderID"].ToString().Trim(),
                };
                _transactions.Add(obj);
            }
            _transactions.Reverse();
        }

        public bool insertTransaction(int idStudent, string courseTitle, double price, string method, string idtrans)
        {
            DateTime nowUtc = DateTime.UtcNow;
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // Múi giờ của Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone);
            bool result = false;
            string query = "INSERT INTO tblTransactionHistory " +
                "\nVALUES ( @idStudent , @courseTItle , @price , @date , @method , @idtrans )";
            result = ConectionData.ExecuteUpdate(query, new object[] { idStudent, courseTitle, price, vietnamTime.ToString("yyyy-MM-dd HH:mm:ss"), method, idtrans });
            return result;
        }
        public void Update()
        {
            _transactions.Clear();
            getStransactions();
        }
    }
}
