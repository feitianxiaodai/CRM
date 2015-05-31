using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Repository
{
    public class BaseDbContext:DbContext
    {
        public BaseDbContext()
            : base("name=CRMEntities")
        {
        }
    }
}
