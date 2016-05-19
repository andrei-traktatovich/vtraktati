using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace VTraktate.Extensions
{
    public static class ModelStateDictionaryExt
    {
        public static string GetModelValidationErrorMessage(this ModelStateDictionary @this)
        {
            string messages = string.Join("; ", @this.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            return messages;
        }

        public static IEnumerable<string> GetModelValidationErrors(this ModelStateDictionary @this)
        {
            return @this.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);
        }
    }
}