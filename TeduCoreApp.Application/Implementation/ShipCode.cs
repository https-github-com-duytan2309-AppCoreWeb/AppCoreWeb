using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Implementation
{
    public class ShipCode : IShipCode
    {
        public void CreateStreet(ShipCodeViewModel shipVm)
        {
            throw new NotImplementedException();
        }

        public void CreateWard(ShipCodeViewModel shipVm)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public PagedResult<ShipCodeViewModel> GetAllPaging(string startDate, string endDate, string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void UpdateStreet(ShipCodeViewModel shipVm)
        {
            throw new NotImplementedException();
        }

        public void UpdateWard(ShipCodeViewModel shipVm)
        {
            throw new NotImplementedException();
        }
    }
}