using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.HIVEducation
{
    public interface IHIVEducation
    {
        DataTable getCounsellingTopics(string counsellingtopics);
    }
}
