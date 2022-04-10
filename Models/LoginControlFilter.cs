using Microsoft.AspNetCore.Mvc.Filters;

namespace PET_GIDA.Controllers
{
    public class LoginControlFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loginId = context.HttpContext.Session.GetString("loginId");
            if (string.IsNullOrEmpty(loginId))
            {
                //Daha giriş yapmamış
                context.HttpContext.Response.Redirect("/Home/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}