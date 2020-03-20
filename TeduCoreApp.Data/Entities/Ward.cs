using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Ward")]
    public class Ward : DomainEntity<int>
    {
        public Ward()
        {
        }

        public Ward(int id, string code, string name,
            string rank, int provinceid, int districtid, bool status)
        {
            Id = id;
            Code = code;
            Name = name;
            Rank = rank;
            DistrictId = districtid;
            ProvinceId = provinceid;
            Status = status;
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
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { set; get; }

        [Required]
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { set; get; }

        public bool Status { set; get; }
        public virtual ICollection<Street> Streets { set; get; }
    }
}