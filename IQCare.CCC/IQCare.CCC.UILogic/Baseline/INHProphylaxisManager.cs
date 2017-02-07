using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Baseline
{
    public class INHProphylaxisManager
    {
        private IINHProphylaxisManager _mgr = (IINHProphylaxisManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BINHProphylaxis, BusinessProcess.CCC");
        private int _retval;

        public int addINHProphylaxis(INHProphylaxis iNHProphylaxis)
        {
            return _retval = _mgr.AddINHProphylaxis(iNHProphylaxis);
        }

    }
}
