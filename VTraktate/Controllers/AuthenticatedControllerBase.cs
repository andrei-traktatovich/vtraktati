using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace VTraktate.Controllers
{
    public class AuthenticatedControllerBase : ApiController
    {
        protected int UserId
        {
            get { return User.Identity.GetUserId<int>(); }
        }
    }
}