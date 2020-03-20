using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class MenuService : IMenuService
    {
        private IRepository<Menu, int> _menuRepository;
        private IUnitOfWork _unitOfWork;

        public MenuService(IRepository<Menu, int> menuRepository,
            IUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }

        public MenuViewModel Add(MenuViewModel menuVm)
        {
            var menu = Mapper.Map<MenuViewModel, Menu>(menuVm);
            _menuRepository.Add(menu);
            return menuVm;
        }

        public void Delete(int id)
        {
            _menuRepository.Remove(id);
        }

        public List<MenuViewModel> GetAll()
        {
            return _menuRepository.FindAll().OrderBy(x => x.ParentId)
                 .ProjectTo<MenuViewModel>().ToList();
        }

        public List<MenuViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _menuRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId).ProjectTo<MenuViewModel>().ToList();
            else
                return _menuRepository.FindAll().OrderBy(x => x.ParentId)
                    .ProjectTo<MenuViewModel>()
                    .ToList();
        }

        public List<MenuViewModel> GetAllByParentId(int parentId)
        {
            return _menuRepository.FindAll(x => x.Status == true
            && x.ParentId == parentId)
             .ProjectTo<MenuViewModel>()
             .ToList();
        }

        public MenuViewModel GetById(int id)
        {
            return Mapper.Map<Menu, MenuViewModel>(_menuRepository.FindById(id));
        }

        public List<MenuViewModel> GetList(string groupAlias)
        {
            return _menuRepository.FindAll(x => x.Status == true && x.GroupAlias == groupAlias)
                .ProjectTo<MenuViewModel>().ToList();
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _menuRepository.FindById(sourceId);
            var target = _menuRepository.FindById(targetId);
            int tempOrder = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _menuRepository.Update(source);
            _menuRepository.Update(target);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MenuViewModel menuVm)
        {
            var menu = Mapper.Map<MenuViewModel, Menu>(menuVm);
            _menuRepository.Update(menu);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sourceCategory = _menuRepository.FindById(sourceId);
            sourceCategory.ParentId = targetId;
            _menuRepository.Update(sourceCategory);

            //Get all sibling
            var sibling = _menuRepository.FindAll(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _menuRepository.Update(child);
            }
        }
    }
}