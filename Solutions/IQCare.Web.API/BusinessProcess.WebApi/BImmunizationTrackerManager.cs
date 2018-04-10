using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Interface.WebApi;

namespace BusinessProcess.WebApi
{
    public class BImmunizationTrackerManager:ProcessBase,IImmunizationTrackerManager
    {
        private int result = 0;

        public int AddImmunizationTracker(DateTime dateAdministered, string antigenAdministered, int personId, int ptnPk)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    if (antigenAdministered != null)
                    {
                        var immunizzation = unitOfWork.ImmunizationTrackerRepository.FindBy(x =>
                            x.AntigenAdministered == antigenAdministered && x.PersonId == personId &&
                            x.DateAdministered == dateAdministered).FirstOrDefault();
                        if (immunizzation != null)
                        {
                        }
                        else
                        {
                            var newImmunization = new ImmunizationTracker()
                            {
                                DateAdministered = Convert.ToDateTime(dateAdministered),
                                AntigenAdministered = antigenAdministered,
                                PersonId = personId,//immunization.PersonId,
                                PtnPk = personId
                            };
                            unitOfWork.ImmunizationTrackerRepository.Add(newImmunization);
                            result = unitOfWork.Complete();
                            unitOfWork.Dispose();
                        }
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int EditImmunizationTracker(ImmunizationTracker immunizationTracker)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.ImmunizationTrackerRepository.Update(immunizationTracker);
                    result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}