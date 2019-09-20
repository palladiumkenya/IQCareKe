using IQCare.Library;
using IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using IQCare.Pharm.BusinessProcess.Services;

namespace IQCare.Pharm.BusinessProcess.CommandHandlers.PatientPharmacy
{
    public class SaveUpdatePatientPharmacyCommandHandler : IRequestHandler<SaveUpdatePatientPharmacyCommand, Result<SaveUpdatePharmacyResponse>>
    {
        public int pharmacypk { get; set; }
        IPharmUnitOfWork _pharmUnitOfWork;
        public SaveUpdatePatientPharmacyCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmUnitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public async Task<Result<SaveUpdatePharmacyResponse>> Handle(SaveUpdatePatientPharmacyCommand request, CancellationToken cancellationToken)
        {


            using (_pharmUnitOfWork)
            {
                try
                {
                    PharmacyService phar = new PharmacyService(_pharmUnitOfWork);

                    if (request.PrescriptionDetails.Count > 0)
                    {

                        foreach (var det in request.PrescriptionDetails)
                        {
                            DrugPrescription dr = new DrugPrescription();
                            dr.DrugId = det.DrugId;
                            dr.DrugAbbr = det.DrugAbb;
                            dr.Dose = det.Dose;
                            dr.BatchId = det.batchId;
                            dr.FreqId = det.Freq;
                            dr.Evening = det.Evening;
                            dr.Midday = det.Midday;
                            dr.Morning = det.Morning;
                            dr.Night = det.Night;
                            dr.Duration = det.Duration;
                            dr.prophylaxis = det.Prophylaxis;
                            dr.qtyDisp = det.QUantityDisp;
                            dr.qtyPres = det.QuantityPres;

                            List<DrugPrescription> prs = new List<DrugPrescription>();
                            prs.Add(dr);
                            var pk = await phar.SaveUpdatePharmacy(Convert.ToInt32(request.Ptn_Pk),Convert.ToInt32(request.PatientMasterVisitId)
                                  , Convert.ToInt32(request.PatientId), Convert.ToInt32(request.LocationId),
                                   request.PrescribedBy, Convert.ToInt32(request.UserId), det.Regimentext, request.DispensedBy,
                                  Convert.ToInt32(det.Regimenline), 0, prs, request.pmscm.ToString(),
                                  Convert.ToInt32(det.TreatmentProgram),
                                  Convert.ToInt32(det.Period), Convert.ToInt32(det.TreatmentPlan),
                                  Convert.ToInt32(det.Reason), Convert.ToInt32(det.Regimen), request.PrescriptionDate, request.DispensedDate,request.VisitDate);


                            pharmacypk = Convert.ToInt32(pk);

                        }

                    }
                    return Result<SaveUpdatePharmacyResponse>.Valid(new SaveUpdatePharmacyResponse()
                    {
                        Ptn_Pharmacy_Pk = pharmacypk

                    });




                }


                catch (Exception ex)
                {


                    return Result<SaveUpdatePharmacyResponse>.Invalid("Error has occured while saving the form"
                        + ex.Message.ToString());
                    
                }
                

            }
        }
    }
}

