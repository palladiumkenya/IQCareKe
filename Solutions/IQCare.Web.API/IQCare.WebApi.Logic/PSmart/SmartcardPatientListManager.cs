using Entity.WebApi.PSmart;
using Interface.WebApi;
using IQCare.DTO.PSmart;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic.PSmart
{
    public class SmartcardPatientListManager
    {
       
        private List<PsmartEligibleList> _patientLists;
        private readonly ISmartcardPatientListManager _smartcardPatientList = (ISmartcardPatientListManager)Application.Presentation.ObjectFactory.CreateInstance("BusinessProcess.WebApi.BSmartcardPatientList, BusinessProcess.WebApi");

        public List<DtoSmartcardPatientList> GetSmartcardPatientList()
        {
            _patientLists = _smartcardPatientList.GetSmartCardPatientList();

            List<DtoSmartcardPatientList> clientligibleList=new List<DtoSmartcardPatientList>();

            foreach (var _patientList in _patientLists)
            {
                var clientList=new DtoSmartcardPatientList()
                {
                    PATIENTID = _patientList.PATIENTID,
                    AGE = _patientList.AGE,
                    FIRSTNAME = _patientList.FIRSTNAME,
                    GENDER = _patientList.GENDER,
                    LASTNAME = _patientList.LASTNAME,
                    MIDDLENAME = _patientList.MIDDLENAME
                };
                clientligibleList.Add(clientList);

            }


            return clientligibleList;
        }
    }
}