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
    [Table("Pages")]
    public class Page : DomainEntity<int>,ISwitchable
    {
        public Page() { }

        public Page(int id, string name, string alias, 
            string content, Status status, int? homeOrder, int sortOrder, string seoAlias)
        {
            Id = id;
            Name = name;
            Alias = alias;
            Content = content;
            Status = status;
            HomeOrder = homeOrder;
            SortOrder = sortOrder;
            SeoAlias = seoAlias;
        }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }
        public Status Status { set; get; }

        public int? HomeOrder { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public string SeoAlias { set; get; }

        [StringLength(25)]
        [Required]
        public string GroupAlias { get; set; }
    }
}
