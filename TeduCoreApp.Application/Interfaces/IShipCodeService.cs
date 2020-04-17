using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IShipCodeService
    {
        void Create(ShipCodeViewModel shipVm);

        void Update(ShipCodeViewModel shipVm);

        void Delete(long id);

        ShipCodeViewModel GetById(long shipVm);

        PagedResult<ShipCodeViewModel> GetAllPaging(/*string startDate, string endDate, string keyword,*/int pageIndex, int pageSize);

        void Save();
    }
}