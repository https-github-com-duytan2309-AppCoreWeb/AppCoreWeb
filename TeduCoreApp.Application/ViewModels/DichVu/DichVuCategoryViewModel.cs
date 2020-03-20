using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Data.Enums;

namespace TeduCoreApp.Application.ViewModels.DichVu
{
    public class DichVuCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }
        public int? MenuId { get; set; }

        public int? HomeOrder { get; set; }
        public MenuViewModel Menu { set; get; }
        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }

        public ICollection<DichVuViewModel> DichVus { set; get; }
    }
}
