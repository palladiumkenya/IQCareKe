using System.Collections.Generic;
using Entities.CCC.PSmart;

namespace Interface.WebApi
{
    public interface IPsmartShrManager
    {
        PATIENTIDENTIFICATION GetPatientidentification(int personId);
        EXTERNALPATIENTID GetExternalpatientid(int personId);
        List<INTERNALPATIENTID> GetInternalpatientids(int personId);
        PATIENTNAME GetPatientname(int personId);
        PHYSICALADDRESS GetPatientPhysicaladdress(int personId);
        PATIENTADDRESS GetPatientaddress(int personId);
        MOTHERNAME GetPatientMothername(int personId);
        List<MOTHERIDENTIFIER> GetPatientMotheridentifier(int personId);
        MOTHERDETAILS GetPatientMotherdetails(int personId);
        NOKNAME GetPatientNokname(int personId);
        List<NEXTOFKIN> GetPatientNextofkin(int personId);
        PROVIDERDETAILS GetPatientProviderdetails(int personId);
       List<HIVTEST>  GetPatientHivtest(int personId);
       List<IMMUNIZATION>  GetPatientImmunization(int personId);
        CARDDETAILS GetPatientCarddetails(int personId);
    }
}