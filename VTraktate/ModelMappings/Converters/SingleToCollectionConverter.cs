using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTraktate.ModelMappings.Converters
{
    public class SingleToCollectionConverter<TSource, TResult> : ITypeConverter<TSource, IEnumerable<TResult>>

    {
        public SingleToCollectionConverter() { }

        public SingleToCollectionConverter(Predicate<TSource> predicate = null)
        {
            _predicate = predicate;
        }

        private Predicate<TSource> _predicate; 

        public virtual IEnumerable<TResult> Convert(ResolutionContext context)
        {
            if (_predicate != null)
            {
                var source = (TSource)context.SourceValue;
                if(_predicate(source))
                    yield return Mapper.Map<TResult>(source);
            }
        }
    }

     
}