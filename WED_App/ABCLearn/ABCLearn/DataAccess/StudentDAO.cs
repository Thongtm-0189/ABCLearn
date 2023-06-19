using ABCLearn.DataAccess;
using ABCLearn.Models;
using ABCLearn.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace ABCLearn.DataContext
{
	public class StudentDAO : IUserDAO<StudentDAO>
	{
		private static StudentDAO instance;
		public static StudentDAO Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new StudentDAO();
				}
				return instance;
			}
		}

		private List<Student> _student = new List<Student>();
		public List<Student> Students()
		{
			if (_student.Count == 0)
			{
				GetAll();
			}
			return _student;
		}

		public bool login(AccountLogin acc)
		{
			string query = "SELECT * FROM tblStudent WHERE Email = @Email AND Password = @pass";
			DataTable tb = ConectionData.ExecuteQuery(query, new object[] { acc.Email, acc.Password });
			foreach (DataRow row in tb.Rows)
			{
				UserLogin obj = new UserLogin()
				{
					Id = Convert.ToInt32(row["IDStudent"].ToString()),
					FirstName = row["FirstName"].ToString().Trim(),
					LastName = row["LastName"].ToString().Trim(),
					RoleID = row["RoleID"].ToString().Trim(),
					Password = row["Password"].ToString().Trim(),
					Email = row["Email"].ToString().Trim(),
					Phone = row["Phone"].ToString().Trim(),
					Avatar = row["Avatar"].ToString().Trim(),
					Courses = GetCourses(Convert.ToInt32(row["IDStudent"].ToString().Trim())),
					Gander = row["Gander"].ToString().Trim(),
					DOB = DateTime.Parse(row["DOB"].ToString().Trim()),
					IsConfirmEmail = Boolean.Parse(row["ConfirmEmail"].ToString().Trim()),
					DateCreated = DateTime.Parse(row["DateCreate"].ToString().Trim())
				};
				UserLogin.Instance = obj;
			}
			if (tb.Rows.Count == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public void GetAll()
		{
			_student.Clear();
			string query = "SELECT * FROM tblStudent";
			DataTable tb = ConectionData.ExecuteQuery(query);
			foreach (DataRow row in tb.Rows)
			{
				Student obj = new Student()
				{
					Id = Convert.ToInt32(row["IDStudent"].ToString().Trim()),
					FirstName = row["FirstName"].ToString().Trim(),
					LastName = row["LastName"].ToString().Trim(),
					RoleID = row["RoleID"].ToString().Trim(),
					Password = row["Password"].ToString().Trim(),
					Email = row["Email"].ToString().Trim(),
					Phone = row["Phone"].ToString().Trim(),
					Avatar = row["Avatar"].ToString().Trim(),
					Courses = GetCourses(Convert.ToInt32(row["IDStudent"].ToString().Trim())),
					Gander = row["Gander"].ToString().Trim(),
					DOB = DateTime.Parse(row["DOB"].ToString().Trim()),
					IsConfirmEmail = Boolean.Parse(row["ConfirmEmail"].ToString().Trim()),
					DateCreated = DateTime.Parse(row["DateCreate"].ToString().Trim())
				};
				_student.Add(obj);
			}
			_student.Reverse();
		}
		public Student Read(int id)
		{
			string query = "SELECT * FROM tblStudent WHERE IDStudent = @id ";
			DataTable tb = ConectionData.ExecuteQuery(query, new object[] { id });
			foreach (DataRow row in tb.Rows)
			{
				Student obj = new Student()
				{
					Id = Convert.ToInt32(row["IDStudent"].ToString().Trim()),
					FirstName = row["FirstName"].ToString().Trim(),
					LastName = row["LastName"].ToString().Trim(),
					RoleID = row["RoleID"].ToString().Trim(),
					Password = row["Password"].ToString().Trim(),
					Email = row["Email"].ToString().Trim(),
					Phone = row["Phone"].ToString().Trim(),
					Avatar = row["Avatar"].ToString().Trim(),
					Courses = GetCourses(Convert.ToInt32(row["IDStudent"].ToString().Trim())),
					Gander = row["Gander"].ToString().Trim(),
					DOB = DateTime.Parse(row["DOB"].ToString().Trim()),
					IsConfirmEmail = Boolean.Parse(row["ConfirmEmail"].ToString().Trim()),
					DateCreated = DateTime.Parse(row["DateCreate"].ToString().Trim())
				};
				if (obj != null)
					return obj;
			}
			return null;
		}
		public List<Course> GetCourses(int id)
		{
			List<Course> _courseStudents = new List<Course>();
			string query = "SELECT * FROM tblCourse , tblCourseOfStudent WHERE tblCourse.IDCourse = tblCourseOfStudent.IDCourse AND IDStudent = @id";
			DataTable dataTable = ConectionData.ExecuteQuery(query, new object[] { id });
			foreach (DataRow row in dataTable.Rows)
			{
				Course obj = new Course()
				{
					Id = Convert.ToInt32(row["IDCourse"].ToString()),
					Lecturer = LecturerDAO.Instance.Lecturers().FirstOrDefault(x => x.Id == Convert.ToInt32(row["IDLecturer"])),
					Title = row["Title"].ToString().Trim(),
					Detail = row["Detail"].ToString().Trim(),
					Status = bool.Parse(row["Status"].ToString().Trim()),
					Price = float.Parse(row["Price"].ToString().Trim()),
					Sale = float.Parse(row["Sale"].ToString().Trim())
				};
				_courseStudents.Add(obj);
			}
			return _courseStudents;
		}
		public bool Update(Profile pro)
		{
			int ID = UserLogin.Instance.Id;
			string query = "UPDATE tblStudent " +
				" \nSET FirstName = @FirstName , LastName = @LastName , Password = @Password , Phone = @Phone , Email = @Email , Gander = @Gander , DOB = @DOB  " +
				" \nWHERE IDStudent = @IDStudent";
			bool result = ConectionData.ExecuteUpdate(query, new object[] { pro.FirstName, pro.LastName, pro.Password, pro.Phone, pro.Email, pro.Gander, pro.DOB.ToString("yyyy-MM-dd"), pro.Id });
			return result;
		}

		public bool uploadAvatar(string name, int id)
		{
			string query = $"UPDATE tblStudent " +
			   " \nSET Avatar = @avatar" +
			   $" \nWHERE IDStudent = @ID";
			bool result = ConectionData.ExecuteUpdate(query, new object[] { name, id });
			return result;
		}

		public bool Delete(int id)
		{
			string query = "DELETE FROM tblStudent WHERE IDStudent = @idStudent ";
			bool result = ConectionData.ExecuteUpdate(query, new object[] { id });
			return result;
		}
		public bool AddNew(Profile pro)
		{
			DateTime vietnamTime = CurrentDateTime.GetcurrentDateTime;
			bool result = false;
			string query = "INSERT INTO tblStudent " +
				"\nVALUES ( @FirstName , @Lastname , 'Student' , @Password , @emil , @phone , 0 , @datecreate , @gander , @DOB , NULL)";
			result = ConectionData.ExecuteUpdate(query, new object[] { pro.FirstName, pro.LastName, pro.Password, pro.Email, pro.Phone, vietnamTime.ToString("yyyy-MM-dd HH:mm:ss"), pro.Gander, pro.DOB.ToString("yyyy-MM-dd") });
			return result;
		}
		public bool confirmAccountUser(int id)
		{
			bool result = false;
			DateTime vietnametime = CurrentDateTime.GetcurrentDateTime;
			string query = "UPDATE tblStudent SET ConfirmEmail = 1, DateCreate = @Date WHERE IDStudent = @id ";
			result = ConectionData.ExecuteUpdate(query, new object[] { vietnametime.ToString("yyyy-MM-dd HH:mm:ss"), id });
			return result;
		}
		public bool resetPassword(Profile pro)
		{
			bool result = false;
			string query = "UPDATE tblStudent SET Password = @pass WHERE Email = @Email ";
			result = ConectionData.ExecuteUpdate(query, new object[] { pro.Password, pro.Email });
			return result;
		}
		public void SaveChange()
		{
			_student.Clear();
			GetAll();
		}
	}
}
