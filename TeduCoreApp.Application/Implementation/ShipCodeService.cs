using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Implementation
{
    public class ShipCodeService : IShipCodeService
    {
        private readonly IShipCodeRepository _shipCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShipCodeService(IShipCodeRepository shipCodeRepository, IUnitOfWork unitOfWork)
        {
            _shipCodeRepository = shipCodeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(ShipCodeViewModel shipVm)
        {
            var ship = Mapper.Map<ShipCodeViewModel, ShipCode>(shipVm);
            _shipCodeRepository.Add(ship);
        }

        public void Update(ShipCodeViewModel shipVm)
        {
            var ship = Mapper.Map<ShipCodeViewModel, ShipCode>(shipVm);
            _shipCodeRepository.Update(ship);
        }

        public void Delete(long id)
        {
            var ship = _shipCodeRepository.FindById(id);
            _shipCodeRepository.Remove(ship);
        }

        public ShipCodeViewModel GetById(long id)
        {
            var shipVM = _shipCodeRepository.FindById(id);
            var ship = Mapper.Map<ShipCode, ShipCodeViewModel>(shipVM);
            return ship;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public PagedResult<ShipCodeViewModel> GetAllPaging(/*string startDate, string endDate, string keyword, */int pageIndex, int pageSize)
        {
            var query = _shipCodeRepository.FindAll();
            //if (!string.IsNullOrEmpty(startDate))
            //{
            //    DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
            //    query = query.Where(x => x.DateCreated >= start);
            //}
            //if (!string.IsNullOrEmpty(endDate))
            //{
            //    DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
            //    query = query.Where(x => x.DateCreated <= end);
            //}
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    query = query.Where(x => x.CustomerName.Contains(keyword) || x.CustomerMobile.Contains(keyword));
            //}
            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ShipCodeViewModel>()
                .ToList();
            return new PagedResult<ShipCodeViewModel>()
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