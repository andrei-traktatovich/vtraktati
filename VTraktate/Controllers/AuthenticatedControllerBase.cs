using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Interfaces;
using VTraktate.DataAccess;

namespace VTraktate.Controllers
{
    [Authorize]
    public class AuthenticatedControllerBase : ApiController
    {
        protected int UserId => User.Identity.GetUserId<int>();

        private readonly ICerberosMum _cerberosMum;
        protected ICerberos Cerberos { get; set; }
        public TraktatContext Context { get; set; }
        
        public AuthenticatedControllerBase(TraktatContext context, ICerberosMum cerberosMum)
        {
            Context = context;
            _cerberosMum = cerberosMum;
        
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            Cerberos = _cerberosMum.MakeCerberos(Context, User.Identity.GetUserId<int>());
        }

        internal virtual IHttpActionResult FromOperationResult<T>(OperationResult<T> result)
        {
            if (result.Success)
                return Ok(result.Data);

            switch (result.StatusCode)
            {
                case OperationResult.ENTITY_NOT_FOUND: return NotFound();
                case OperationResult.UNAUTHORIZED: return Unauthorized();
                default: return BadRequest(result.ErrorMessage);
            }
        }

        internal virtual void Log(Exception ex)
        {
            throw new NotImplementedException(); 
        }
    }
}