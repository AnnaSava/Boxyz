using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Entities
{
    public class BoxShapeBoard : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<BoxShapeBoardCulture> Cultures { get; set; }

        public long? ParentBoardId { get; set; }

        public virtual BoxShapeBoard ParentBoard { get; set; }

        public virtual ICollection<BoxShapeBoard> ChildBoards { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }
    }
}
