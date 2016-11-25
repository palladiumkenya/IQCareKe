using System;
using System.Data;


namespace Interface.Clinical
{
    public interface IPatientTransfer
    {
        DataSet GetLatestTransferDate(int PatientId, int VisitID);
        DataTable GetSatelliteID(string PatientId);
        DataSet GetSatelliteLocation(string PatientId, string TransferId, int Flag, string SystemID);
        DataSet GetDataValidate(string PatientId, string transferdate);
        DataSet GetDateValidateBetween(string PatientId, string Existingdate);
        int SaveUpdate(string ID, string PatientId, string TransferfromId, string TransfertoId, string TransfertoDate, int userId, string createdate, int flag); 
    }
}
