using System.Collections.Generic;
using Entities.CCC.psmart;

namespace Interface.WebApi
{
    public interface IBPsmartShrCardSerialManager
    {
        PATIENTIDENTIFICATION GetPatientidentification(string cardSerial);
        EXTERNALPATIENTID GetExternalpatientid(string cardSerial);
        List<INTERNALPATIENTID> GetInternalpatientids(string cardSerial);
        PATIENTNAME GetPatientname(string cardSerial);
        PHYSICALADDRESS GetPatientPhysicaladdress(string cardSerial);
        PATIENTADDRESS GetPatientaddress(string cardSerial);
        MOTHERNAME GetPatientMothername(string cardSerial);
        List<MOTHERIDENTIFIER> GetPatientMotheridentifier(string cardSerial);
        MOTHERDETAILS GetPatientMotherdetails(string cardSerial);
        NOKNAME GetPatientNokname(string cardSerial);
        List<NEXTOFKIN> GetPatientNextofkin(string cardSerial);
        PROVIDERDETAILS GetPatientProviderdetails(string cardSerial);
        List<HIVTEST> GetPatientHivtest(string cardSerial);
        List<IMMUNIZATION> GetPatientImmunization(string cardSerial);
        CARDDETAILS GetPatientCarddetails(string cardSerial);
    }
}