import { VisitDetailsEditCommand } from './../_models/VisitDetailsEditCommand';
import { FamilyPlanningCommand } from './../_models/FamilyPlanningCommand';
import { HivTestsCommand } from './../_models/HivTestsCommand';
import { PatientMasterVisitEncounter } from './../_models/PatientMasterVisitEncounter';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { PncVisitDetailsCommand } from '../_models/PncVisitDetailsCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import { PatientDiagnosisCommand } from '../_models/PatientDiagnosisCommand';
import { PatientReferralCommand } from '../_models/PatientReferralCommand';
import { PatientAppointment } from '../_models/PatientAppointmet';
import { PostNatalExamCommand } from '../_models/PostNatalExamCommand';
import { FamilyPlanningMethodCommand } from '../_models/FamilyPlanningMethodCommand';
import { PartnerTestingCommand } from '../_models/PartnerTestingCommand';
import { DrugAdministrationCommand } from '../maternity/commands/drug-administration-command';
import { PatientReferralEditCommand } from '../_models/PatientReferralEditCommand';
import { PatientAppointmentEditCommand } from '../_models/PatientAppointmentEditCommand';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PncService {
    private API_URL = environment.API_URL;
    private API_LAB_URL = environment.API_LAB_URL;
    private API_PMTCT_URL = environment.API_PMTCT_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public savePncMasterVisit(patientMasterVisitEncounter: PatientMasterVisitEncounter): Observable<any> {
        return this.http.post<PatientMasterVisitEncounter>(this.API_URL + '/api/PatientMasterVisit',
            JSON.stringify(patientMasterVisitEncounter), httpOptions).pipe(
                tap(savePncMasterVisit => this.errorHandler.log(`successfully added pnc patientmastervisit`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc patientmastervisit'))
            );
    }

    public savePncVisitDetails(pncVisitDetailsCommand: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/AncVisitDetails/Post',
            JSON.stringify(pncVisitDetailsCommand), httpOptions).pipe(
                tap(savePncVisitDetails => this.errorHandler.log(`successfully saved pnc visit details`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc visit details'))
            );
    }

    public editPncVisitDetails(visitDetailsEditCommand: VisitDetailsEditCommand) {
        const Indata = {
            VisitDetails: visitDetailsEditCommand
        };

        return this.http.put(this.API_URL + '/api/AncVisitDetails/Put', JSON.stringify(Indata), httpOptions).pipe(
            tap(editPncVisitDetails => this.errorHandler.log(`successfully edited pnc visit details`)),
            catchError(this.errorHandler.handleError<any>('Error editing pnc visit details'))
        );
    }

    public savePncPostNatalExam(pncPostNatalExamCommand: PostNatalExamCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/PostNatalAndBabyExamination',
            JSON.stringify(pncPostNatalExamCommand), httpOptions).pipe(
                tap(savePncPostNatalExam => this.errorHandler.log(`successfully saved pnc postnatal exam`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc postnatal exam'))
            );
    }

    public updatePncPostNatalExam(pncPostNatalExamCommand: PostNatalExamCommand): Observable<any> {
        return this.http.post(this.API_PMTCT_URL
            + '/api/PostNatalAndBabyExamination/UpdatePatientExamination', JSON.stringify(pncPostNatalExamCommand), httpOptions).pipe(
                tap(updatePncPostNatalExam => this.errorHandler.log(`successfully edited postnatal exam`)),
                catchError(this.errorHandler.handleError<any>('Error editing postnatal exam'))
            );
    }

    public savePncHivStatus(hivStatusCommand: HivStatusCommand, anyTests: any[]): Observable<any> {
        if (anyTests.length == 0) {
            return of([]);
        }

        const Indata = {
            'Encounter': hivStatusCommand
        };

        return this.http.post<any>(this.API_URL + '/api/HtsEncounter', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePncBabyExamination => this.errorHandler.log(`successfully saved pnc hiv status`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc hiv status'))
        );
    }

    public savePncHivTests(hivTestsCommand: HivTestsCommand): Observable<any> {
        if (hivTestsCommand.Testing.length == 0) {
            return of([]);
        }
        return this.http.post<any>(this.API_URL + '/api/HtsEncounter/addTestResults', JSON.stringify(hivTestsCommand), httpOptions).pipe(
            tap(savePncHivTests => this.errorHandler.log(`successfully saved pnc hiv tests`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc hiv tests'))
        );
    }

    public savePncDrugAdministration(drugAdministrationCommand: DrugAdministrationCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/PatientDrugAdministration/Add',
            JSON.stringify(drugAdministrationCommand), httpOptions).pipe(
                tap(savePncDrugAdministration => this.errorHandler.log(`successfully saved pnc drug administration`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc drug administration'))
            );
    }

    public getPncDrugAdministration(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/PatientDrugAdministration/GetByPatientIdAndPatientMasterVisitId/'
            + patientId + '/' + patientMasterVisitId).pipe(
                tap(getPncDrugAdministration =>
                    this.errorHandler.log(`successfully fetched patient drug administration by patientid: `
                        + patientId + ` and patientMasterVisitId: ` + patientMasterVisitId)),
                catchError(this.errorHandler.handleError<any>('Error fetching patient drug administration values'))
            );
    }

    public savePncScreening(): Observable<any> {
        return this.http.post<any>(this.API_URL + '', JSON.stringify(''), httpOptions).pipe(
            tap(savePncScreening => this.errorHandler.log(`successfully saved pnc screening`)),
            catchError(this.errorHandler.handleError<any>(`Error saving pnc screening`))
        );
    }

    public savePartnerTesting(partnerTestingCommand: PartnerTestingCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/PatientPartnerTesting/AddPartnerTesting',
            JSON.stringify(partnerTestingCommand), httpOptions).pipe(
                tap(savePartnerTesting => this.errorHandler.log(`successfully saved pnc partner testing`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc partner testing'))
            );
    }

    public getPartnerTesting(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/PatientPartnerTesting/Get/' + patientId).pipe(
            tap(getPartnerTesting => this.errorHandler.log(`successfully fetched pnc partner testing`)),
            catchError(this.errorHandler.handleError<any>('Error fetching pnc partner testing'))
        );
    }

    public saveDiagnosis(pncPatientDiagnosis: PatientDiagnosisCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/PatientDiagnosis/AddDiagnosis',
            JSON.stringify(pncPatientDiagnosis), httpOptions).pipe(
                tap(saveDiagnosis => this.errorHandler.log(`successfully saved pnc diagnosis`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc diagnosis'))
            );
    }

    public updatePatientDiagnosis(patientDiagnosisEdit: any) {
        return this.http.post(this.API_PMTCT_URL
            + '/api/PatientDiagnosis/UpdateDiagnosis', JSON.stringify(patientDiagnosisEdit), httpOptions).pipe(
                tap(updatePatientDiagnosis => this.errorHandler.log(`successfully updated pnc diagnosis`)),
                catchError(this.errorHandler.handleError<any>('Error editing pnc diagnosis'))
            );
    }

    public savePncReferral(pncReferralCommand: PatientReferralCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/PatientReferralAndAppointment/AddPatientReferralInfo',
            JSON.stringify(pncReferralCommand), httpOptions).pipe(
                tap(savePncReferral => this.errorHandler.log(`successfully saved pnc referral`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc referral'))
            );
    }

    public getReferral(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get<any>(this.API_URL
            + '/api/PatientReferralAndAppointment/GetReferral/' + patientId + '/' + patientMasterVisitId).pipe(
                tap(getReferral => this.errorHandler.log(`successfully fetched referral`)),
                catchError(this.errorHandler.handleError<any>('Error fetching referral'))
            );
    }

    public updateReferral(patientReferralEditCommand: PatientReferralEditCommand): Observable<any> {
        return this.http.post(this.API_URL
            + '/api/PatientReferralAndAppointment/UpdatePatientReferralInfo', JSON.stringify(patientReferralEditCommand), httpOptions).pipe(
                tap(updateReferral => this.errorHandler.log(`successfully updated referral`)),
                catchError(this.errorHandler.handleError<any>('Error updating referral'))
            );
    }

    public savePncNextAppointment(pncNextAppointmentCommand: PatientAppointment): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/PatientReferralAndAppointment/AddPatientNextAppointment',
            JSON.stringify(pncNextAppointmentCommand), httpOptions).pipe(
                tap(savePncNextAppointment => this.errorHandler.log(`successfully saved pnc next appointment`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc next appointment'))
            );
    }

    public getAppointments(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get(this.API_URL
            + '/api/PatientReferralAndAppointment/GetAppointment/' + patientId + '/' + patientMasterVisitId).pipe(
                tap(getAppointments => this.errorHandler.log(`successfully fetched appointment`)),
                catchError(this.errorHandler.handleError<any>('Error fetching appointment'))
            );
    }

    public updateAppointment(patientAppointmentEditCommand: PatientAppointmentEditCommand): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/UpdatePatientNextAppointment',
            JSON.stringify(patientAppointmentEditCommand), httpOptions).pipe(
                tap(updateAppointment => this.errorHandler.log(`successfully updated appointment`)),
                catchError(this.errorHandler.handleError<any>('Error updating appointment'))
            );
    }

    public savePncFamilyPlanning(familyPlanningCommand: FamilyPlanningCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/FamilyPlanning', JSON.stringify(familyPlanningCommand),
            httpOptions).pipe(
                tap(savePncFamilyPlanning => this.errorHandler.log(`successfully saved pnc family planning`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc family planning'))
            );
    }

    public getFamilyPlanning(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/FamilyPlanning/' + patientId).pipe(
            tap(getFamilyPlanning => this.errorHandler.log(`successfully fetched family planning`)),
            catchError(this.errorHandler.handleError<any>('Error fetching family planning'))
        );
    }

    public savePncFamilyPlanningMethod(familyPlanningMethodCommand: FamilyPlanningMethodCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/AddFamilyPlanningMetods', JSON.stringify(familyPlanningMethodCommand),
            httpOptions).pipe(
                tap(savePncFamilyPlanningMethod => this.errorHandler.log(`successfully saved pnc family planning method`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc family planning method'))
            );
    }

    public getFamilyPlanningMethod(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/AddFamilyPlanningMetods/' + patientId).pipe(
            tap(getFamilyPlanningMethod => this.errorHandler.log(`successfully fetched family planning method`)),
            catchError(this.errorHandler.handleError<any>('Error fetching family planning method'))
        );
    }

    public getPncPostNatalExamBabyExaminationHistory(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/PostNatalAndBabyExamination/' + patientId + '/' + patientMasterVisitId).pipe(
            tap(getPncPostNatalExamBabyExaminationHistory =>
                this.errorHandler.log(`successfully fetched postnatal exam and baby examination history`)),
            catchError(this.errorHandler.handleError<any>('Error fetching postnatal exam and baby examination history'))
        );
    }

    public getHivTests(patientMasterVisitId: number, patientEncounterId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/HtsEncounter/getTestResults/' + patientMasterVisitId + '/' + patientEncounterId)
            .pipe(
                tap(getHivTests => this.errorHandler.log(`successfully fetched hiv tests`)),
                catchError(this.errorHandler.handleError<any>('Error fetching hiv tests'))
            );
    }
}
