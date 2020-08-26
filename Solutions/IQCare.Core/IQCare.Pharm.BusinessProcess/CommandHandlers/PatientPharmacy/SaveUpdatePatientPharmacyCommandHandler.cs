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
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using IQCare.Prep.Core.Models;

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
                   
                   
                    List<PharmacyDetails> pharmlist = new List<PharmacyDetails>();
                     PharmacyService phar = new PharmacyService(_pharmUnitOfWork);
                    List<string> Pk = new List<string>();
                    if (request.PrescriptionDetails.Count > 0)
                    {
                        request.PrescriptionDetails.ForEach(x =>
                        {
                            if (String.IsNullOrEmpty(x.Reason) == true)
                            {
                                x.Reason = "0";
                            }
                            if (String.IsNullOrEmpty(x.Period) == true)
                            {
                                x.Period = "0";
                            }
                        });

                        var TreatmentProgram = request.PrescriptionDetails.Select(x => new { x.TreatmentPlan, x.TreatmentProgram,x.Reason, x.Regimenline,x.Regimentext, x.Regimen,x.Period }).Distinct().ToList();
                        // .Select(x => x.TreatmentProgram).Distinct().ToList();
                       
                        
                        if (TreatmentProgram.Count > 0)
                        {
                            
                            var FirstTreatmentProgram = TreatmentProgram[0].TreatmentProgram;

                            TreatmentProgram.ForEach(t =>
                            {
                                List<DrugPrescription> presc = new List<DrugPrescription>();
                                PharmacyDetails pharmacyDetails = new PharmacyDetails();
                                pharmacyDetails.Regimen = t.Regimen;
                                pharmacyDetails.Regimentext = t.Regimentext;
                                pharmacyDetails.RegimenLine = t.Regimenline;
                                pharmacyDetails.TreatmentPlan = t.TreatmentPlan;
                                pharmacyDetails.TreatmentProgram = t.TreatmentProgram;
                                pharmacyDetails.Reason = t.Reason;
                                pharmacyDetails.Period = t.Period;
                                
                                request.PrescriptionDetails.ForEach(x =>
                                {




                                    if (x.TreatmentProgram.ToString() == t.TreatmentProgram.ToString())
                                    {
                                        DrugPrescription dr = new DrugPrescription();
                                        dr.DrugId = x.DrugId;
                                        dr.DrugAbbr = x.DrugAbb;
                                        dr.Dose = x.Dose;
                                        dr.BatchId = x.batchId;
                                        dr.FreqId = x.Freq;
                                        dr.Evening = x.Evening;
                                        dr.Midday = x.Midday;
                                        dr.Morning = x.Morning;
                                        dr.Night = x.Night;
                                        dr.Duration = x.Duration;
                                        dr.prophylaxis = x.Prophylaxis;
                                        dr.qtyDisp = x.QUantityDisp;
                                        dr.qtyPres = x.QuantityPres;

                                        presc.Add(dr);

                                       
                                    }
                                    if(String.IsNullOrEmpty(x.TreatmentProgram)== true )
                                    {
                                        if(t.TreatmentProgram.ToString()== FirstTreatmentProgram.ToString())
                                        {
                                            DrugPrescription dr = new DrugPrescription();
                                            dr.DrugId = x.DrugId;
                                            dr.DrugAbbr = x.DrugAbb;
                                            dr.Dose = x.Dose;
                                            dr.BatchId = x.batchId;
                                            dr.FreqId = x.Freq;
                                            dr.Evening = x.Evening;
                                            dr.Midday = x.Midday;
                                            dr.Morning = x.Morning;
                                            dr.Night = x.Night;
                                            dr.Duration = x.Duration;
                                            dr.prophylaxis = x.Prophylaxis;
                                            dr.qtyDisp = x.QUantityDisp;
                                            dr.qtyPres = x.QuantityPres;

                                            presc.Add(dr);
                                         
                                        }
                                    }



                                });

                                pharmacyDetails.DrugPrescriptions = presc;

                                pharmlist.Add(pharmacyDetails);

                              



                            });

                             Pk =  await phar.SaveUpdatePharmacy(Convert.ToInt32(request.Ptn_Pk), Convert.ToInt32(request.PatientMasterVisitId)
                          , Convert.ToInt32(request.PatientId), Convert.ToInt32(request.LocationId),
                           request.PrescribedBy, Convert.ToInt32(request.UserId), pharmlist, request.DispensedBy,

                               0,  request.pmscm.ToString(), request.PrescriptionDate, request.DispensedDate, request.VisitDate);


                        }


                    }
                    return Result<SaveUpdatePharmacyResponse>.Valid(new SaveUpdatePharmacyResponse()
                    {
                        Ptn_Pharmacy_Pk = Pk

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

