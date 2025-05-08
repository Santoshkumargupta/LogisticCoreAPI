using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Core.Exceptions
{
    public class AggregateException : Exception
    {
        public AggregateException(string message) : base(message)
        {

        }
    }
}
