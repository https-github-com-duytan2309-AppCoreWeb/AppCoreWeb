using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Common;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IMenuService
    {
        MenuViewModel Add(MenuViewModel menuVm);

        void Update(MenuViewModel menuVm);

        void Delete(int id);

        List<MenuViewModel> GetAll();

        List<MenuViewModel> GetAll(string keyword);

        List<MenuViewModel> GetAllByParentId(int parentId);

        MenuViewModel GetById(int id);

        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);
        void ReOrder(int sourceId, int targetId);
        

        List<MenuViewModel> GetList(string groupAlias);



        void Save();
    }
}