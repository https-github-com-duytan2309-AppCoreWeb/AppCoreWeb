using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class ProductTrademarkService : IProductTrademarkService
    {
        private IRepository<ProductTrademark, int> _productTrademarkRepository;
        private IUnitOfWork _unitOfWork;

        public ProductTrademarkService(IRepository<ProductTrademark, int> productTrademarkRepository,
            IUnitOfWork unitOfWork)
        {
            _productTrademarkRepository = productTrademarkRepository;
            _unitOfWork = unitOfWork;
        }

        public ProductTrademarkViewModel Add(ProductTrademarkViewModel productTrademarkVm)
        {
            var productTrademark = Mapper.Map<ProductTrademarkViewModel, ProductTrademark>(productTrademarkVm);
            _productTrademarkRepository.Add(productTrademark);
            return productTrademarkVm;
        }

        public void Delete(int id)
        {
            _productTrademarkRepository.Remove(id);
        }

        public List<ProductTrademarkViewModel> GetAll()
        {
            return _productTrademarkRepository.FindAll()
                 .ProjectTo<ProductTrademarkViewModel>().ToList();
        }

        public List<ProductTrademarkViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productTrademarkRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword)).ProjectTo<ProductTrademarkViewModel>().ToList();
            else
                return _productTrademarkRepository.FindAll()
                    .ProjectTo<ProductTrademarkViewModel>()
                    .ToList();
        }

        public ProductTrademarkViewModel GetById(int id)
        {
            return Mapper.Map<ProductTrademark, ProductTrademarkViewModel>(_productTrademarkRepository.FindById(id));
        }

        public List<ProductTrademarkViewModel> GetHomeTrademark(int top)
        {
            var query = _productTrademarkRepository
                .FindAll(x => x.HomeFlag == true, c => c.Products)
                  .OrderBy(x => x.HomeOrder)
                  .Take(top).ProjectTo<ProductTrademarkViewModel>();

            var trademark = query.ToList();
            foreach (var category in trademark)
            {
                //category.Products = _productRepository
                //    .FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
                //    .OrderByDescending(x => x.DateCreated)
                //    .Take(5)
                //    .ProjectTo<ProductViewModel>().ToList();
            }
            return trademark;
        }

        public void ReOrder(int sourceId, int targetId)
        {
            var source = _productTrademarkRepository.FindById(sourceId);
            var target = _productTrademarkRepository.FindById(targetId);
            int tempOrder = source.SortOrder;
            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _productTrademarkRepository.Update(source);
            _productTrademarkRepository.Update(target);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductTrademarkViewModel productTrademarkVm)
        {
            var productTrademark = Mapper.Map<ProductTrademarkViewModel, ProductTrademark>(productTrademarkVm);
            _productTrademarkRepository.Update(productTrademark);
        }
    }
}