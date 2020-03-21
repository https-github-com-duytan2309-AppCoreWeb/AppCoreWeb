using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Models
{
    public class ShoppingCartViewModel
    {
        public ProductViewModel Product { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        [Required(ErrorMessage = "Vui Lòng Chọn Màu")]
        public ColorViewModel Color { get; set; }

        [Required(ErrorMessage = "Vui Lòng Chọn Kích Thước")]
        public SizeViewModel Size { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { set; get; }
    }
}