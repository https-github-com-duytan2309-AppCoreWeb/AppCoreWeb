using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Interfaces
{
    public interface ISizeService
    {
        void Create(SizeViewModel sizeVm);

        void Update(SizeViewModel sizeVm);

        void Delete(int id);

        SizeViewModel GetById(int sizeVm);

        PagedResult<SizeViewModel> GetAllPaging(int pageIndex, int pageSize);

        void Save();
    }
}