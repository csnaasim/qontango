using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Model
{
    public class EntryLog
    {
            public int CreatedBy { get; set; }
            public DateTime CreateDate { get; set; }
            public int UpdatedBy { get; set; }
            public DateTime UpdatedDate { get; set; }
        
    }
}
