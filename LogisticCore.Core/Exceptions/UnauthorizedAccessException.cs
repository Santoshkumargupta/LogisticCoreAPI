﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Core.Exceptions
{
    public class UnauthorizedAccessException:Exception
    {
        public UnauthorizedAccessException(string message):base(message)
        {

        }
    }
}
