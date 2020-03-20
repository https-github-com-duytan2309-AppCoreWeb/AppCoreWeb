using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Utilities.Extensions;

namespace TeduCoreApp.Models
{
    public class CheckoutViewModel : BillViewModel
    {
        public List<ShoppingCartViewModel> Carts { get; set; }

        public List<EnumModel> PaymentMethods
        {
            get
            {
                return ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                    .Select(c => new EnumModel
                    {
                        Value = (int)c,
                        Name = c.GetDescription()
                    }).ToList();
            }
        }

        [Display(Name = "Tỉnh (Thành Phố)")]
        [Required(ErrorMessage = "Vùi Lòng Nhập Tỉnh (Thành Phố)")]
        public string Province { set; get; }

        [Display(Name = "Quận (Huyện)")]
        [Required(ErrorMessage = "Vùi Lòng Nhập Quận (Huyện)")]
        public string District { set; get; }

        [Display(Name = "Phường (Xã)")]
        [Required(ErrorMessage = "Vùi Lòng Nhập Phường (Xã)")]
        public string Ward { set; get; }

        [Display(Name = "Đường (Phố)")]
        [Required(ErrorMessage = "Vùi Lòng Nhập Đường (Phố)")]
        public string Street { set; get; }
    }
}