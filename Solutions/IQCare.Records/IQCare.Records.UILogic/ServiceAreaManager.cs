using Application.Presentation;
using Entities.Records.Enrollment;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
    class ServiceAreaManager
    {

        private IServiceArea mgr =
            (IServiceArea)ObjectFactory.CreateInstance(
                "BusinessProcess.Records.BServiceArea, BusinessProcess.Records");

        public int GetServiceAreaById (string name)
        {
            try
            {
                ServiceArea sv = mgr.GetServiceArea(name);
                return sv.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
}
