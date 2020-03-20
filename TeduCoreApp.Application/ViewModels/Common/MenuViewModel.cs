using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Application.ViewModels.Blog;
using TeduCoreApp.Application.ViewModels.DichVu;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.Common
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public bool Status { set; get; }
        public string SeoAlias { set; get; }

        [StringLength(25)]
        [Required]
        public string GroupAlias { get; set; }

        public  ICollection<DichVuCategoryViewModel> DichVuCategories { set; get; }
        public ICollection<BlogCategoryViewModel> BlogCategories { set; get; }
    }
}
