using Entities.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Baseline
{
    public interface IINHProphylaxisManager
    {
        int AddINHProphylaxis(INHProphylaxis iNHProphylaxis);
        int UpdateINHProphylaxis(INHProphylaxis iNHProphylaxis);
        int DeleteINHProphylaxis(int id);
    }
}
