using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.SCM
{
    public interface ILaboratory
    {
        DataTable GetLabList(int LabTestId);
        DataTable GetLabLocationList();
        int SaveUpdateLabConfiguration(DataTable dtLabConfig, int UserId);
    }
}
