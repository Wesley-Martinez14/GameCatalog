using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace GameCatalog.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermisoRequeridoAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _permisos;

        public PermisoRequeridoAttribute(params string[] permisos)
        {
            _permisos = permisos;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated || !_permisos.Any(p => user.HasClaim("Permission", p)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
