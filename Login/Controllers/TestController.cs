using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        public string Get()
        { 
           return "111111111111";
        }
    }
}
