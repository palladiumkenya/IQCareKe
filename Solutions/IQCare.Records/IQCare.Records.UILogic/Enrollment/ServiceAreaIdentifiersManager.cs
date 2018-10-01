using Application.Presentation;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic.Enrollment
{
    public class ServiceAreaIdentifiersManager
    {
        private IServiceAreaIdentifiers mgr =
            (IServiceAreaIdentifiers)ObjectFactory.CreateInstance(
                "BusinessProcess.Records.Enrollment.BServiceAreaIdentifiers, BusinessProcess.Records");

        public List<ServiceAreaIdentifiers> GetIdentifiersByServiceArea(int serviceArea)
        {
            try
            {
                return mgr.GetIdentifiersByServiceArea(serviceArea);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
