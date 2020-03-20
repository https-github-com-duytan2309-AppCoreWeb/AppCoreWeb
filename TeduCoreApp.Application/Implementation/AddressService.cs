using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.EF.Repositories;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class AddressService : IAddressService
    {
        private readonly ProvinceRepository _provinceRepository;
        private readonly DistrictRepository _disctrictRepository;
        private readonly WardRepository _wardRepository;
        private readonly StreetRepository _streetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(
            ProvinceRepository provinceRepository,
            DistrictRepository disctrictRepository,
            WardRepository wardRepository,
            StreetRepository streetRepository,
            IUnitOfWork unitOfWork
            )
        {
            _provinceRepository = provinceRepository;
            _disctrictRepository = disctrictRepository;
            _wardRepository = wardRepository;
            _streetRepository = streetRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateDistrict(DistrictViewModel districtVm)
        {
            var district = Mapper.Map<DistrictViewModel, District>(districtVm);
            var wards = Mapper.Map<List<WardViewModel>, List<Ward>>(districtVm.Wards);
            var streets = Mapper.Map<List<StreetViewModel>, List<Street>>(districtVm.Streets);

            //foreach (var district in districts)
            //{
            //    var product = _disctrictRepository.FindById(district.ProvinceId);
            //    //district. = product.Price;
            //}

            district.Wards = wards;
            district.Streets = streets;
            _disctrictRepository.Add(district);
        }

        public void CreateProvince(ProvinceViewModel provoinceVm)
        {
            var province = Mapper.Map<ProvinceViewModel, Province>(provoinceVm);
            var districts = Mapper.Map<List<DistrictViewModel>, List<District>>(provoinceVm.Districts);
            var wards = Mapper.Map<List<WardViewModel>, List<Ward>>(provoinceVm.Wards);
            var streets = Mapper.Map<List<StreetViewModel>, List<Street>>(provoinceVm.Streets);

            //foreach (var district in districts)
            //{
            //    var product = _disctrictRepository.FindById(district.ProvinceId);
            //    //district. = product.Price;
            //}
            province.Districts = districts;
            province.Wards = wards;
            province.Streets = streets;
            _provinceRepository.Add(province);
        }

        public void CreateStreet(StreetViewModel streetVm)
        {
            var street = Mapper.Map<StreetViewModel, Street>(streetVm);
            _streetRepository.Add(street);
        }

        public void CreateWard(WardViewModel wardVm)
        {
            var ward = Mapper.Map<WardViewModel, Ward>(wardVm);
            var streets = Mapper.Map<List<StreetViewModel>, List<Street>>(wardVm.Streets);

            //foreach (var district in districts)
            //{
            //    var product = _disctrictRepository.FindById(district.ProvinceId);
            //    //district. = product.Price;
            //}

            ward.Streets = streets;
            _wardRepository.Add(ward);
        }

        public StreetViewModel GetDetailDistrict(int districtid, int provinceid)
        {
            throw new NotImplementedException();
        }

        public StreetViewModel GetDetailProvince(int provinceid)
        {
            throw new NotImplementedException();
        }

        public StreetViewModel GetDetailStreet(int streetid, int wardid, int districtid, int provinceid)
        {
            throw new NotImplementedException();
        }

        public StreetViewModel GetDetailWard(int wardid, int districtid, int provinceid)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateDistrict(DistrictViewModel districtVm)
        {
            //var province = Mapper.Map<ProvinceViewModel, Province>(provoinceVm);
        }

        public void UpdateProvince(ProvinceViewModel provoinceVm)
        {
            var province = Mapper.Map<ProvinceViewModel, Province>(provoinceVm);

            //District
            var newdistricts = province.Districts;

            //new districts added
            var addedDistricts = newdistricts.Where(x => x.Id == 0).ToList();

            //get updated districts
            var updatedDistricts = newdistricts.Where(x => x.Id != 0).ToList();

            //Existed districts
            var existedDistricts = _disctrictRepository.FindAll(x => x.ProvinceId == provoinceVm.Id);

            province.Districts.Clear();

            //Ward
            var newwards = province.Wards;

            //new wards added
            var addedWards = newwards.Where(x => x.Id == 0).ToList();

            //get updated wards
            var updatedWards = newwards.Where(x => x.Id != 0).ToList();

            //Existed wards
            var existedWards = _wardRepository.FindAll(x => x.ProvinceId == provoinceVm.Id);

            province.Wards.Clear();

            var newtreets = province.Streets;

            //new districts added
            var addedTreets = newtreets.Where(x => x.Id == 0).ToList();

            //get updated districts
            var updatedTreets = newtreets.Where(x => x.Id != 0).ToList();

            //Existed details
            var existedTreets = _streetRepository.FindAll(x => x.ProvinceId == provoinceVm.Id);
            //Clear db
            province.Streets.Clear();

            foreach (var district in updatedDistricts)
            {
                _disctrictRepository.Update(district);
            }
            foreach (var ward in updatedWards)
            {
                _wardRepository.Update(ward);
            }
            foreach (var street in updatedTreets)
            {
                _streetRepository.Update(street);
            }
        }

        public void UpdateStreet(StreetViewModel streetVm)
        {
            throw new NotImplementedException();
        }

        public void UpdateWard(WardViewModel wardVm)
        {
            throw new NotImplementedException();
        }
    }
}