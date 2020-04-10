using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }

        public string Code { set; get; }

        public string Name { set; get; }

        public string Rank { set; get; }

        public bool Status { set; get; }

        public List<DistrictViewModel> Districts { set; get; }
        //public List<WardViewModel> Wards { set; get; }
        //public List<StreetViewModel> Streets { set; get; }
    }
}