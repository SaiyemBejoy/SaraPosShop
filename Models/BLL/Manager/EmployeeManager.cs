using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;

namespace BLL.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeManager()
        {
            _repository = new EmployeeRepository();
        }
        public async Task<string> UpdateEmployee(AuthModel objAuthModel)
        {
            try
            {
                var data = await _repository.UpdateEmployee(objAuthModel);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
