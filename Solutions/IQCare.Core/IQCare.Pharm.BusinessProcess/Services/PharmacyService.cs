using IQCare.Pharm.Core.Models;
using IQCare.Pharm.Infrastructure;
using IQCare.Library;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace IQCare.Pharm.BusinessProcess.Services
{
    public class PharmacyService

    {
        private readonly IPharmUnitOfWork _unitOfWork;
        public int PatientPk { get; set; }

        public int PharmacyPk{ get; set; }
        public int VisitPk { get; set; }

        public int VisitTypeId { get; set; }
        public PharmacyService(IPharmUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<bool> HasPatientTreatmentStarted(int patientId)
        {
            try
            {
                var treatmentstarted = await _unitOfWork.Repository<PatientTreamentTrackerLookup>().Get(x => x.PatientId == patientId && x.Regimen != null).AnyAsync();

                return treatmentstarted;

            }
            catch (Exception e)

            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }



        }

        public async Task<int> SaveUpdatePharmacy(int ptn,int PatientMasterVisitID, int PatientId, int LocationID, int OrderedBy,
            int UserID, string RegimenType, int DispensedBy, int RegimenLine, int ModuleID,
            List<DrugPrescription> drugPrescription, string pmscmFlag, int TreatmentProgram,
            int PeriodTaken, int TreatmentPlan, int TreatmentPlanReason, int Regimen, string prescriptionDate,
            string dispensedDate,DateTime VisitDate)
        {

            try
            {
                var PatientPharmacy = await _unitOfWork.Repository<PatientPharmacyOrder>()
                    .Get(x => x.PatientId == PatientId && x.PatientMasterVisitId == PatientMasterVisitID && x.ProgId == TreatmentProgram && x.DeleteFlag != 1).OrderByDescending(x => x.ptn_pharmacy_pk).FirstOrDefaultAsync();

                if (PatientPharmacy != null)
                {

                    PatientPharmacy.OrderedBy = OrderedBy;
                    PatientPharmacy.OrderedbyDate = (prescriptionDate == "") ? (DateTime?)null : Convert.ToDateTime(prescriptionDate);
                    PatientPharmacy.DispensedBy = DispensedBy;
                    PatientPharmacy.DispensedByDate = (dispensedDate == "") ? (DateTime?)null : Convert.ToDateTime(dispensedDate);
                    PatientPharmacy.ProgId = TreatmentProgram;
                    PatientPharmacy.UserId = UserID;
                    PatientPharmacy.RegimenLine = RegimenLine;
                    PatientPharmacy.PharmacyPeriodTaken = PeriodTaken;

                    _unitOfWork.Repository<PatientPharmacyOrder>().Update(PatientPharmacy);
                    await _unitOfWork.SaveAsync();

                    PharmacyPk = PatientPharmacy.ptn_pharmacy_pk;
                    PatientPk = PatientPharmacy.Ptn_pk;
                    VisitPk = PatientPharmacy.VisitId;
                    var ArvTreatmentTracker = await _unitOfWork.Repository<ARVTreatmentTracker>().Get(x => x.PatientId == PatientId && x.PatientMasterVisitId == x.PatientMasterVisitId && x.DeleteFlag == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    if (ArvTreatmentTracker != null)
                    {
                        ArvTreatmentTracker.RegimenId = Regimen;
                        ArvTreatmentTracker.RegimenLineId = RegimenLine;
                        ArvTreatmentTracker.TreatmentStatusId = TreatmentPlan;
                        ArvTreatmentTracker.TreatmentStatusReasonId = TreatmentPlanReason;

                        _unitOfWork.Repository<ARVTreatmentTracker>().Update(ArvTreatmentTracker);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        ARVTreatmentTracker arv = new ARVTreatmentTracker();


                        arv.PatientId = PatientId;
                        arv.PatientMasterVisitId = PatientMasterVisitID;
                        arv.RegimenId = Regimen;
                        arv.RegimenLineId = RegimenLine;
                        arv.TreatmentStatusId = TreatmentPlan;
                        arv.TreatmentStatusReasonId = TreatmentPlanReason;
                        arv.DeleteFlag = false;
                        arv.CreateBy = UserID;
                        arv.CreateDate = DateTime.Now;

                        await _unitOfWork.Repository<ARVTreatmentTracker>().AddAsync(arv);
                        await _unitOfWork.SaveAsync();

                    }

                    var regimenmap = await _unitOfWork.Repository<RegimenMap>().Get(x => x.Ptn_Pk == PatientPk && x.OrderId == PharmacyPk && x.DeleteFlag == 0).OrderByDescending(x => x.RegimenMap_Pk).FirstOrDefaultAsync();
                    if (regimenmap != null)
                    {
                        regimenmap.RegimenType = RegimenType;

                        _unitOfWork.Repository<RegimenMap>().Update(regimenmap);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        RegimenMap rm = new RegimenMap();
                        rm.Ptn_Pk = PatientPk;
                        rm.LocationID = LocationID;
                        rm.RegimenType = rm.RegimenType;
                        rm.Visit_pk = VisitPk;
                        rm.UserId = UserID;
                        rm.OrderId = PharmacyPk;
                        rm.CreateDate = DateTime.Now;
                        await _unitOfWork.Repository<RegimenMap>().AddAsync(rm);
                        await _unitOfWork.SaveAsync();


                    }
                    StringBuilder sqlartstartdate = new StringBuilder();
                    sqlartstartdate.Append("Select Top 1  a.DispensedByDate as DispensedDate From  ord_PatientPharmacyOrder a,dtl_RegimenMap b  " +
                       "  Where a.ptn_pk = b.Ptn_Pk And a.ptn_pharmacy_pk = b.orderid And(a.DeleteFlag = 0 Or a.DeleteFlag Is Null) And(b.DeleteFlag = 0 Or b.DeleteFlag Is Null)" +
                       "  And a.ptn_pk = @pk And a.ProgId In(222, 223)   And a.dispensedbydate Is Not Null And b.regimentype Is Not Null And b.regimentype <> ''''  " +
                       "  Order By a.dispensedbydate Asc ");

                    var ptnPk = new SqlParameter();
                    ptnPk.SqlDbType = SqlDbType.Int;
                    ptnPk.Size = -1;
                    ptnPk.ParameterName = "@pk";
                    ptnPk.Value = PatientPk;
                   

                    var PatientARTStartDate = _unitOfWork.Context.Query<PatientARTStartDate>().FromSql(sqlartstartdate.ToString(),
                      parameters: new[]
                      {
                            ptnPk
                      }).FirstOrDefault();

                    if (PatientARTStartDate != null)
                    {
                        var patient = await _unitOfWork.Repository<Patient>().Get(x => x.Ptn_Pk == PatientPk).FirstOrDefaultAsync();
                        if (patient != null)
                        {
                            patient.ARTStartDate = Convert.ToDateTime(PatientARTStartDate.DispensedDate);
                            _unitOfWork.Repository<Patient>().Update(patient);
                            await _unitOfWork.SaveAsync();
                        }
                    }

                }
                else
                {
                   var  VisitType = _unitOfWork.Repository<VisitType>().Get(x => x.VisitName == "Pharmacy" && x.DeleteFlag != 1).FirstOrDefault();
                    if (VisitType != null)
                    {
                        VisitTypeId = VisitType.VisitTypeID;

                    }

                    PatientVisit pvv = new PatientVisit();
                    pvv.Ptn_pk = ptn;
                    pvv.LocationID = LocationID;
                    pvv.VisitDate = Convert.ToDateTime(VisitDate);
                    pvv.VisitType = VisitTypeId;
                    pvv.DataQuality = 1;
                    pvv.DeleteFlag = 0;
                    pvv.UserID = UserID;
                    pvv.CreateDate = DateTime.Now;
                    pvv.CreatedBy = UserID;

                    

                   await  _unitOfWork.Repository<PatientVisit>().AddAsync(pvv);
                   await _unitOfWork.SaveAsync();

                    VisitPk = pvv.Visit_Id;

                    PatientPharmacyOrder pho = new PatientPharmacyOrder();
                    pho.Ptn_pk = pvv.Ptn_pk;
                    pho.PatientId = PatientId;
                    pho.PatientMasterVisitId = PatientMasterVisitID;
                    pho.LocationID = LocationID;
                    pho.OrderedBy = OrderedBy;
                    pho.OrderedbyDate = (prescriptionDate == "") ? (DateTime?)null : Convert.ToDateTime(prescriptionDate);
                    pho.DispensedBy = DispensedBy;
                    pho.DispensedByDate = (dispensedDate == "") ? (DateTime?)null : Convert.ToDateTime(dispensedDate);
                    pho.ProgId = TreatmentProgram;
                    pho.UserId = UserID;
                    pho.RegimenLine = RegimenLine;
                    pho.PharmacyPeriodTaken = PeriodTaken;
                    pho.VisitId =pvv.Visit_Id ;
                    pho.ProviderId = TreatmentProgram;
                    pho.CreateDate = DateTime.Now;


                    await _unitOfWork.Repository<PatientPharmacyOrder>().AddAsync(pho);
                    await _unitOfWork.SaveAsync();
                  
                    PharmacyPk = pho.ptn_pharmacy_pk;
                    PatientPk = pho.Ptn_pk;
                    VisitPk = pho.VisitId;
                    var ArvTreatmentTracker = await _unitOfWork.Repository<ARVTreatmentTracker>().Get(x => x.PatientId == PatientId && x.PatientMasterVisitId == x.PatientMasterVisitId && x.DeleteFlag == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    if (ArvTreatmentTracker != null)
                    {
                        ArvTreatmentTracker.RegimenId = Regimen;
                        ArvTreatmentTracker.RegimenLineId = RegimenLine;
                        ArvTreatmentTracker.TreatmentStatusId = TreatmentPlan;
                        ArvTreatmentTracker.TreatmentStatusReasonId = TreatmentPlanReason;

                        _unitOfWork.Repository<ARVTreatmentTracker>().Update(ArvTreatmentTracker);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        ARVTreatmentTracker arv = new ARVTreatmentTracker();


                        arv.PatientId = PatientId;
                        arv.PatientMasterVisitId = PatientMasterVisitID;
                        arv.RegimenId = Regimen;
                        arv.RegimenLineId = RegimenLine;
                        arv.TreatmentStatusId = TreatmentPlan;
                        arv.TreatmentStatusReasonId = TreatmentPlanReason;
                        arv.DeleteFlag = false;
                        arv.CreateBy = UserID;
                        arv.CreateDate = DateTime.Now;

                        await _unitOfWork.Repository<ARVTreatmentTracker>().AddAsync(arv);
                        await _unitOfWork.SaveAsync();

                    }

                    var regimenmap = await _unitOfWork.Repository<RegimenMap>().Get(x => x.Ptn_Pk == PatientPk && x.OrderId == PharmacyPk && x.DeleteFlag == 0).OrderByDescending(x => x.RegimenMap_Pk).FirstOrDefaultAsync();
                    if (regimenmap != null)
                    {
                        regimenmap.RegimenType = RegimenType;

                        _unitOfWork.Repository<RegimenMap>().Update(regimenmap);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        RegimenMap rm = new RegimenMap();
                        rm.Ptn_Pk = PatientPk;
                        rm.LocationID = LocationID;
                        rm.RegimenType = rm.RegimenType;
                        rm.Visit_pk = VisitPk;
                        rm.UserId = UserID;
                        rm.OrderId = PharmacyPk;
                        rm.CreateDate = DateTime.Now;
                        await _unitOfWork.Repository<RegimenMap>().AddAsync(rm);
                        await _unitOfWork.SaveAsync();


                    }
                    StringBuilder sqlartstartdate = new StringBuilder();
                    sqlartstartdate.Append("Select Top 1  a.DispensedByDate as DispensedDate From  ord_PatientPharmacyOrder a,dtl_RegimenMap b  " +
                       "  Where a.ptn_pk = b.Ptn_Pk And a.ptn_pharmacy_pk = b.orderid And(a.DeleteFlag = 0 Or a.DeleteFlag Is Null) And(b.DeleteFlag = 0 Or b.DeleteFlag Is Null)" +
                       "  And a.ptn_pk = @pk And a.ProgId In(222, 223)   And a.dispensedbydate Is Not Null And b.regimentype Is Not Null And b.regimentype <> ''''  " +
                       "  Order By a.dispensedbydate Asc ");

                    var ptnPk = new SqlParameter();
                    ptnPk.SqlDbType = SqlDbType.Int;
                    ptnPk.ParameterName = "@pk";
                    ptnPk.Size = -1;
                    ptnPk.Value = Convert.ToInt32(PatientPk);

                    var PatientARTStartDate = _unitOfWork.Context.Query<PatientARTStartDate>().FromSql(sqlartstartdate.ToString(),
                      parameters: new[]
                      {
                            ptnPk
                      }).FirstOrDefault();

                    if (PatientARTStartDate != null)
                    {
                        var patient = await _unitOfWork.Repository<Patient>().Get(x => x.Ptn_Pk == PatientPk).FirstOrDefaultAsync();
                        if (patient != null)
                        {
                            patient.ARTStartDate = Convert.ToDateTime(PatientARTStartDate.DispensedDate);
                            _unitOfWork.Repository<Patient>().Update(patient);
                            await _unitOfWork.SaveAsync();
                        }
                    }



                }


                string ptn_pharmacy_pk = PharmacyPk.ToString();

                StringBuilder sqldel = new StringBuilder();
                sqldel.Append("sp_DeletePharmacyPrescription_GreenCard @ptn_pharmacy_pk");
                var ptnpk = new SqlParameter("@ptn_pharmacy_pk", ptn_pharmacy_pk);
                var k = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqldel.ToString(), parameters: new[] {
                    ptnpk
            });

                foreach (var drg in drugPrescription)
                {
                    if (drg.DrugId != "")
                    {
                        if (drg.qtyDisp == "")
                            drg.qtyDisp = "0";

                        StringBuilder sqlprescph = new StringBuilder();
                        sqlprescph.Append("sp_SaveUpdatePharmacyPrescription_GreenCard @ptn_pharmacy_pk,@DrugId,@BatchId,@FreqId,@Dose,@Morning," +
                            "@Midday,@Evening,@Night,@Duration,@qtyPres,@qtyDisp,@prophylaxis,@pmscm,@UserID");

                        // var ptnpharmacy = new SqlParameter("@ptn_pharmacy_pk", ptn_pharmacy_pk);



                        var ptnpharmacy = new SqlParameter();
                        ptnpharmacy.SqlDbType = SqlDbType.Int;
                        ptnpharmacy.ParameterName = "@ptn_pharmacy_pk";
                        ptnpharmacy.Size = -1;
                        ptnpharmacy.Value = Convert.ToInt32(ptn_pharmacy_pk);


                        //  var drugid = new SqlParameter("@DrugId",  drg.DrugId);



                        var drugid = new SqlParameter();
                        drugid.SqlDbType = SqlDbType.Int;
                        drugid.ParameterName = "@DrugId";
                        drugid.Size = -1;
                        drugid.Value = Convert.ToInt32(drg.DrugId);
                        // var batchid = new SqlParameter("@BatchId",  drg.BatchId);
                        var batchid = new SqlParameter();
                        batchid.SqlDbType = SqlDbType.VarChar;
                        batchid.ParameterName = "@BatchId";
                        batchid.Size = -1;
                        batchid.Value = drg.BatchId;


                        // var freqid = new SqlParameter("@FreqId", drg.FreqId);

                        var freqid = new SqlParameter();
                        freqid.SqlDbType = SqlDbType.Int;
                        freqid.ParameterName = "@FreqId";
                        freqid.Size = -1;
                        freqid.Value = Convert.ToInt32(drg.FreqId);


                        //  var dose = new SqlParameter("@Dose", drg.Dose);


                        var dose = new SqlParameter();
                        dose.SqlDbType = SqlDbType.Int;
                        dose.ParameterName = "@Dose";
                        dose.Size = -1;
                        dose.Value = Convert.ToInt32(drg.Dose);


                        // var morning = new SqlParameter("@Morning",  drg.Morning == "" ? "0" : drg.Morning);

                        var morning = new SqlParameter();
                        morning.SqlDbType = SqlDbType.Int;
                        morning.ParameterName = "@Morning";
                        morning.Size = -1;
                        morning.Value = Convert.ToInt32(drg.Morning);


                        //var  midday = new SqlParameter("@Midday",  drg.Midday == "" ? "0" : drg.Midday);

                        var midday = new SqlParameter();
                        midday.SqlDbType = SqlDbType.Int;
                        midday.ParameterName = "@Midday";
                        midday.Size = -1;
                        midday.Value = Convert.ToInt32(drg.Midday);

                        // var  evening = new SqlParameter("@Evening",  drg.Evening == "" ? "0" : drg.Evening);


                        var evening = new SqlParameter();
                        evening.SqlDbType = SqlDbType.Int;
                        evening.ParameterName = "@Evening";
                        evening.Size = -1;
                        evening.Value = Convert.ToInt32(drg.Evening);


                        // var   night=new SqlParameter("@Night",  drg.Night == "" ? "0" : drg.Night);



                        var night = new SqlParameter();
                        night.SqlDbType = SqlDbType.Int;
                        night.ParameterName = "@Night";
                        night.Size = -1;
                        night.Value = Convert.ToInt32(drg.Night);


                        // var duration = new SqlParameter("@Duration", drg.Duration);
                        var duration = new SqlParameter();
                        duration.SqlDbType = SqlDbType.Int;
                        duration.ParameterName = "@Duration";
                        duration.Size = -1;
                        duration.Value = Convert.ToInt32(drg.Duration);

                        // var qtypres = new SqlParameter("@qtyPres", drg.qtyPres);


                        var qtypres = new SqlParameter();
                        qtypres.SqlDbType = SqlDbType.Int;
                        qtypres.ParameterName = "@qtyPres";
                        qtypres.Size = -1;
                        qtypres.Value = Convert.ToInt32(drg.qtyPres);


                        // var qtydisp = new SqlParameter("@qtyDisp",  drg.qtyDisp);

                        var qtydisp = new SqlParameter();
                        qtydisp.SqlDbType = SqlDbType.Int;
                        qtydisp.ParameterName = "@qtyDisp";
                        qtydisp.Size = -1;
                        qtydisp.Value = Convert.ToInt32(drg.qtyDisp);
                        // var prophylaxis = new SqlParameter("@prophylaxis", drg.prophylaxis);

                        var prophylaxis = new SqlParameter();
                        prophylaxis.SqlDbType = SqlDbType.Int;
                        prophylaxis.ParameterName = "@prophylaxis";
                        prophylaxis.Size = -1;
                        prophylaxis.Value =(drg.prophylaxis== "")? 0: Convert.ToInt32(drg.prophylaxis);
                        //var pmscmflag= new SqlParameter("@pmscm",  pmscmFlag);
                        var pmscmflag = new SqlParameter();
                        pmscmflag.SqlDbType = SqlDbType.Int;
                        pmscmflag.ParameterName = "@pmscm";
                        pmscmflag.Size = -1;
                        pmscmflag.Value = Convert.ToInt32(pmscmFlag);
                        //var userid =new SqlParameter("@UserID",UserID);

                        var userid = new SqlParameter();
                        userid.SqlDbType = SqlDbType.Int;
                        userid.ParameterName = "@UserID";
                        userid.Size = -1;
                        userid.Value = Convert.ToInt32(UserID);

                        var presc = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqlprescph.ToString(), parameters: new[] {
                    ptnpharmacy,
                    drugid,
                    batchid,
                    freqid,
                    dose,
                    morning,
                    midday,
                    evening,
                    night,
                    duration,qtypres,
                    qtydisp,
                    prophylaxis,
                    pmscmflag,
                    userid

            });



                    }
                }

                return Convert.ToInt32(ptn_pharmacy_pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
