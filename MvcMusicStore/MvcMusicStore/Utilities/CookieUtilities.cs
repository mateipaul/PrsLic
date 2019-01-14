using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcMusicStore.Utilities
{
    public static class CookieUtilities
    {
        internal static Guid GetUserIdFromCookie(HttpRequestBase request)
        {
            Guid userId = new Guid();
            HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new char[] { '|' });
                userId = Guid.Parse(roles.FirstOrDefault());
            }
            return userId;
        }
    }
}