using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.DichVu;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IDichVuService
    {
        DichVuViewModel Add(DichVuViewModel product);

        void Update(DichVuViewModel product);

        void Delete(int id);

        List<DichVuViewModel> GetAll();

        PagedResult<DichVuViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);

        List<DichVuViewModel> GetLastest(int top);

        List<DichVuViewModel> GetHotProduct(int top);

        List<DichVuViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);

        List<DichVuViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        List<DichVuViewModel> GetList(string keyword);

        List<DichVuViewModel> GetRelatedDichVus(int id, int top);

        List<string> GetListByName(string name);

        DichVuViewModel GetById(int id);

        void Save();

        List<TagViewModel> GetListTagById(int id);

        TagViewModel GetTag(string tagId);

        void IncreaseView(int id);

        List<DichVuViewModel> GetListByTag(string tagId, int page, int pagesize, out int totalRow);

        List<TagViewModel> GetListTag(string searchText);
    }
}
