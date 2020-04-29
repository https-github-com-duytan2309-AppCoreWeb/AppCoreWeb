using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Implementation
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(ISizeRepository sizeRepository, IUnitOfWork unitOfWork)
        {
            _sizeRepository = sizeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(SizeViewModel sizeVm)
        {
            var size = Mapper.Map<SizeViewModel, Size>(sizeVm);
            _sizeRepository.Add(size);
        }

        public void Update(SizeViewModel sizeVm)
        {
            var size = Mapper.Map<SizeViewModel, Size>(sizeVm);
            _sizeRepository.Update(size);
        }

        public void Delete(int id)
        {
            var size = _sizeRepository.FindById(id);
            _sizeRepository.Remove(size);
        }

        public SizeViewModel GetById(int id)
        {
            var sizeVM = _sizeRepository.FindById(id);
            var size = Mapper.Map<Size, SizeViewModel>(sizeVM);
            return size;
        }

        public PagedResult<SizeViewModel> GetAllPaging(int pageIndex, int pageSize)
        {
            var query = _sizeRepository.FindAll();
            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<SizeViewModel>()
                .ToList();
            return new PagedResult<SizeViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}