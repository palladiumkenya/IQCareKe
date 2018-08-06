using Application.Presentation;
using Interface.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.HIVEducation
{
    public class HIVEducationLogic
    {
        //private int result = 0;
        //private int encounterTypeId = 0;
   
    public DataTable getCounsellingTopics(string counsellingtopics)
    {
        IHIVEducation hiveducation = (IHIVEducation)ObjectFactory.CreateInstance("BusinessProcess.CCC.HIVEducation.BHIVEducation, BusinessProcess.CCC.HIVEducation");
        return hiveducation.getCounsellingTopics(counsellingtopics);

    }
    }
}
