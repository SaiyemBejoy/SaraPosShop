using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;

namespace DAL.IRepository
{
    public interface IEmployeeRepository
    {
        Task<string> UpdateEmployee(AuthModel objAuthModel);
    }
}
