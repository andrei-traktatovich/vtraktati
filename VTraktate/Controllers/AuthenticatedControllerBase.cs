﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Interfaces;
using VTraktate.DataAccess;
using VTraktate.Extensions;

namespace VTraktate.Controllers
{
    public class MyCustomFilterAttribute : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var x = actionContext.ControllerContext.RequestContext.Principal.Identity.Name;
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
            
        }
    }
    [Authorize]
    [MyCustomFilter]
    public class AuthenticatedControllerBase : ApiController
    {
        protected int UserId => User.Identity.GetUserId<int>();

        protected string ModelValidationErrorMessage => ModelState?.GetModelValidationErrorMessage();

        private ICerberos _cerberos;
        private ICerberosMum _cerberosMum;

        protected ICerberos Cerberos => _cerberos ?? (_cerberos = _cerberosMum.MakeCerberos(Context, () => UserId));
        
        public TraktatContext Context { get; set; }
        
        public AuthenticatedControllerBase(TraktatContext context, ICerberosMum cerberosMum)
        {
            Context = context;
            _cerberosMum = cerberosMum;
        }
        
        internal virtual IHttpActionResult FromOperationResult<T, TResult>(OperationResult<T> result)
        {
            if (result.Success)
            {
                var mapped = AutoMapper.Mapper.Map<T, TResult>(result.Data);
                return Ok(mapped);
            }
                

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