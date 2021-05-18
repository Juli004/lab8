﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OSBB.Security.Identity
{
    public class SSi
        : Client
    {
        public SSi(int userId, string name, int catalogId) 
            : base(userId, name, catalogId, nameof(SSi))
        {
        }
    }
}
