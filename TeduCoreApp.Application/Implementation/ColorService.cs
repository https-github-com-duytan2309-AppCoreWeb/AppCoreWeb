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
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(ColorViewModel colorVm)
        {
            var color = Mapper.Map<ColorViewModel, Color>(colorVm);
            _colorRepository.Add(color);
        }

        public void Update(ColorViewModel colorVm)
        {
            var color = Mapper.Map<ColorViewModel, Color>(colorVm);
            _colorRepository.Update(color);
        }

        public void Delete(int id)
        {
            var color = _colorRepository.FindById(id);
            _colorRepository.Remove(color);
        }

        public ColorViewModel GetById(int id)
        {
            var colorVM = _colorRepository.FindById(id);
            var color = Mapper.Map<Color, ColorViewModel>(colorVM);
            return color;
        }

        public PagedResult<ColorViewModel> GetAllPaging(int pageIndex, int pageSize)
        {
            var query = _colorRepository.FindAll();
            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ColorViewModel>()
                .ToList();
            return new PagedResult<ColorViewModel>()
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