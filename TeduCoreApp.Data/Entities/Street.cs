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
            string rank, int wardid, int district, bool status)
        {
            Id = id;
            Code = code;
            Name = name;
            Rank = rank;
            WardId = wardid;
            DistrictId = district;
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

        public int WardId { get; set; }
        public int DistrictId { get; set; }
        public bool Status { set; get; }

        [ForeignKey("WardId")]
        public virtual Ward Ward { set; get; }

        [ForeignKey("DistrictId")]
        public virtual District District { set; get; }
    }
}