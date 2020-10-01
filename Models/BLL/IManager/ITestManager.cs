using Models.AllModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.IManager
{
    public interface ITestManager
    {
        Task<IEnumerable<TestModel>> ViewAllData();

        Task<TestModel> ViewSingleDataById(int id);

        Task<TestModel> ViewSingleDataByName(string name);

        Task<int> ViewMaxId();

        Task<bool> ViewIsExist(int id);

        Task<string> SaveOrUpdate(TestModel objTestModel);

        Task<string> Delete(int id);
    }
}