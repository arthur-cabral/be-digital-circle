﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
    }
}
