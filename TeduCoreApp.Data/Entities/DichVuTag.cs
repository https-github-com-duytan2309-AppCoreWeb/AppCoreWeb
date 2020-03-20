using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("DichVuTags")]
    public class DichVuTag : DomainEntity<int>
    {
        public int DichVuId { set; get; }


        public string TagId { set; get; }

        [ForeignKey("DichVuId")]
        public virtual DichVu DichVu { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
