using System.Collections.Generic;

namespace Nodes.Entities
{
    public class AsnBodyEntity : AsnHeaderEntity
    {
        public List<PODetailEntity> Details
        {
            get;
            set;
        }
    }
}
