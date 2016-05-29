using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.App_Start
{
    public class MyCookieAuthenticationProvider : CookieAuthenticationProvider
    {
        public override System.Threading.Tasks.Task ValidateIdentity(CookieValidateIdentityContext context)
        {

            

            return base.ValidateIdentity(context);
        }
    }
}