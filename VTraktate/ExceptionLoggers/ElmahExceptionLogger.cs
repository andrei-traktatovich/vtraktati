using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace VTraktate.ExceptionLoggers
{
    public class ElmahExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            if (context.Exception != null)
                Elmah.ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
}