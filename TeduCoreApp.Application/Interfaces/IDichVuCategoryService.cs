using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.DichVu;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IDichVuCategoryService
    {
        DichVuCategoryViewModel Add(DichVuCategoryViewModel dichvuCategoryVm);

        void Update(DichVuCategoryViewModel dichvuCategoryVm);

        void Delete(int id);

        List<DichVuCategoryViewModel> GetAll();

        List<DichVuCategoryViewModel> GetAll(string keyword);

        List<DichVuCategoryViewModel> GetAllByParentId(int parentId);

        DichVuCategoryViewModel GetById(int id);

        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);

        void ReOrder(int sourceId, int targetId);

        List<DichVuCategoryViewModel> GetHomeCategories(int top);

        void Save();
    }
}