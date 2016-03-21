using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using VTraktate.Models;
using VTraktate.Repository;
using Microsoft.AspNet.Identity;

namespace VTraktate.Controllers
{
    public class EmploymentController : ApiController
    {
        protected int UserId
        {
            get
            {
                return User.Identity.GetUserId<int>();
            }
        }
        protected EmploymentRepo Repository { get; private set; }
        public EmploymentController(EmploymentRepo repo)
        {
            Repository = repo;
        }

         
    }
}
