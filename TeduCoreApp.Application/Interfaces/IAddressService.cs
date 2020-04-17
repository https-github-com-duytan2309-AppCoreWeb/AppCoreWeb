using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IAddressService
    {
        void CreateStreet(StreetViewModel streetVm);

        void UpdateStreet(StreetViewModel streetVm);

        //void AddStreetAndShipCodeInCheckout(string NameDistrict, string NameWard, string Street);

        #region AJAX Request Province

        List<Province> GetProvinces();

        List<Province> GetProvincesByKeyString(string KeyString);

        Province GetProvinceByNameDistrict(string NameDistrict);

        Province GetProvinceByNameWard(string NameWard);

        #endregion AJAX Request Province

        #region AJAX Request District

        List<District> GetDistricts();

        List<District> GetDistrictsByNameProvince(string NameProvince);

        District GetDistrictsByNameWard(string NameWard);

        List<District> GetDistrictsByKeyString(string KeyString);

        List<District> GetDistrictsByKeyStringAndNameProvince(string KeyString, string NameProvince);

        #endregion AJAX Request District

        #region AJAX Request Ward

        List<Ward> GetWards();

        List<Ward> GetWardsByKeyString(string KeyString);

        List<Ward> GetWardsByKeyStringAndNameDistrict(string KeyString, string NameDistrict);

        List<Ward> GetWardsByNameDistrict(string NameDistrict);

        Ward GetWardByNameStreet(string NameStreet);

        List<Ward> GetWardsByKeyStringAndNameProvince(string KeyString, string NameProvince);

        List<Ward> GetWardsByNameProvince(string NameProvince);

        #endregion AJAX Request Ward

        #region AJAX Request Street

        List<Street> GetStreets();

        List<Street> GetStreetsByKeyString(string KeyString);

        List<Street> GetStreetsByNameWard(string NameWard);

        List<Street> GetStreetsByNameDistrict(string NameDistrict);

        List<Street> GetStreetsByKeyStringAndByNameWard(string KeyString, string NameWard);

        List<Street> GetStreetsByKeyStringAndByNameDistrict(string KeyString, string NameDistrict);

        #endregion AJAX Request Street

        void Save();
    }
}