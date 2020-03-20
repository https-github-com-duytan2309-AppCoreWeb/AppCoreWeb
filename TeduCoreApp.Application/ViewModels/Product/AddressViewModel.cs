using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class AddressViewModel
    {
        public ProvinceViewModel Province { set; get; }
        public DistrictViewModel Color { set; get; }
        public WardViewModel Ward { set; get; }
        public StreetViewModel Street { set; get; }
    }
}