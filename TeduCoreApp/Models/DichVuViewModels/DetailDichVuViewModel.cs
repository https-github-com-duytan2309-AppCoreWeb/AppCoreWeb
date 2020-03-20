using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Application.ViewModels.DichVu;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Models.DichVuViewModels
{
    public class DetailDichVuViewModel
    {
        public DichVuViewModel DichVu { get; set; }

        public bool Available { set; get; }

        public List<DichVuViewModel> RelatedDichVus { get; set; }

        public DichVuCategoryViewModel DichVuCategory { get; set; }
        //public ProductTrademarkViewModel Trademark { get; set; }

        //public List<ProductImageViewModel> ProductImages { set; get; }

        public List<DichVuViewModel> UpsellProducts { get; set; }

        public List<DichVuViewModel> LastestProducts { get; set; }

        public List<TagViewModel> Tags { set; get; }
    }
}
