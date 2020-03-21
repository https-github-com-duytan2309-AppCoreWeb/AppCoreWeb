using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class BillViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui Lòng Nhập Tên Người Nhận")]
        [Display(Name = "Họ Tên")]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        //[Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required(ErrorMessage = "Vui Lòng Nhập Số Điện Thoại")]
        [Display(Name = "Số Điện Thoại")]
        [MaxLength(50)]
        [Phone(ErrorMessage = "Số Điện Thoại Không Đúng")]
        public string CustomerMobile { set; get; }

        [Display(Name = "Tin Nhắn")]
        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }

        public Guid? CustomerId { set; get; }

        public List<BillDetailViewModel> BillDetails { set; get; }

        public string Code { get; set; }
    }
}