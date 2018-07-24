using Entities.CCC.PSmart;
using Entities.PSmart;

namespace Interface.WebApi
{
    public interface IShrApiManager
    {
        SHR GetShrByPatientId(int patientId);
        SHR GetShrBtPatientCardSerialNumber(string serial);

        int LogPSmartRequest(TransactionLog transactionLog);
        int UpdateRequestLog(TransactionLog transactionLog);
    }
}