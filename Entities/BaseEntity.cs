using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BaseEntity
    {
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
