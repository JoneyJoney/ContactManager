using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesInterface
{
    public interface IErrorHandleService
    {
        void InsertLogIntoDatabaseTable(Exception ex);
    }
}
