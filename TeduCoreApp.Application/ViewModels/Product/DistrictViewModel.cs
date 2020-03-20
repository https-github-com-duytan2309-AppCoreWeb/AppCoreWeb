using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class DistrictViewModel
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { set; get; }

        [StringLength(250)]
        public string Name { set; get; }

        [StringLength(250)]
        public string Rank { set; get; }

        [Required]
        public int ProvinceId { get; set; }

        public ProvinceViewModel Province { set; get; }

        public bool Status { set; get; }
        public List<WardViewModel> Wards { set; get; }
        public List<StreetViewModel> Streets { set; get; }
    }
}