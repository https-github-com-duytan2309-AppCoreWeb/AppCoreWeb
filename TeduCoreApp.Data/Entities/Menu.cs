using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Menu")]
    public class Menu : DomainEntity<int>, ISortable, IDateTracking
    {
        public Menu()
        {
            DichVuCategories = new List<DichVuCategory>();
            BlogCategories = new List<BlogCategory>();
        }

        public Menu(string name, string description, string url, string target, int? parentId, int? homeOrder,int sortOrder, string seoAlias
          )
        {
            Name = name;
            Description = description;
            Url = url;
            Target = target;
            ParentId = parentId;
            HomeOrder = homeOrder;
            SortOrder = sortOrder;
            SeoAlias = seoAlias;
        }
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
        
        public virtual ICollection<DichVuCategory> DichVuCategories { set; get; }
        public virtual ICollection<BlogCategory> BlogCategories { set; get; }
    }
}
