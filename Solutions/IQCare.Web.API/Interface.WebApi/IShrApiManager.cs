using Entities.CCC.psmart;
using Entities.PSmart;

namespace Interface.WebApi
{
    public interface IShrApiManager
    {
        SHR GetShrByPatientId(int patientId);
        SHR GetShrBtPatientCardSerialNumber(string serial);

        void LogPSmartRequest(TransactionLog transactionLog);
    }
}