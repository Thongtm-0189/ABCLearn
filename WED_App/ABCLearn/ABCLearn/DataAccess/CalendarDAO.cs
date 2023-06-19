using ABCLearn.DataContext;
using ABCLearn.Models;
using NuGet.Packaging.Signing;
using System.Data;

namespace ABCLearn.DataAccess
{
	public class CalendarDAO : IServiceDAO<Calendar, CalendarDAO>
	{
		private List<Calendar> _calendar = new List<Calendar>();
		private static CalendarDAO insctance;
		public static CalendarDAO Instance
		{
			get
			{
				if (insctance == null)
				{
					insctance = new CalendarDAO();
				}
				return insctance;
			}
		}

		public List<Calendar> Calendars()
		{
			if (_calendar.Count == 0)
			{
				GetAll();
			}
			return _calendar;
		}
		private CalendarDAO() { }
		public bool AddNew(Calendar calendar)
		{
			bool result = false;
			string query = "INSERT INTO tblCalendar VALUES " +
				" ( @IDCourse , @start , @end ) ";
			result = ConectionData.ExecuteUpdate(query, new object[] { calendar.IDCourse, calendar.StartTime, calendar.EndTime });
			return result;
		}

		public void GetAll()
		{
			_calendar.Clear();
			if (_calendar.Count == 0)
			{
				string query = "SELECT * FROM tblCourse, tblCalendar WHERE tblCourse.IDCourse = tblCalendar.IDcourse ORDER BY tblCalendar.TimeStart ASC;";
				DataTable dataTable = ConectionData.ExecuteQuery(query);
				foreach (DataRow row in dataTable.Rows)
				{

					Calendar obj = new Calendar()
					{
						Id = Convert.ToInt32(row["IDCalendar"].ToString().Trim()),
						IDCourse = Convert.ToInt32(row["IDCourse"].ToString().Trim()),
						StartTime = TimeSpan.Parse(row["TimeStart"].ToString().Trim()),
						EndTime = TimeSpan.Parse(row["TimeEnd"].ToString().Trim()),
						NameCourse = row["Title"].ToString().Trim(),
					};

					_calendar.Add(obj);
				}
			}
		}

		public void SaveChange()
		{
			_calendar.Clear();
			GetAll();
		}

		public bool Update(Calendar calendar)
		{
			string query = "UPDATE tblCalendar SET TimeStart = @start , TimeEnd = @end WHERE IDCalendar = @IDCalendar ";
			return ConectionData.ExecuteUpdate(query, new object[] { calendar.StartTime.ToString(@"hh\:mm\:ss"), calendar.EndTime.ToString(@"hh\:mm\:ss"), calendar.Id });
		}

		public bool Delete(int index)
		{
			string query = "DELETE FROM tblCalendar WHERE IDCalendar = @IDCalendar ";
			return ConectionData.ExecuteUpdate(query, new object[] { index });
		}
	}
}
