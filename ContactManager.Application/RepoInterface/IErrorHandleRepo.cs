﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.RepoInterface
{
    public interface IErrorHandleRepo
    {
        void InsertLogIntoDatabaseTable(Exception ex);
    }
}
