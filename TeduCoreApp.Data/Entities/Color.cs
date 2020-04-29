using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Colors")]
    public class Color : DomainEntity<int>
    {
        public Color(int id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        public Color(string name, string code)
        {
            Name = name;
            Code = code;
        }

        [StringLength(250)]
        public string Name
        {
            get; set;
        }

        [StringLength(250)]
        public string Code { get; set; }
    }
}