using ABCLearn.DataContext;
using ABCLearn.Models;
using ABCLearn.Services;
using System.Data;

namespace ABCLearn.DataAccess
{
	public class TransactionDAO : IServiceDAO<Transaction, TransactionDAO>
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
				GetAll();
			}
			return _transactions;

		}

		public void GetAll()
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

		public bool AddNew(Transaction trans)
		{
			DateTime vietnamTime = CurrentDateTime.GetcurrentDateTime;
			bool result = false;
			string query = "INSERT INTO tblTransactionHistory " +
				"\nVALUES ( @idStudent , @courseTItle , @price , @date , @method , @idtrans )";
			result = ConectionData.ExecuteUpdate(query, new object[] { trans.IdStudent, trans.CourseTitle, trans.Price, vietnamTime.ToString("yyyy-MM-dd HH:mm:ss"), trans.Method, trans.OrderID });
			return result;
		}
		public void SaveChange()
		{
			_transactions.Clear();
			GetAll();
		}

		public bool Update(Transaction obj)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int obj)
		{
			throw new NotImplementedException();
		}
	}
}
