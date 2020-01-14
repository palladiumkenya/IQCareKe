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
using System.Data;

using System.Linq;
namespace IQCare.Pharm.BusinessProcess.CommandHandlers.PatientPharmacy
{
    public class GetPatientPharmacyDetailsCommandHandler : IRequestHandler<GetPatientPharmacyDetailsCommand, Result<GetPatientPharmacyResponse>>
    {
        IPharmUnitOfWork _pharmunitOfWork;
        public List<DrugDetails> presc = new List<DrugDetails>();

        public DateTime? dispensedDate { get; set; }
        public DateTime? orderedDate { get; set; }

        public DateTime? visitDate { get; set; }
        public GetPatientPharmacyDetailsCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmunitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public async Task<Result<GetPatientPharmacyResponse>> Handle(GetPatientPharmacyDetailsCommand request, CancellationToken cancellationToken)
        {

            using (_pharmunitOfWork)
            {
                try
                {


                    //StringBuilder sql = new StringBuilder();
                    //sql.Append($"select * from [dbo].[PharmacyDrugVisitDetailsView] where PatientMasterVisitId='{request.PatientMasterVisitId }'");



                    /* StringBuilder sqldel = new StringBuilder();
                     sqldel.Append("[dbo].[pr_Pharmacy_GetExistPharmacyDrugVisitDetails]  @PatientMasterVisitId");
                     var patientmastervisitid = new SqlParameter();
                     patientmastervisitid.SqlDbType = SqlDbType.Int;
                     patientmastervisitid.ParameterName = "@PatientMasterVisitId";
                     patientmastervisitid.Size = -1;
                     patientmastervisitid.Value = Convert.ToInt32(request.PatientMasterVisitId); */
                    //var patientmastervisitid = new SqlParameter("@PatientMasterVisitId", request.PatientMasterVisitId);
                    /*  var pharmacyvisit = await _pharmunitOfWork.Repository<PharmacyDrugVisitDetails>().FromSql(sqldel.ToString(), parameters: new[] {
                      patientmastervisitid
              });*/
                    var pharmacyorder = await _pharmunitOfWork.Repository<PatientPharmacyOrder>().Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId).ToListAsync();

                    if(pharmacyorder.Count > 0)
                    {

                        dispensedDate = pharmacyorder[0].DispensedByDate;
                        orderedDate = pharmacyorder[0].OrderedbyDate;

                        StringBuilder sql = new StringBuilder();
                        sql.Append($"SELECT  [Visit_Id],[Ptn_Pk],[LocationID],[VisitDate], Convert(Int,[VisitType]) as VisitType, DataQuality,[DeleteFlag],[UserID] ,[CreateDate] ,[CreatedBy] FROM [dbo].[ord_Visit] where Visit_Id='{pharmacyorder[0].VisitId}' and (DeleteFlag is null or DeleteFlag=0) ");

                        var visit = await _pharmunitOfWork.Repository<PatientVisit>().FromSql(sql.ToString());

                        if (visit != null)
                        {
                            if (visit.Count > 0)
                            {
                                visitDate = visit[0].VisitDate;
                            }
                        }
                        foreach (var phar in pharmacyorder )
                        {
                            StringBuilder sqlptn = new StringBuilder();
                            sqlptn.Append($"select * from PharmacyDrugVisitDetailsView where ptn_pharmacy_pk='{phar.ptn_pharmacy_pk}'");
                            var pharmacylist = _pharmunitOfWork.Repository<PharmacyDrugVisitDetails>().Get(x=>x.ptn_pharmacy_pk == phar.ptn_pharmacy_pk).ToList();

                           
                            if (pharmacylist != null)
                            {
                                if (pharmacylist.Count > 0)
                                {


                                    pharmacylist.ForEach(x =>
                                    {
                                        DrugDetails pds = new DrugDetails();
                                        pds.ptn_pharmacy_pk = x.ptn_pharmacy_pk;
                                        pds.DrugName = x.DrugName;
                                        pds.DrugId = Convert.ToString(x.Drug_pk);
                                        pds.DrugAbb = x.Abbreviation;
                                        pds.batchId = Convert.ToString(x.BatchNo);
                                        pds.batchText = x.BatchName;
                                        pds.Dose = Convert.ToString(x.SingleDose);
                                        pds.Freq = Convert.ToString(x.FrequencyID);
                                        pds.FreqText = x.FrequencyName;
                                        pds.Duration = Convert.ToString(x.Duration);
                                        pds.QUantityDisp = Convert.ToString(x.DispensedQuantity);
                                        pds.QuantityPres = Convert.ToString(x.OrderedQuantity);
                                        pds.Reason = Convert.ToString(x.TreatmentPlanReasonId);
                                        pds.ReasonText = Convert.ToString(x.TreatmentPlanReason);
                                        pds.Regimen = Convert.ToString(x.RegimenId);
                                        pds.Regimentext = Convert.ToString(x.Regimen);
                                        pds.Regimenline = Convert.ToString(x.RegimenLineId);
                                        pds.Regimenlinetext = Convert.ToString(x.RegimenLine);
                                        pds.TreatmentPlan = Convert.ToString(x.TreatmentPlan);
                                        pds.TreatmentPlantext = Convert.ToString(x.TreatmentPlanText);
                                        pds.TreatmentProgram = Convert.ToString(x.ProgID);
                                        pds.TreatmentProgramText = Convert.ToString(x.TreatmentProgram);
                                        pds.Morning = Convert.ToString(x.MorningDose);
                                        pds.Evening = Convert.ToString(x.EveningDose);
                                        pds.Night = Convert.ToString(x.EveningDose);
                                        pds.Midday = Convert.ToString(x.MiddayDose);
                                        pds.Night = Convert.ToString(x.NightDose);
                                        pds.Period = Convert.ToString(x.PeriodTaken);
                                        pds.PeriodTakentext = Convert.ToString(x.PeriodTakenText);
                                        pds.Prophylaxis = Convert.ToString(x.Prophylaxis);

                                        presc.Add(pds);
                                    });

                                }
                            }
                        }
                    }
                    //   var pharmacylist = await _pharmunitOfWork.Repository<PharmacyDrugVisitDetails>().GetAllAsync();
                    //var pharmacyvisit = pharmacylist.Where(x => x.PatientMasterVisitId == request.PatientMasterVisitId  && x.ptn_pharmacy_pk.ToString() == "156872").ToList();
                    //var pharmacyvisit = await _pharmunitOfWork.Repository<PharmacyDrugVisitDetails>().FromSql(sql.ToString());

                    /*  if (pharmacyvisit.Count > 0)
                      {

                          dispensedDate = pharmacyvisit[0].DispensedByDate;
                          orderedDate = pharmacyvisit[0].OrderedByDate;
                          visitDate = pharmacyvisit[0].VisitDate;

                          pharmacyvisit.ForEach(  x =>
                          {
                              DrugDetails pds = new DrugDetails();
                              pds.ptn_pharmacy_pk = x.ptn_pharmacy_pk;
                              pds.DrugName = x.DrugName;
                              pds.DrugId = Convert.ToString(x.Drug_pk);
                              pds.DrugAbb = x.Abbreviation;
                              pds.batchId = Convert.ToString(x.BatchNo);
                              pds.batchText = x.BatchName;
                              pds.Dose = Convert.ToString(x.SingleDose);
                              pds.Freq = Convert.ToString(x.FrequencyID);
                              pds.FreqText = x.FrequencyName;
                              pds.Duration = Convert.ToString(x.Duration);
                              pds.QUantityDisp = Convert.ToString(x.DispensedQuantity);
                              pds.QuantityPres = Convert.ToString(x.OrderedQuantity);
                              pds.Reason = Convert.ToString(x.TreatmentPlanReasonId);
                              pds.ReasonText = Convert.ToString(x.TreatmentPlanReason);
                              pds.Regimen = Convert.ToString(x.RegimenId);
                              pds.Regimentext = Convert.ToString(x.Regimen);
                              pds.Regimenline = Convert.ToString(x.RegimenLineId);
                              pds.Regimenlinetext = Convert.ToString(x.RegimenLine);
                              pds.TreatmentPlan = Convert.ToString(x.TreatmentPlan);
                              pds.TreatmentPlantext = Convert.ToString(x.TreatmentPlanText);
                              pds.TreatmentProgram = Convert.ToString(x.ProgID);
                              pds.TreatmentProgramText = Convert.ToString(x.TreatmentProgram);
                              pds.Morning = Convert.ToString(x.MorningDose);
                              pds.Evening = Convert.ToString(x.EveningDose);
                              pds.Night = Convert.ToString(x.EveningDose);
                              pds.Midday = Convert.ToString(x.MiddayDose);
                              pds.Night = Convert.ToString(x.NightDose);
                              pds.Period = Convert.ToString(x.PeriodTaken);
                              pds.PeriodTakentext = Convert.ToString(x.PeriodTakenText);
                              pds.Prophylaxis = Convert.ToString(x.Prophylaxis);

                              presc.Add(pds);
                          }); 



                      }*/

                    return Result<GetPatientPharmacyResponse>.Valid(new GetPatientPharmacyResponse()
                    {
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        PatientId = request.PatientId,
                         DispensedDate=dispensedDate,
                         VisitDate=visitDate,
                         OrderedByDate =orderedDate,
                        DrugDetails = presc

                    });


                }
                catch (Exception ex)
                {
                    return Result<GetPatientPharmacyResponse>.Invalid("Error getting the drug records for this visit " + ex.Message);

                }
            }
        }
    }
}
