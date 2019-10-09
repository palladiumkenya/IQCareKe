import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { PrepStatusCommand } from '../_models/commands/PrepStatusCommand';
import { AllergiesCommand } from '../_models/commands/AllergiesCommand';
import { ClientCircumcisionStatusCommand } from '../_models/commands/ClientCircumcisionStatusCommand';
import { PregnancyIndicatorCommand } from '../_models/commands/PregnancyIndicatorCommand';
import { PregnancyIndicatorLogCommand } from '../_models/commands/PregnancyIndicatorLogCommand';
import { EditAppointmentCommand } from '../_models/commands/nextAppointmentCommand';
import { EncounterDetails } from '../../dashboard/_model/HtsEncounterdetails';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PrepService {
    private API_URL = environment.API_URL;
    private PREP_API_URL = environment.API_PREP_URL;
    private MATERNITY_API_URL = environment.API_PMTCT_URL;
    private LAB_URL = environment.API_LAB_URL;
    private _htsurl = '/api/HtsEncounter';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    public StiScreeningTreatment(STIScreeningCommand: any): Observable<any> {
        if (STIScreeningCommand.Screenings.length == 0) {
            return of([]);
        }

        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PatientScreening/PostPatientScreenings',
            JSON.stringify(STIScreeningCommand), httpOptions).pipe(
                tap(StiScreeningTreatment => this.errorHandler.log(`successfully added sti screening details`)),
                catchError(this.errorHandler.handleError<any>('Error adding sti screening details'))
            );
    }
    public getHTSEncounterDetailsBypersonId(personId: number): Observable<any[]> {
        return this.http.get<EncounterDetails[]>(this.API_URL + this._htsurl + '/getEncounterDetailsByPersonId/' + personId).pipe(
            tap(getHTSEncounterDetailsBypersonId => this.errorHandler.log('fetched a single client encounter details')),
            catchError(this.errorHandler.handleError<any[]>('getHTSEncounterDetailsBypersonId', []))
        );
    }
    public GetCurrentPatientVitalsInfo(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetCurrentPersonVitals/' + personId).pipe(
            tap(GetCurrentPatientVitalsInfo => this.errorHandler.log('get patient vitals details')),
            catchError(this.errorHandler.handleError<any>('GetCurrentPatientVitalsInfo'))
        );
    }
    CheckPrepencounterExists(personId: number): Observable<any[]> {
        const Indata = {
            'PersonId': personId
        };
        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/Encounterexists', JSON.stringify(Indata), httpOptions)
            .pipe(tap(CheckencounterExists => this.errorHandler.log('checked if RiskAssessmentEncounter Exists')),
                catchError(this.errorHandler.handleError<any[]>('CheckencounterExists'))
            );
    }

    public UpdateStiScreeningTreatment(STIScreeningCommand: any): Observable<any> {
        if (STIScreeningCommand.Screenings.length == 0) {
            return of([]);
        }

        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PatientScreening/UpdatePatientScreenings',
            JSON.stringify(STIScreeningCommand), httpOptions).pipe(
                tap(StiScreeningTreatment => this.errorHandler.log(`successfully updated sti screening details`)),
                catchError(this.errorHandler.handleError<any>('Error updating sti screening details'))
            );
    }

    public getStiScreeningTreatment(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.MATERNITY_API_URL + '/api/PatientScreening/' + patientId + '/' + patientMasterVisitId).pipe(
            tap(getStiScreeningTreatment => this.errorHandler.log(`successfully fetched sti screening details`)),
            catchError(this.errorHandler.handleError<any>('Error fetching sti screening details'))
        );
    }

    public savePrepStatus(prepStatusCommand: PrepStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/PrepStatus/AddPrepStatus',
            JSON.stringify(prepStatusCommand), httpOptions).pipe(
                tap(savePrepStatus => this.errorHandler.log(`successfully added prep status details`)),
                catchError(this.errorHandler.handleError<any>('Error saving prep status'))
            );
    }

    public getPrepStatus(patientId: number, patientEncounterId: number): Observable<any[]> {
        return this.http.get<any[]>(this.PREP_API_URL + '/api/PrepStatus/GetPrepStatus/' + patientId + '/' + patientEncounterId).pipe(
            tap(getPrepStatus => this.errorHandler.log(`successfully fetched prep status details`)),
            catchError(this.errorHandler.handleError<any>('Error fetching prep status'))
        );
    }

    public savePatientAdverseEvents(adverseEventsCommand: any[]): Observable<any> {
        if (adverseEventsCommand.length == 0) {
            return of([]);
        }

        const Indata = {
            'AdverseEvents': adverseEventsCommand
        };

        return this.http.post<any>(this.API_URL + '/api/AdverseEvents/AddAdverseEvents', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePatientAdverseEvents => this.errorHandler.log('Successfully saved adverse events')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Adverse Events'))
        );
    }

    public DeleteAdverseEvents(id: any): Observable<any> {
        const Indata = {
            'Id': id
        };
        return this.http.post<any>(this.API_URL + '/api/AdverseEvents/DeleteAdverseEvents', JSON.stringify(Indata), httpOptions).pipe(
            tap(deleteAdverseEvents => this.errorHandler.log('Successfully deleted adverse events')),
            catchError(this.errorHandler.handleError<any>('Error in deleting Patient Adverse Events'))
        );


    }

    public getPatientAdverseEvents(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/AdverseEvents/GetPatientAdverseEvents/' + patientId).pipe(
            tap(getPatientAdverseEvents => this.errorHandler.log('Successfully fetched adverse events')),
            catchError(this.errorHandler.handleError<any>('Error in fetching Patient Adverse Events'))
        );
    }

    public savePatientAllergies(allergiesCommand: AllergiesCommand[]): Observable<any> {
        if (allergiesCommand.length == 0) {
            return of([]);
        }

        const Indata = {
            'PatientAllergies': allergiesCommand
        };

        return this.http.post<any>(this.API_URL + '/api/PatientAllergy/AddAllergy', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePatientAllergies => this.errorHandler.log('Successfully saved patient allergies')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Allergies'))
        );
    }
    public DeleteAllergy(id: any): Observable<any> {
        const Indata = {
            'Id': id
        };
        return this.http.post<any>(this.API_URL + '/api/PatientAllergy/DeleteAllergy', JSON.stringify(Indata), httpOptions).pipe(
            tap(deleteAllergy => this.errorHandler.log('Successfully deleted allergies')),
            catchError(this.errorHandler.handleError<any>('Error in deleting Patient Allergies'))
        );


    }



    public getPatientAllergies(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientAllergy/GetPatientAllergy/' + patientId).pipe(
            tap(getPatientAllergies => this.errorHandler.log('Successfully fetched patient allergies')),
            catchError(this.errorHandler.handleError<any>('Error in fetching Patient Allergies'))
        );
    }

    public saveCircumcisionStatus(clientCircumcisionStatusCommand: ClientCircumcisionStatusCommand): Observable<any> {
        return this.http.post<any>(this.PREP_API_URL + '/api/CircumcisionStatus/AddCircumcisionStatus',
            JSON.stringify(clientCircumcisionStatusCommand), httpOptions).pipe(
                tap(saveCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
            );
    }

    public getCircumcisionStatus(patientId: number): Observable<any> {
        return this.http.get<any>(this.PREP_API_URL + '/api/CircumcisionStatus/GetCircumcisionStatus/' + patientId).pipe(
            tap(getCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
        );
    }

    public savePregnancyIndicatorCommand(pregnancyIndicatorCommand: PregnancyIndicatorCommand): Observable<any> {
        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PregnancyIndicator/AddPregnancyIndicator',
            JSON.stringify(pregnancyIndicatorCommand), httpOptions).pipe(
                tap(savePregnancyIndicatorCommand => this.errorHandler.log('Successfully saved patient pregnancy indicator status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient pregnancy indicator status'))
            );
    }

    public getPregnancyIndicator(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get<any>(this.MATERNITY_API_URL +
            '/api/PregnancyIndicator/GetPregnancyIndicator/' + patientId + '/' + patientMasterVisitId).pipe(
                tap(getPregnancyIndicator => this.errorHandler.log('Successfully fetched patient pregnancy indicator status')),
                catchError(this.errorHandler.handleError<any>('Error in fetching Patient pregnancy indicator status'))
            );
    }

    public savePregnancyIndicatorLogCommand(pregnancyIndicatorLog: PregnancyIndicatorLogCommand): Observable<any> {
        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PregnancyIndicator/AddPregnancyOutcome',
            JSON.stringify(pregnancyIndicatorLog), httpOptions).pipe(
                tap(savePregnancyIndicatorLogCommand => this.errorHandler.log('Successfully saved patient pregnancy indicator log status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient pregnancy indicator log status'))
            );
    }

    public getPregnancyIndicatorLog(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.MATERNITY_API_URL + '/api/PregnancyIndicator/GetPregnancyOutcome/' +
            patientId + '/' + patientMasterVisitId).pipe(
                tap(getPregnancyIndicatorLog => this.errorHandler.log('Successfully fetched patient pregnancy indicator log status')),
                catchError(this.errorHandler.handleError<any>('Error in fetching Patient pregnancy indicator log status'))
            );
    }

    AddEditBehaviourRisk(EncounterTypeId: number,
        createdby: number, patientid: number, patientmastervisitid: number, visitdate: string, serviceareaId:
            number, riskassessment: any[], clinicalnotes: any[]): Observable<any[]> {
        const Indata = {
            'EncounterTypeId': EncounterTypeId,
            'UserId': createdby,
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid,
            'VisitDate': visitdate,
            'ServiceAreaId': serviceareaId,
            'riskAssessments': riskassessment,
            'ClinicalNotes': clinicalnotes
        };

        console.log(Indata);

        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/AddAssessmentVisitDetail', JSON.stringify(Indata)
            , httpOptions).pipe(
                tap((submitRiskAssessments: any) => this.errorHandler.log(`Submit RiskAssessment Results`)),
                catchError(this.errorHandler.handleError<any>('submitRiskAssessmentResults'))
            );

    }

    GetRiskAssessmentDetails(personId: number): Observable<any[]> {
        return this.http.get<any>(this.PREP_API_URL + '/api/BehaviourRisk/GetRiskAssessmentVisitsDetails/' + personId)
            .pipe(tap(GetRiskAssessmentDetails => this.errorHandler.log('Get Risk details')),
                catchError(this.errorHandler.handleError<any[]>('RiskAssessmentDetails'))
            );
    }
    CheckencounterExists(patientid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid
        };
        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/Encounterexists', JSON.stringify(Indata), httpOptions)
            .pipe(tap(CheckencounterExists => this.errorHandler.log('checked if RiskAssessmentEncounter Exists')),
                catchError(this.errorHandler.handleError<any[]>('CheckencounterExists'))
            );
    }

    GetAssessmentDetails(patientid: number, patientmastervisitid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid
        };

        return this.http.post<any>(this.PREP_API_URL + '/api/BehaviourRisk/GetAssessmentFormDetails', JSON.stringify(Indata), httpOptions)
            .pipe(tap(GetAssessmentDetails => this.errorHandler.log('GetAssessment Form Details ')),
                catchError(this.errorHandler.handleError<any[]>('GetAssessmentDetails'))
            );

    }
    public getPatientStartEncounterEventDate(patientId: number, startitemId: number): Observable<any[]> {

        return this.http.get<any[]>(this.PREP_API_URL + '/api/PrepStatus/GetPrepStartEventStatus/' +
            patientId + '/' + startitemId).pipe(
                tap(getPatientStartEncounterEventDate => this.errorHandler.log('Successfully fetched patient start date status')),
                catchError(this.errorHandler.handleError<any>('Error in fetching P patient start date event status'))
            );
    }

    public getPatientAdherenceOutcome(patientId: number) {
        return this.http.get<any[]>(this.PREP_API_URL + '/api/PrepStatus/GetPatientAdherenceStatus/' +
            patientId).pipe(
                tap(getPatientStartEncounterEventDate => this.errorHandler.log('Successfully fetched patient adherence status')),
                catchError(this.errorHandler.handleError<any>('Error in fetching Patient adherence status'))
            );
    }


    public getPatientMasterVisits(patientId: number, patientmastervisitid: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientServices/GetMasterVisits/' + patientId + '/' + patientmastervisitid).pipe(
            tap(getPatientEncounters => this.errorHandler.log('get ')),
            catchError(this.errorHandler.handleError<any[]>('getPatientEncounters', []))
        );
    }

    public getPrepEncounterHistory(patientId: number, serviceAreaId: number,
        fromDate: Date = null, toDate: Date = null): Observable<any[]> {
        const Indata = {
            'PatientId': patientId,
            'ServiceAreaId': serviceAreaId,
            'fromDate': fromDate,
            'toDate': toDate
        };

        return this.http.post<any[]>(this.PREP_API_URL + '/api/PrepEncounter/GetPrepEncounters', JSON.stringify(Indata)
            , httpOptions).pipe(
                tap(getPrepEncounterHistory => this.errorHandler.log(`successfully fetched prep encounters`)),
                catchError(this.errorHandler.handleError<any>('Error fetching prep encounters'))
            );
    }

    public getPatientCareEndDetails(patientmasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientServices/GetPatientCareEndedDetails/' + patientmasterVisitId).pipe(
            tap(getPatientCareEndDetails => this.errorHandler.log(`successfully fetch careended details`)),
            catchError(this.errorHandler.handleError<any>('Error fetching careend details'))
        );
    }
    public getMonthlyRefillDetails(patientmasterVisitId: number, patientId: number, serviceAreaId: number): Observable<any[]> {
        return this.http.get<any[]>(this.PREP_API_URL + '/api/MonthlyRefill/GetMonthlyRefillDetails/'
            + patientId + '/' + patientmasterVisitId + '/' + serviceAreaId).
            pipe(tap(getMonthlyRefillDetails => this.errorHandler.log(`successfully fetch details`)),
                catchError(this.errorHandler.handleError<any>('Error fetching details'))
            );
    }




    public AddMonthlyRefill(patientId: number, PatientMasterVisitId: number, CreatedBy: number, serviceAreaId: number, VisitDate: Date,
        adherence: any[], screeningdetail: any[], clinicalnotes: any[]) {
        const Indata = {
            'PatientId': patientId,
            'PatientMasterVisitId': PatientMasterVisitId,
            'CreatedBy': CreatedBy,
            'ServiceAreaId': serviceAreaId,
            'VisitDate': VisitDate,
            'Adherence': adherence,
            'screeningdetail': screeningdetail,
            'clinicalNotes': clinicalnotes
        };

        return this.http.post<any>(this.PREP_API_URL + '/api/MonthlyRefill/AddMonthlyRefill', JSON.stringify(Indata), httpOptions).
            pipe(tap(AddMonthlyRefill => this.errorHandler.log(`Added the monthlyrefilldetails`)),
                catchError(this.errorHandler.handleError<any[]>('MonthlyRefillDetails')));

    }
    public careEndPatientdetails(patientId: number, serviceAreaId: number,
        patientmasterVisitId: number, careEndDate: string, specify: string
        , disclosurereason: number, deathdate: string, userId: number) {

        const Indata = {
            'PatientId': patientId,
            'ServiceAreaId': serviceAreaId,
            'PatientMasterVisitId': patientmasterVisitId,
            'CareEndedDate': careEndDate,
            'specify': specify,
            'DisclosureReason': disclosurereason,
            'DeathDate': deathdate,
            'UserId': userId
        };
        return this.http.post<any>(this.API_URL + '/api/PatientServices/CareEndPatient', JSON.stringify(Indata), httpOptions)
            .pipe(tap(careEndPatientdetails => this.errorHandler.log(`CareEnding the patient`)),
                catchError(this.errorHandler.handleError<any[]>('careEndPatientdetails'))
            );
    }

    public getLabTestResults(patientId: number, status: string): Observable<any> {
        const url = status == null ? this.LAB_URL + '/api/LabOrder/GetLabTestResults?patientId=' + patientId :
            this.LAB_URL + '/api/LabOrder/GetLabTestResults?patientId=' + patientId + '&status=' + status;

        return this.http.get<any>(url).pipe(
            tap(getLabTestResults => this.errorHandler.log('get lab order test results')),
            catchError(this.errorHandler.handleError<any[]>('getLabOrderTestResults'))
        );
    }



    public saveNextAppointment(appointment: any): Observable<any> {
        if (!appointment.AppointmentDate || appointment.AppointmentDate == null
            || appointment.AppointmentDate == 'null') {
            return of([]);
        }

        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/AddPatientNextAppointment', JSON.stringify(appointment),
            httpOptions).pipe(
                tap(saveReferrals => this.errorHandler.log(`successfully added Referral details`)),
                catchError(this.errorHandler.handleError<any>('Error saving Referral details'))
            );
    }


    public getAppointments(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get(this.API_URL
            + '/api/PatientReferralAndAppointment/GetAppointment/' + patientId + '/' + patientMasterVisitId).pipe(
                tap(getAppointments => this.errorHandler.log(`successfully fetched appointment`)),
                catchError(this.errorHandler.handleError<any>('Error fetching appointment'))
            );
    }

    public updateAppointment(patientAppointmentEditCommand: EditAppointmentCommand): Observable<any> {
        // console.log(patientAppointmentEditCommand);

        if ((!patientAppointmentEditCommand.AppointmentDate || !patientAppointmentEditCommand.AppointmentId)
            || (patientAppointmentEditCommand.AppointmentDate == null || patientAppointmentEditCommand.AppointmentId == null)) {
            return of([]);
        }

        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/UpdatePatientNextAppointment',
            JSON.stringify(patientAppointmentEditCommand), httpOptions).pipe(
                tap(updateAppointment => this.errorHandler.log(`successfully updated appointment`)),
                catchError(this.errorHandler.handleError<any>('Error updating appointment'))
            );
    }

    public getPatientAppointmentsServiceArea(patientId: number, serviceAreaId: number): Observable<any> {
        return this.http.get(this.API_URL
            + '/api/PatientServices/GetPatientAppointmentServiceArea/' + patientId + '/' + serviceAreaId).pipe(
                tap(getPatientAppointmentsServiceArea => this.errorHandler.log(`successfully fetched appointment`)),
                catchError(this.errorHandler.handleError<any>('Error fetching appointment'))
            );
    }

    public getAllHTSEncounterBypersonId(personId: number): Observable<any[]> {
        return this.http.get<EncounterDetails[]>(this.API_URL + this._htsurl + '/getLatestEncounterDetails/' + personId).pipe(
            tap(getHTSEncounterDetailsBypersonId => this.errorHandler.log('fetched a single client encounter details')),
            catchError(this.errorHandler.handleError<any[]>('getHTSEncounterDetailsBypersonId', []))
        );
    }

}
