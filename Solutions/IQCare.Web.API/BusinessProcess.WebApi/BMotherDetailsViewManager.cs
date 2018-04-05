using System;
using System.Linq;
using System.Security.Permissions;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Interface;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BMotherDetailsViewManager : ProcessBase, IMotherDetailsViewManager
    {

        public string FindExistingMother(MotherDetailsView motherdetails)
        {
            try
            {
                string ptnPk = "";
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var motherDetailsView = unitOfWork.MotherDetailsViewRepository.FindBy(x => x.Ptn_pk==motherdetails.Ptn_pk && x.firstName==motherdetails.firstName && x.lastName==motherdetails.lastName).FirstOrDefault();
                    unitOfWork.Dispose();
                    if (motherDetailsView != null )
                    {
                        ptnPk= motherDetailsView.Ptn_pk.ToString();
                    }
                }
                return ptnPk;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}