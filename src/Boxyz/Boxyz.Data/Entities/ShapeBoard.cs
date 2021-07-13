using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class ShapeBoard : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<ShapeBoardCulture> Cultures { get; set; }

        public long? ParentBoardId { get; set; }

        public virtual ShapeBoard ParentBoard { get; set; }

        public virtual ICollection<ShapeBoard> ChildBoards { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }
    }
}
