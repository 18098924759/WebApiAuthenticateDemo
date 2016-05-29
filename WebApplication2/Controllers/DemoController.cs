using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Filters;

namespace WebApplication2.Controllers
{
    [NoneLoginAuthentication]    
    public class DemoController : ApiController
    {
        public string Get()
        {
            return "Hello,World!";
        }
    }
}
