using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IEmployeeManager
    {
        Task<string> UpdateEmployee(AuthModel objAuthModel);
    }
}
