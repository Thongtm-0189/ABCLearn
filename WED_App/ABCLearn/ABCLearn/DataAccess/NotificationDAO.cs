using ABCLearn.DataContext;
using ABCLearn.Models;
using System.Data;

namespace ABCLearn.DataAccess
{
    public class NotificationDAO
    {
        private static NotificationDAO _instance;
        private static List<Notification> _notification = new List<Notification>();

        public static NotificationDAO Instanse
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationDAO();
                }
                return _instance;
            }
        }
        public List<Notification> Transactions()
        {

            if (_notification.Count == 0)
            {
                getStransactions();
            }
            return _notification;

        }

        public void getStransactions()
        {
            _notification.Clear();
            string query = "SELECT * FROM tblNontification";
            DataTable tb = ConectionData.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Notification obj = new Notification()
                {
                    Id = Int32.Parse(row["IDContification"].ToString().Trim()),
                    Title = row["Title"].ToString().Trim(),
                    Detail = row["Detail"].ToString().Trim(),
                    StartDate = DateTime.Parse(row["StartTime"].ToString().Trim()),
                    EndDate = DateTime.Parse(row["EndTime"].ToString().Trim()),
                };
                _notification.Add(obj);
            }
            _notification.Reverse();
        }

        public bool insertTransaction(Notification notification)
        {
            DateTime nowUtc = DateTime.UtcNow;
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // Múi giờ của Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone);
            bool result = false;
            string query = "INSERT INTO tblNontification " +
                "\nVALUES ( @title , @detail , @start , @end )";
            result = ConectionData.ExecuteUpdate(query, new object[] { notification.Title, notification.Detail, notification.StartDate.ToString("yyyy-MM-dd HH:mm:ss"), notification.EndDate.ToString("yyyy-MM-dd HH:mm:ss") });
            return result;
        }
        public bool delete(int id)
        {
            bool result = false;
            string query = "DELETE tblNontification WHERE IDContification = @id ";
            result = ConectionData.ExecuteUpdate(query, new object[] { id });
            return result;
        }
        public void Update()
        {
            _notification.Clear();
            getStransactions();
        }
    }
}
