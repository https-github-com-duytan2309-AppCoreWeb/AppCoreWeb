using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IAddressService
    {
        void CreateStreet(StreetViewModel streetVm);

        void UpdateStreet(StreetViewModel streetVm);

        void CreateWard(WardViewModel wardVm);

        void UpdateWard(WardViewModel wardVm);

        void CreateDistrict(DistrictViewModel districtVm);

        void UpdateDistrict(DistrictViewModel districtVm);

        void CreateProvince(ProvinceViewModel provoinceVm);

        void UpdateProvince(ProvinceViewModel provoinceVm);

        StreetViewModel GetDetailStreet(int streetid, int wardid, int districtid, int provinceid);

        StreetViewModel GetDetailWard(int wardid, int districtid, int provinceid);

        StreetViewModel GetDetailDistrict(int districtid, int provinceid);

        StreetViewModel GetDetailProvince(int provinceid);

        void Save();
    }
}