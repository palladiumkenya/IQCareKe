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
    public class IdentifierManager
    {
        private IIdentifiersManager mgr =
            (IIdentifiersManager) ObjectFactory.CreateInstance(
                "BusinessProcess.CCC.Enrollment.BIdentifier, BusinessProcess.CCC");

        public List<Identifier> GetIdentifiersById(int identifierId)
        {
            try
            {
                return mgr.GetIdentifiersById(identifierId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Identifier> GetAllIdentifiers()
        {
            try
            {
                return mgr.GetAllIdentifiers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
