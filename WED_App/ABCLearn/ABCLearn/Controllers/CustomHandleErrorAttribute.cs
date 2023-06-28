using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace ABCLearn.Controllers
{
	public class CustomHandleErrorAttribute : ActionFilterAttribute, IExceptionFilter
	{
		public CustomHandleErrorAttribute() { }
		public void OnException(ExceptionContext filterContext)
		{
			if (!filterContext.ExceptionHandled && filterContext.Exception is HttpRequestException)
			{
				var httpException = (HttpRequestException)filterContext.Exception;
				if (httpException.GetHashCode() == 404)
				{
					filterContext.ExceptionHandled = true;
					filterContext.Result = new ViewResult
					{
						ViewName = "Error404" // Tên view cho trang 404
					};
				}
			}
		}
	}
}
