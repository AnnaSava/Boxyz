using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public interface ICultureEntity
    {
        public long ContentId { get; set; }

        public string Culture { get; set; }
    }

    public abstract class BaseCultureEntity<TContent> : ICultureEntity 
        where TContent : BaseEntity
    {
        [Key]
        public long ContentId { get; set; }

        [Key]
        public string Culture { get; set; }

        public virtual TContent Content { get; set; }
    }
}
