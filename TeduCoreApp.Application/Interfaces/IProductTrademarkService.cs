using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IProductTrademarkService
    {
        ProductTrademarkViewModel Add(ProductTrademarkViewModel productTrademarkVm);

        void Update(ProductTrademarkViewModel productTrademarkVm);

        void Delete(int id);

        List<ProductTrademarkViewModel> GetAll();

        List<ProductTrademarkViewModel> GetAll(string keyword);
        

        ProductTrademarkViewModel GetById(int id);
        
        void ReOrder(int sourceId, int targetId);

        List<ProductTrademarkViewModel> GetHomeTrademark(int top);

       


        void Save();
    }
}
