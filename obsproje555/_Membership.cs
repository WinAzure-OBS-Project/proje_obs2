using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace proje_obs
{
    public static class _Membership
    {
        public static void Login(String username, String role, HttpResponseBase Response)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        role,
                        FormsAuthentication.FormsCookiePath);

            string EncryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedTicket);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}

