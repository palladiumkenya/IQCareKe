using System;
using IQCare.DTO;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPharmacyDispense
    {
        private string Msg { get; set; }

        public string Process(DtoDrugDispensed drugDispensed)
        {
            try
            {
                Msg = "Sucess";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Msg = "error " + e.Message;
            }
            return Msg;
        }
    }
}