using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class StreetViewModel
    {
        public int Id { get; set; }

        public string Code { set; get; }

        public string Name { set; get; }

        public string Rank { set; get; }

        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public bool Status { set; get; }
        public ProvinceViewModel Province { set; get; }
        public DistrictViewModel District { set; get; }
        public WardViewModel Ward { set; get; }
    }
}