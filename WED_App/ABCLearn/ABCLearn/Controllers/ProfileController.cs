﻿using ABCLearn.DataContext;
using ABCLearn.Extend;
using ABCLearn.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABCLearn.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IWebHostEnvironment _environment;

		public ProfileController(IWebHostEnvironment environment)
		{
			_environment = environment;
		}
		public IActionResult Index()
		{
			if (HttpContext.Session.GetObject<UserLogin>("User") == null)
			{
				UserLogin.Instance = null;
			}
			SessionUser();
			return View();
		}
		public IActionResult EditInformation(Profile pro)
		{
			if (pro != null)
			{
				if (pro.Role.Trim().Equals("Student"))
				{
					bool checkEdited = StudentDAO.Instance.Update(pro);
					if (checkEdited)
					{
						int idUser = pro.Id;
						pro.Role = "Student";
						UserLogin.updateIfor(idUser, "Student");

						StudentDAO.Instance.SaveChange();
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					bool checkEdited = LecturerDAO.Instance.Update(pro);
					if (checkEdited)
					{
						pro.Role = "Lecturer";
						int idUserLgin = pro.Id;
						UserLogin.updateIfor(idUserLgin, "Lecturer");

						LecturerDAO.Instance.SaveChange();
					}
				}
				UserLogin.Instance.Islogin = true;
				UserLogin.Instance.Courses.ForEach(course => course.Calendars = CourseDAO.Instance.getCalendar(course.Id));
				HttpContext.Session.Clear();
				HttpContext.Session.SetObject("User", UserLogin.Instance);
				SessionUser();
				return RedirectToAction("Profile", "Home");
			}
			return RedirectToAction("Index", "Home");
		}
		public IActionResult UploadAvatar(IFormFile image)
		{
			var user = HttpContext.Session.GetObject<UserLogin>("User");
			if (user == null)
			{
				UserLogin.Instance = null;
			}

			renderData();
			SessionUser();
			var roleID = user.RoleID;
			var id = user.Id;
			if (image != null && image.Length > 0)
			{
				ViewBag.fail = "not fail";
				var fileName = $"{roleID}-{id}{Path.GetExtension(image.FileName)}";
				//var fileName = Path.GetFileName(image.FileName);
				var path = Path.Combine(_environment.WebRootPath, $"img\\Avatar\\{user.RoleID}", fileName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					image.CopyTo(fileStream);
				}
				if (user.RoleID == "Student")
				{
					StudentDAO.Instance.uploadAvatar(fileName, user.Id);
				}
				else
				{
					LecturerDAO.Instance.uploadAvatar(fileName, user.Id);
				}
				user.Avatar = fileName;
				HttpContext.Session.SetObject("User", user);
			}
			else
			{
				ViewBag.fail = "fail";
			}
			SessionUser();
			return RedirectToAction("Profile", "Home");
		}
		private void renderData()
		{
			LecturerDAO.Instance.GetAll();
			StudentDAO.Instance.GetAll();
			CourseDAO.Instance.GetAll();
			QuizDAO.Instance.GetAll();
		}
		private void SessionUser()
		{
			var user = HttpContext.Session.GetObject<UserLogin>("User");
			if (user != null)
			{
				ViewBag.Role = user.RoleID;
				ViewBag.login = true;
				ViewBag.user = user;

			}
		}
	}
}
