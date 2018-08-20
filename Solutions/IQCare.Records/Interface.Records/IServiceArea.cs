using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
   public   interface IServiceArea
    {
        ServiceArea GetServiceArea(string name);
    }
}
