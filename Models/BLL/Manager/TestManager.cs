using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;

namespace BLL.Manager
{
    public class TestManager : ITestManager
    {
        private readonly ITestRepository _repository;

        public TestManager()
        {
            _repository = new TestRepository();
        }

        public async Task<IEnumerable<TestModel>> ViewAllData()
        {
            try
            {
                var test = await _repository.GetAllData();
                return test;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TestModel> ViewSingleDataById(int id)
        {
            try
            {
                var test = await _repository.GetSingleData(id);
                return test;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TestModel> ViewSingleDataByName(string name)
        {
            try
            {
                var test = await _repository.GetSingleData(name);
                return test;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> ViewMaxId()
        {
            try
            {
                var test = await _repository.GetMaxId();
                return test;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> ViewIsExist(int id)
        {
            try
            {
                var test = await _repository.CheckIsExist(id);
                return test;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SaveOrUpdate(TestModel objTestModel)
        {
            try
            {
                var test = await _repository.SaveOrUpdate(objTestModel);
                return test;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                var test = await _repository.Delete(id);
                return test;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }
    }
}
