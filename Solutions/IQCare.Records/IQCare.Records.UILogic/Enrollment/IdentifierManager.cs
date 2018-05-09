using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Presentation;
using BusinessProcess.Records.Enrollment;
using Interface.Records.Enrollment;
using Entities.Records.Enrollment;
namespace IQCare.Records.UILogic.Enrollment
{
   public class IdentifierManager
    {
        private IIdentifierManager
              mgr = (IIdentifierManager)
              ObjectFactory.CreateInstance
              ("BusinessProcess.Records.Enrollment.BIdentifier,BusinessProcess.Records");



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

        public Identifier GetIdentifierByCode(string code)
        {
            return mgr.GetIdentifierByCode(code);
        }
       public List<Identifier> GetMultipleIdentifierByCode(string code)
        {
            return mgr.GetMultipleIdentifierByCode(code);
        }
    }
}
