using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Application.ServicesImplementation
{
    public class ErrorHandleService : IErrorHandleService
    {
        private readonly IErrorHandleRepo _errorHandleRepo;
        public ErrorHandleService(IErrorHandleRepo errorHandleRepo) 
        {
            _errorHandleRepo = errorHandleRepo; ;
        
        }
        public void InsertLogIntoDatabaseTable(Exception ex)
        {
            _errorHandleRepo.InsertLogIntoDatabaseTable(ex);
        }
    }
}
