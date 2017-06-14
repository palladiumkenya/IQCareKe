using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class ServiceAreaIdentifiersManager
    {
        private IServiceAreaIdentifiers mgr =
            (IServiceAreaIdentifiers) ObjectFactory.CreateInstance(
                "BusinessProcess.CCC.Enrollment.BServiceAreaIdentifiers, BusinessProcess.CCC");

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
