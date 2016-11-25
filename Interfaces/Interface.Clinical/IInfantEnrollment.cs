using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IInfantEnrollment
    {
        
        DataSet GetExposedInfantByParentId(int Ptn_Pk);
        int DeleteExposedInfantById(int Id);
        int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
            string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate, int UserID);
     

        DataSet CheckIdentity(string ExposedInfantId);



    }
}
