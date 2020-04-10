using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Province")]
    public class Province : DomainEntity<int>
    {
        public Province()
        {
        }

        public Province(int id, string code, string name,
            string rank, bool status)
        {
            Id = id;
            Code = code;
            Name = name;
            Rank = rank;
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

        public bool Status { set; get; }

        public virtual ICollection<District> Districts { set; get; }
        public virtual ICollection<Ward> Wards { set; get; }
    }
}