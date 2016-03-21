using PhoneBookMVC.Entity;
using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Filters
{
    public class AuthorizeUser : AuthorizeAttribute
    {
        private enum ReasonEnum {Login, Role, Username}
        private ReasonEnum Reason { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Check if LoggedUserExists
            if (AuthenticationManager.LoggedUser == null)
            {
                Reason = ReasonEnum.Login;
                return false;
            }

            // Roles authorization
            if (!string.IsNullOrEmpty(Roles))
            {
                if (!(Roles.Split(',').Any(role => string.Compare(role.Trim(), AuthenticationManager.LoggedUser.Role.ToString(), StringComparison.OrdinalIgnoreCase) == 0)))
                {
                    Reason = ReasonEnum.Role;
                    return false;
                }
            }

            // Users authorization
            if (!string.IsNullOrEmpty(Users))
            {
                if (Users.Split(',').Any(u => string.Compare(u, AuthenticationManager.LoggedUser.Username, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    return true;
                }
            }

            // If authorization is ok, return true
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (Reason == ReasonEnum.Role || Reason == ReasonEnum.Username)
            {
                filterContext.HttpContext.Response.Redirect("~/Error/Unauthorized");
                filterContext.Result = new EmptyResult();
                return;
            }
            if (Reason == ReasonEnum.Login)
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
                filterContext.Result = new EmptyResult();
                return;
            }
        }
    }
}