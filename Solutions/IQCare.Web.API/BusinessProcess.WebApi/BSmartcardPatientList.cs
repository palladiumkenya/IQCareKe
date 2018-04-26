using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entity.WebApi.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BSmartcardPatientList : ProcessBase, ISmartcardPatientListManager
    {
       // private List<SmartCardPatientList> _eligible;


        public List<PsmartEligibleList>  GetSmartCardPatientList()
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var eligible = unitOfWork.SmartCardPatientListRepository.FindBy(x => x.PATIENTID > 0)
                        .Select(x => new PsmartEligibleList()
                        {
                           PATIENTID = x.PATIENTID,
                           FIRSTNAME = x.FIRSTNAME,
                           MIDDLENAME = x.MIDDLENAME,
                           LASTNAME  = x.LASTNAME,
                           GENDER = x.GENDER,
                           AGE = x.AGE
                        }).ToList();
                        
                    unitOfWork.Dispose();
                    return eligible;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message+e.InnerException);
            }
        }
    }
}