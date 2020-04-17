using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class AddressService : IAddressService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _disctrictRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly AppDbContext _context;

        public AddressService(
            IProvinceRepository provinceRepository,
            IDistrictRepository disctrictRepository,
            IWardRepository wardRepository,
            IStreetRepository streetRepository,
            IUnitOfWork unitOfWork, AppDbContext context
            )
        {
            _provinceRepository = provinceRepository;
            _disctrictRepository = disctrictRepository;
            _wardRepository = wardRepository;
            _streetRepository = streetRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public void CreateStreet(StreetViewModel streetVm)
        {
            var street = Mapper.Map<StreetViewModel, Street>(streetVm);
            _streetRepository.Add(street);
        }

        public void UpdateStreet(StreetViewModel streetVm)
        {
            var street = Mapper.Map<StreetViewModel, Street>(streetVm);
            _streetRepository.Update(street);
        }

        //public void AddStreetAndShipCodeInCheckout(string NameDistrict, string NameWard, string Street)
        //{
        //    var idDistrict = _provinceRepository.FindSingle(x => x.Name == NameDistrict).Id;
        //    var idWard = _provinceRepository.FindSingle(x => x.Name == NameWard).Id;
        //    StreetViewModel streetVm = new StreetViewModel();
        //    streetVm.Name = Street;
        //    streetVm.DistrictId = idDistrict;
        //    streetVm.WardId = idWard;
        //    streetVm.Status = false;
        //    var street = Mapper.Map<StreetViewModel, Street>(streetVm);
        //    _streetRepository.Add(street);

        //    var idAddress = _context.Address.Where(x => x.WardId == idWard && x.DistrictId == idDistrict).FirstOrDefault();
        //    ShipCode ship = new ShipCode();
        //    ship.IdAddress = idAddress.Id;
        //    _context.Add(ship);
        //    _context.SaveChanges();
        //}

        #region AJAX Request Province

        public List<Province> GetProvinces()
        {
            return _provinceRepository.FindAll().ToList();
        }

        public List<Province> GetProvincesByKeyString(string KeyString)
        {
            var lists = _provinceRepository.FindAll().Where(x => x.Name.Contains(KeyString)).ToList();
            return lists;
        }

        public Province GetProvinceByNameDistrict(string NameDistrict)
        {
            var idProvince = _disctrictRepository.FindSingle(x => x.Name == NameDistrict).ProvinceId;
            var province = _provinceRepository.FindById(idProvince);
            return province;
        }

        public Province GetProvinceByNameWard(string NameWard)
        {
            var idProvince = _wardRepository.FindSingle(x => x.Name == NameWard).ProvinceId;
            var province = _provinceRepository.FindById(idProvince);
            return province;
        }

        #endregion AJAX Request Province

        #region AJAX Request District

        public List<District> GetDistricts()
        {
            return _disctrictRepository.FindAll().ToList();
        }

        public List<District> GetDistrictsByNameProvince(string NameProvince)
        {
            var idProvice = _provinceRepository.FindSingle(x => x.Name == NameProvince).Id;
            var lists = _disctrictRepository.FindAll(x => x.ProvinceId == idProvice).ToList();
            return lists;
        }

        public District GetDistrictsByNameWard(string NameWard)
        {
            int IdDistrict = _wardRepository.FindSingle(x => x.Name == NameWard).DistrictId;
            var district = _disctrictRepository.FindById(IdDistrict);
            return district;
        }

        public List<District> GetDistrictsByKeyString(string KeyString)
        {
            var lists = _disctrictRepository.FindAll().Where(x => x.Name.Contains(KeyString)).ToList();
            return lists;
        }

        public List<District> GetDistrictsByKeyStringAndNameProvince(string KeyString, string NameProvince)
        {
            var idProvice = _provinceRepository.FindSingle(x => x.Name == NameProvince).Id;
            var lists = _disctrictRepository.FindAll().Where(x => x.Name.Contains(KeyString) && x.ProvinceId == idProvice).ToList();
            return lists;
        }

        #endregion AJAX Request District

        #region AJAX Request Ward

        public List<Ward> GetWards()
        {
            return _wardRepository.FindAll().ToList();
        }

        public List<Ward> GetWardsByKeyString(string KeyString)
        {
            var lists = _wardRepository.FindAll().Where(x => x.Name.Contains(KeyString)).ToList();
            return lists;
        }

        public List<Ward> GetWardsByNameDistrict(string NameDistrict)
        {
            var idDistrict = _disctrictRepository.FindSingle(x => x.Name == NameDistrict).Id;
            var lists = _wardRepository.FindAll().Where(x => x.DistrictId == idDistrict).ToList();
            return lists;
        }

        public List<Ward> GetWardsByKeyStringAndNameDistrict(string KeyString, string NameDistrict)
        {
            var idDistrict = _disctrictRepository.FindSingle(x => x.Name == NameDistrict).Id;
            var lists = _wardRepository.FindAll().Where(x => x.Name.Contains(KeyString) && x.DistrictId == idDistrict).ToList();
            return lists;
        }

        public Ward GetWardByNameStreet(string NameStreet)
        {
            var idWard = _streetRepository.FindSingle(x => x.Name == NameStreet).WardId;
            var ward = _wardRepository.FindById(idWard);
            return ward;
        }

        public List<Ward> GetWardsByKeyStringAndNameProvince(string KeyString, string NameProvince)
        {
            var idProvince = _provinceRepository.FindSingle(x => x.Name == NameProvince).Id;
            var lists = _wardRepository.FindAll().Where(x => x.Name.Contains(KeyString) && x.ProvinceId == idProvince).ToList();
            return lists;
        }

        public List<Ward> GetWardsByNameProvince(string NameProvince)
        {
            var idProvince = _provinceRepository.FindSingle(x => x.Name == NameProvince).Id;
            var lists = _wardRepository.FindAll().Where(x => x.ProvinceId == idProvince).ToList();
            return lists;
        }

        #endregion AJAX Request Ward

        #region AJAX Request Street

        public List<Street> GetStreets()
        {
            return _streetRepository.FindAll().ToList();
        }

        public List<Street> GetStreetsByKeyString(string KeyString)
        {
            var lists = _streetRepository.FindAll().Where(x => x.Name.Contains(KeyString)).ToList();
            return lists;
        }

        public List<Street> GetStreetsByNameWard(string NameWard)
        {
            var idWard = _wardRepository.FindSingle(x => x.Name == NameWard).Id;
            var lists = _streetRepository.FindAll().Where(x => x.WardId == idWard).ToList();
            return lists;
        }

        public List<Street> GetStreetsByNameDistrict(string NameDistrict)
        {
            var idDistrict = _disctrictRepository.FindAll(x => x.Name == NameDistrict).Select(x => x.Id).FirstOrDefault();
            var lists = _streetRepository.FindAll().Where(x => x.DistrictId == idDistrict).ToList();
            return lists;
        }

        public List<Street> GetStreetsByKeyStringAndByNameWard(string KeyString, string NameWard)
        {
            var idWard = _wardRepository.FindSingle(x => x.Name == NameWard).Id;
            var lists = _streetRepository.FindAll().Where(x => x.WardId == idWard && x.Name.Contains(KeyString)).ToList();
            return lists;
        }

        public List<Street> GetStreetsByKeyStringAndByNameDistrict(string KeyString, string NameDistrict)
        {
            var idDistrict = _disctrictRepository.FindSingle(x => x.Name == NameDistrict).Id;
            var lists = _streetRepository.FindAll().Where(x => x.DistrictId == idDistrict && x.Name.Contains(KeyString)).ToList();
            return lists;
        }

        #endregion AJAX Request Street

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}