using System;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.psmart;
using Interface.CCC.psmart;

namespace BusinessProcess.CCC.psmart
{
    public class Bshr : ProcessBase, IShrManager
    {
        public SHR GetCLientShrByCardSerialNUmber(string serialNumber)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    SHR clientShr = unitOfWork.ShrRepository
                        .FindBy(x => x.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID == serialNumber).FirstOrDefault();
                    unitOfWork.Dispose();
                    return clientShr;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new System.Exception(ex.Message);
            }

        }

        public SHR GetClientShrByPatientId(int patientId)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var clientShr = unitOfWork.ShrRepository
                        .FindBy(x => x.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID == patientId.ToString()).FirstOrDefault();
                    unitOfWork.Dispose();
                    return clientShr;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new System.Exception(ex.Message);
            }
                
        }
    }
}