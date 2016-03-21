using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTraktate.Domain
{
    public class QA
    {
        public decimal Grade { get; set; }
        public Stars Stars { get; set; }

        public string Comment { get; set; }
    }

    public enum Stars
    {
        Zero = 0,
        Medium = 1,
        Good = 2,
        Excellent = 3
    }
}
