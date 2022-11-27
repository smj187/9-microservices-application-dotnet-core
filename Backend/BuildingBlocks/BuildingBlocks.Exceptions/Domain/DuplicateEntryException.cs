﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Domain
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message)
            : base(message)
        {

        }
    }
}
