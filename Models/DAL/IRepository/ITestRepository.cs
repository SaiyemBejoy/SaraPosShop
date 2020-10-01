using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;

namespace DAL.IRepository
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestModel>> GetAllData();

        Task<TestModel> GetSingleData(int id);

        Task<TestModel> GetSingleData(string name);

        Task<int> GetMaxId();

        Task<bool> CheckIsExist(int id);

        Task<string> SaveOrUpdate(TestModel objTestModel);

        Task<string> Delete(int id);
    }
}
