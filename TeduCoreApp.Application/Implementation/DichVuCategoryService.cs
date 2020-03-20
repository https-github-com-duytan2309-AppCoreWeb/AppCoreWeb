using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.DichVu;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class DichVuCategoryService : IDichVuCategoryService
    {
        private IRepository<DichVuCategory, int> _dichvuCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public DichVuCategoryService(IRepository<DichVuCategory, int> dichvuCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _dichvuCategoryRepository = dichvuCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public DichVuCategoryViewModel Add(DichVuCategoryViewModel dichvuCategoryVm)
        {
            var dichvuCategory = Mapper.Map<DichVuCategoryViewModel, DichVuCategory>(dichvuCategoryVm);
            _dichvuCategoryRepository.Add(dichvuCategory);
            return dichvuCategoryVm;
        }

        public void Delete(int id)
        {
            _dichvuCategoryRepository.Remove(id);
        }

        public List<DichVuCategoryViewModel> GetAll()
        {
            return _dichvuCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                 .ProjectTo<DichVuCategoryViewModel>().ToList();
        }

        public List<DichVuCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _dichvuCategoryRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId).ProjectTo<DichVuCategoryViewModel>().ToList();
            else
                return _dichvuCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                    .ProjectTo<DichVuCategoryViewModel>()
                    .ToList();
        }

        public List<DichVuCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _dichvuCategoryRepository.FindAll(x => x.Status == Status.Active
            && x.ParentId == parentId)
             .ProjectTo<DichVuCategoryViewModel>()
             .ToList();
        }

        public DichVuCategoryViewModel GetById(int id)
        {
            return Mapper.Map<DichVuCategory, DichVuCategoryViewModel>(_dichvuCategoryRepository.FindById(id));
        }

        public List<DichVuCategoryViewModel> GetHomeCategories(int top)
        {
            var query = _dichvuCategoryRepository
                .FindAll(x => x.HomeFlag == true && x.ParentId == null, c => c.DichVus)
                  .OrderBy(x => x.HomeOrder)
                  .Take(top).ProjectTo<DichVuCategoryViewModel>();

            var categories = query.ToList();
            foreach (var category in categories)
            {
                //category.DichVus = _dichvuRepository
                //    .FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
                //    .OrderByDescending(x => x.DateCreated)
                //    .Take(5)
                //    .ProjectTo<ProductViewModel>().ToList();
            }
            return categories;
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _dichvuCategoryRepository.FindById(sourceId);
            var target = _dichvuCategoryRepository.FindById(targetId);
            int tempOrder = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _dichvuCategoryRepository.Update(source);
            _dichvuCategoryRepository.Update(target);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(DichVuCategoryViewModel dichvuCategoryVm)
        {
            var dichvuCategory = Mapper.Map<DichVuCategoryViewModel, DichVuCategory>(dichvuCategoryVm);
            _dichvuCategoryRepository.Update(dichvuCategory);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sourceCategory = _dichvuCategoryRepository.FindById(sourceId);
            sourceCategory.ParentId = targetId;
            _dichvuCategoryRepository.Update(sourceCategory);

            //Get all sibling
            var sibling = _dichvuCategoryRepository.FindAll(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _dichvuCategoryRepository.Update(child);
            }
        }
    }
}