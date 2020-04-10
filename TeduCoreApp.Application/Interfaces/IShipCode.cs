using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IShipCode : IDisposable
    {
        void CreateStreet(ShipCodeViewModel shipVm);

        void UpdateStreet(ShipCodeViewModel shipVm);

        void CreateWard(ShipCodeViewModel shipVm);

        void UpdateWard(ShipCodeViewModel shipVm);

        PagedResult<ShipCodeViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);
    }
}