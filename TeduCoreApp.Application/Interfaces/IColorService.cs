using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IColorService
    {
        void Create(ColorViewModel colorVm);

        void Update(ColorViewModel colorVm);

        void Delete(int id);

        ColorViewModel GetById(int colorVm);

        PagedResult<ColorViewModel> GetAllPaging(int pageIndex, int pageSize);

        void Save();
    }
}