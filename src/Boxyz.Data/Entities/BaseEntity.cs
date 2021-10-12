using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Entities
{
    public interface IVersion
    {
        public long ContentId { get; set; }

        public DateTime Created { get; set; }
    }

    public abstract class BaseEntity
    {
        public long Id { get; set; }
    }
}
