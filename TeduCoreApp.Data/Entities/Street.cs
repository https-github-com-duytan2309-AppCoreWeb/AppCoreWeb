using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Street")]
    public class Street : DomainEntity<int>
    {
        public Street()
        {
        }

        public Street(int id, string code, string name,
            string rank, int provinceid, int districtid, int wardid, bool status)
        {
            Id = id;
            Code = code;
            Name = name;
            Rank = rank;
            DistrictId = districtid;
            WardId = wardid;
            Status = status;
            ProvinceId = provinceid;
        }

        [StringLength(10)]
        [Required]
        public string Code { set; get; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Rank { set; get; }

        [Required]
        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }
        public int WardId { get; set; }

        public bool Status { set; get; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { set; get; }

        [ForeignKey("DistrictId")]
        public virtual District District { set; get; }

        [ForeignKey("WardId")]
        public virtual Ward Ward { set; get; }
    }
}