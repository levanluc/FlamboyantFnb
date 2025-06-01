using System;
using System.Collections.Generic;
using System.Text;

namespace FlamboyantFnb.Domain.Exceptions
{
    public class ValidateException : Exception
    {
        public ValidateException(string msg) : base(msg) { }
    }
}
