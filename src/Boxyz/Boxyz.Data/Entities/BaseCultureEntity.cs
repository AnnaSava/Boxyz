using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public abstract class BaseCultureEntity
    {
        [Key]
        public string Culture { get; set; }
    }
}
