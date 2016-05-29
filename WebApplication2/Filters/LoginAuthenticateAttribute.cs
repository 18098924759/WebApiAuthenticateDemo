using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace WebApplication2.Filters
{
    public class LoginAuthenticateAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.Filters.IAuthenticationFilter
    {
        private static Dictionary<string, string> userAccounts;

        public LoginAuthenticateAttribute()
        {
            userAccounts = new Dictionary<string, string>();
            userAccounts.Add("Foo", "Password");
            userAccounts.Add("Bar", "Password");
            userAccounts.Add("Baz", "Password");
        }

        public void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        {
            var httpContext = filterContext.RequestContext.HttpContext;
            string ticket = string.Empty;
            if (httpContext.Request.QueryString["ticket"] != "")
            {
                ticket = httpContext.Request.QueryString["ticket"];
            }
            else
            {
                ticket = httpContext.Request.Cookies["ticket"].Value;
            }

            if (string.IsNullOrEmpty(ticket))
            {
                
            }

        }

        public void OnAuthenticationChallenge(System.Web.Mvc.Filters.AuthenticationChallengeContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoneLoginAuthenticationAttribute : System.Web.Http.Filters.FilterAttribute, System.Web.Http.Filters.IAuthenticationFilter
    {

        private static Dictionary<string, string> _userAccounts;

        public NoneLoginAuthenticationAttribute()
        {
            _userAccounts = new Dictionary<string, string>();
            _userAccounts.Add("Foo", "Password");
            _userAccounts.Add("Bar", "Password");
            _userAccounts.Add("Baz", "Password");
        }

        public System.Threading.Tasks.Task AuthenticateAsync(System.Web.Http.Filters.HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            IPrincipal user = null;
            AuthenticationHeaderValue headerValue = context.Request.Headers.Authorization;
            if (headerValue != null && headerValue.Scheme == "Basic")
            {
                string credential = Encoding.Default.GetString(Convert.FromBase64String(headerValue.Parameter));
                string[] split = credential.Split(':');
                if (split.Length == 2)
                {
                    string userName = split[0];
                    string password;
                    if (_userAccounts.TryGetValue(userName, out password))
                    {
                        if (password == split[1])
                        {
                            GenericIdentity identity = new GenericIdentity(userName, "Basic");
                            user = new GenericPrincipal(identity, new string[0]);
                        }
                    }

                }
            }
            context.Principal = user;
            return Task.FromResult<object>(null);

        }

        public System.Threading.Tasks.Task ChallengeAsync(System.Web.Http.Filters.HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            IPrincipal user = context.ActionContext.ControllerContext.RequestContext.Principal;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                string parameters = string.Format("realm=\"{0}\"", context.Request.RequestUri.DnsSafeHost);
                AuthenticationHeaderValue challenge = new AuthenticationHeaderValue("Basic", parameters);

                context.Result = new UnauthorizedResult(new AuthenticationHeaderValue[] { challenge }, context.Request);
            }

            return Task.FromResult<object>(null);

        }

    }

}