using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChillSpot.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string _requiredRol;

        public SessionAuthorizeAttribute(string requiredRol)
        {
            _requiredRol = requiredRol;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var isAuthenticated = httpContext.Session.GetString("UsuarioAutenticado") == "true";
            var rol = httpContext.Session.GetString("Rol");

            if (!isAuthenticated || rol != _requiredRol)
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            }
            base.OnActionExecuting(context);
        }
    }
}
