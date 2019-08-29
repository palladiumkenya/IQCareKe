import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { environment } from '../../../environments/environment';
import { Observable, of, BehaviorSubject } from 'rxjs/index';
import { catchError, tap } from 'rxjs/operators';
import { PatientEducationCommand } from '../_models/PatientEducationCommand';
import { ClientMonitoringCommand } from '../_models/ClientMonitoringCommand';
import { HaartProphylaxisCommand } from '../_models/HaartProphylaxisCommand';
import { ReferralAppointmentCommandService } from './referral-appointment-command.service';
import { PatientPreventiveService } from '../_models/PatientPreventiveService';
import { PatientProfile } from '../_models/patientProfile';
import { PncVisitDetailsCommand } from '../_models/PncVisitDetailsCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import { VisitDetailsCommand } from '../_models/visit-details-command';
import { PregnancyAncCommand } from '../_models/pregnancy-anc-command';
import { BaselineAncProfileCommand } from '../_models/baseline-anc-profile-command';
import { HivTestsCommand } from '../_models/HivTestsCommand';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})

export class AncService {
    private API_URL = environment.API_URL;
    private API_PMTCT_URL = environment.API_PMTCT_URL;
    private _url = '/api/anc/';
    private _url_pedc = '/api/PatientEducationExamination/post';
    private _url_cm = '/api/ClientMonitoring/';
    private _url_haart = '/api/HaartProphylaxis/';
    private _url_ref = '/api/PatientReferralAndAppointment/AddPatientReferralInfo';
    private _url_app = '/api/PatientReferralAndAppointment/AddPatientNextAppointment';

    private _url_ref_edit = '/api/PatientReferralAndAppointment/UpdatePatientReferralInfo';
    private _url_app_edit = '/api/PatientReferralAndAppointment/UpdatePatientNextAppointment';

    private _url_pre = '/api/ANCPreventivervice/';
    private _url_pci = '/api/PatientChronicIllness/post';

    public profile: PatientProfile = {};

    private visitDateMessageSource = new BehaviorSubject<any>({});
    visitDate = this.visitDateMessageSource.asObservable();


    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public saveANCVisitDetails(ancVisitDetailsCommand: PncVisitDetailsCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/VisitDetails/AddANCVisit', JSON.stringify(ancVisitDetailsCommand),
            httpOptions).pipe(
                tap(saveANCVisitDetails => this.errorHandler.log(`successfully saved ANC visit details`)),
                catchError(this.errorHandler.handleError<any>('Error saving ANC visit details'))
            );
    }


    public updateVisitDate(visitDate: any) {
        this.visitDateMessageSource.next(visitDate);
    }

    public EditVisitDetails(VisitDetailsEditCommand: any): Observable<any> {
        return this.http.put<any>(this.API_URL + '/api/VisitDetails/EditANCVisit', JSON.stringify(VisitDetailsEditCommand),
            httpOptions).pipe(
                tap(saveANCVisitDetails => this.errorHandler.log(`successfully Edited ANC visit details`)),
                catchError(this.errorHandler.handleError<any>('Error Editing ANC visit details'))
            );
    }

    public savePregnancy(pregnancyCommand: PregnancyAncCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/Pregnancy/post', JSON.stringify(pregnancyCommand),
            httpOptions).pipe(
                tap(savePregnancy => this.errorHandler.log(`successfully saved Pregnancy details`)),
                catchError(this.errorHandler.handleError<any>('Error saving Pregnancy details'))
            );
    }

    public saveVisitDetails(visitDetailsCommand: VisitDetailsCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/AncVisitDetails/post', JSON.stringify(visitDetailsCommand),
            httpOptions).pipe(
                tap(saveVisitDetails => this.errorHandler.log(`successfully saved ANC visit details`)),
                catchError(this.errorHandler.handleError<any>('Error saving ANC visit details'))
            );
    }

    public EditANCVisitDetails(visitDetailsCommand: any): Observable<any> {
        return this.http.put<any>(this.API_URL + '/api/AncVisitDetails/put', JSON.stringify(visitDetailsCommand),
            httpOptions).pipe(
                tap(saveVisitDetails => this.errorHandler.log(`successfully Edited ANC visit details`)),
                catchError(this.errorHandler.handleError<any>('Error Editing ANC visit details'))
            );
    }

    public SaveBaselineProfile(baselineAncCommand: BaselineAncProfileCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/BaselineAnc/post', JSON.stringify(baselineAncCommand),
            httpOptions).pipe(
                tap(SaveAncProfile => this.errorHandler.log(`successfully saved ANC Baseline`)),
                catchError(this.errorHandler.handleError<any>('Error saving ANC Baseline'))
            );
    }

    public EditBaselineProfile(baselineAncCommand: any): Observable<any> {
        return this.http.put<any>(this.API_URL + '/api/BaselineAnc/put', JSON.stringify(baselineAncCommand),
            httpOptions).pipe(
                tap(BaselineProfile => this.errorHandler.log(`successfully Edited ANC Baseline`)),
                catchError(this.errorHandler.handleError<any>('Error Editing ANC Baseline'))
            );
    }


    public savePatientEducation(patientEducationCommand: PatientEducationCommand): Observable<PatientEducationCommand> {

        return this.http.post<any>(this.API_URL + '' + this._url_pedc, JSON.stringify(patientEducationCommand),
            httpOptions).pipe(
                tap(savePatientEducation => this.errorHandler.log('Successfully saved patient education')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient Education'))
            );
    }

    public deletePatientCounselling(id: number): Observable<any> {

        const Indata = {
            'Id': id
        };
        return this.http.post<any>(this.API_URL + '/api/PatientEducationExamination/DeletePatientCounselling', JSON.stringify(Indata),
            httpOptions).pipe(
                tap(deletePatientCounselling => this.errorHandler.log('Successfully deleted patient education')),
                catchError(this.errorHandler.handleError<any>('Error in delete Patient Education'))
            );

    }


    public saveClientMonitoring(clientMonitoringCommand: ClientMonitoringCommand): Observable<ClientMonitoringCommand> {
        return this.http.post<any>(this.API_URL + '' + this._url_cm, JSON.stringify(clientMonitoringCommand), httpOptions).pipe(
            tap(saveClientMonitoring => this.errorHandler.log('Successfully saved client monitoring')),
            catchError(this.errorHandler.handleError<any>('Error in saving ' + this.API_URL + '' + this._url_pedc))
        );
    }

    public EditClientMonitoring(clientMonitoringCommand: any): Observable<any> {
        return this.http.put<any>(this.API_URL + '' + this._url_cm, JSON.stringify(clientMonitoringCommand), httpOptions).pipe(
            tap(saveClientMonitoring => this.errorHandler.log('Successfully edited client monitoring Command')),
            catchError(this.errorHandler.handleError<any>('Error in Editing client monitoring' + this.API_URL + '' + this._url_pedc))
        );
    }


    public saveHaartProphylaxis(haartProphylaxisCommand: HaartProphylaxisCommand): Observable<HaartProphylaxisCommand> {
        return this.http.post<any>(this.API_URL + '' + this._url_haart, JSON.stringify(haartProphylaxisCommand), httpOptions).pipe(
            tap(saveHaartProphylaxis => this.errorHandler.log('Successfully saved Haart Prophylaxis')),
            catchError(this.errorHandler.handleError<any>('Error in saving Haart Prophylaxis'))
        );
    }

    public deletePatientChronicIllness(id: number): Observable<any> {
        const Indata = {
            'Id': id
        };

        return this.http.post<any>(this.API_URL + '/api/PatientChronicIllness/Delete', JSON.stringify(Indata), httpOptions).pipe(
            tap(deletePatientChronicIllness => this.errorHandler.log('Successfully deleted chronic illness')),
            catchError(this.errorHandler.handleError<any>('Error in deleting Patient Chronic Illness'))
        );

    }

    public savePatientChronicIllness(chronicIllnessCommand: any[]): Observable<any> {
        if (chronicIllnessCommand.length == 0) {
            return of([]);
        }

        const Indata = {
            'PatientChronicIllnesses': chronicIllnessCommand
        };

        return this.http.post<any>(this.API_URL + '' + this._url_pci, JSON.stringify(Indata), httpOptions).pipe(
            tap(savePatientChronicIllness => this.errorHandler.log('Successfully saved chronic illness')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Chronic Illness'))
        );
    }

    public saveDrugAdministration(drug: any): Observable<any> {
        if (drug.length == 0) {
            return of([]);
        }
        return this.http.post<any>(this.API_PMTCT_URL + '/api/PatientDrugAdministration/Add',
            JSON.stringify(drug), httpOptions).pipe(
                tap(saveDrugAdministration => this.errorHandler.log('Successfully saved Drug Administration')),
                catchError(this.errorHandler.handleError<any>('Error in saving Drug Administration'))
            );
    }


    public saveReferral(referralCommand: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {

        return this.http.post<any>(this.API_URL + '' + this._url_ref, JSON.stringify(referralCommand), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Successfully saved Referral')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Referral'))
        );
    }

    public EditReferral(referralCommand: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {

        return this.http.post<any>(this.API_URL + this._url_ref_edit, JSON.stringify(referralCommand), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Successfully Edit Patient Referral')),
            catchError(this.errorHandler.handleError<any>('Error in Editing Patient Referral'))
        );
    }

    public saveAppointment(appointmentCommand: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {
        if (appointmentCommand['AppointmentReason'] == 'None') {
            return of([]);
        }
        return this.http.post<any>(this.API_URL + '' + this._url_app, JSON.stringify(appointmentCommand), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Successfully saved Patient Appointment')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Appointment'))
        );
    }

    public EditAppointment(appointmentCommand: ReferralAppointmentCommandService): Observable<ReferralAppointmentCommandService> {
        if (appointmentCommand['AppointmentReason'] == 'None') {
            return of([]);
        }
        return this.http.put<any>(this.API_URL + '' + this._url_app_edit, JSON.stringify(appointmentCommand), httpOptions).pipe(
            tap(saveReferralAppointment => this.errorHandler.log('Successfully Edited Patient Appointment')),
            catchError(this.errorHandler.handleError<any>('Error in Editing Patient Appointment'))
        );
    }

    public savePreventiveServices(patientPreventiveService: PatientPreventiveService): Observable<any> {
        if (patientPreventiveService.preventiveService.length == 0) {
            return of([]);
        }
        return this.http.post<any>(this.API_URL + '' + this._url_pre, JSON.stringify(patientPreventiveService), httpOptions).pipe(
            tap(savePreventiveServices => this.errorHandler.log('Successfully saved Preventive Services')),
            catchError(this.errorHandler.handleError<any>('Error in saving Preventive Services'))
        );
    }



    public deletePreventiveServices(id: number): Observable<any> {
        const Indata = {
            'Id': id
        };

        return this.http.post<any>(this.API_URL + '/api/PatientService/DeletePatientPreventiveService'
            , JSON.stringify(Indata), httpOptions).pipe(
                tap(deletePreventiveServices => this.errorHandler.log('Successfully delete Preventive Services')),
                catchError(this.errorHandler.handleError<any>('Error in deleting Preventive Services'))
            );
    }

    public saveAncHivStatus(hivStatusCommand: HivStatusCommand, anyTests: any[]): Observable<any> {
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

    public saveHivResults(hivTestsCommand: HivTestsCommand): Observable<any> {
        if (!hivTestsCommand.HtsEncounterId || hivTestsCommand.HtsEncounterId == null || hivTestsCommand.HtsEncounterId == 0) {
            return of([]);
        }

        return this.http.post<any>(this.API_URL + '/api/HtsEncounter/addTestResults', JSON.stringify(hivTestsCommand), httpOptions).pipe(
            tap(saveHivResults => this.errorHandler.log('SaveHivResults command')),
            catchError(this.errorHandler.handleError<any>('PreventiveServiceController'))
        );
    }

    public getPatientCounselingInfo(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientEducationExamination/GetPatientCounseling/' +
            patientId + ' / ' + patientMasterVisitId).pipe(
                tap(getPatientCounselingInfo => this.errorHandler.log('get ANC Counseling Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientCounselingInfo'))
            );
    }

    public getPatientCounselingInfoAll(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientEducationExamination/GetPatientCounsellingAll/' +
            patientId).pipe(
                tap(getPatientCounselingInfoAll => this.errorHandler.log('get ANC Counseling Data All')),
                catchError(this.errorHandler.handleError<any[]>('getPatientCounselingInfoAll'))
            );
    }



    public getPatientPhysicalExaminationInfo(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PhysicalExamination/GetPhysicalExam/' +
            patientId + ' / ' + patientMasterVisitId).pipe(
                tap(getPatientPhysicalExminationInfo => this.errorHandler.log('get ANC Physical examination Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientPhysicalExaminationInfo'))
            );
    }

    public getPatientWhoStageInfoCurrent(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientWhoStage/GetWhoStageCurrent/' +
            patientId).pipe(
                tap(getPatientWhoStageInfo => this.errorHandler.log('get WHO Stage info Data All')),
                catchError(this.errorHandler.handleError<any[]>('getPatientWhoStageInfoAll'))
            );
    }

    public getPatientWhoStageInfo(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientWhoStage/GetWhoStage/' +
            patientId + ' / ' + patientMasterVisitId).pipe(
                tap(getPatientWhoStageInfo => this.errorHandler.log('get WHO Stage info Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientWhoStageInfo'))
            );
    }

    public getPatientScreeningInfoByPatientId(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientScreening/GetPatientScreeningByPatientId/' +
            patientId).pipe(
                tap(getPatientScreeningInfo => this.errorHandler.log('get WHO Stage info Data All')),
                catchError(this.errorHandler.handleError<any[]>('getPatientScreeningInfo All'))
            );
    }

    public getPatientScreeningInfo(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientScreening/GetPatientScreening/' +
            patientId + ' / ' + patientMasterVisitId).pipe(
                tap(getPatientScreeningInfo => this.errorHandler.log('get WHO Stage info Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientScreeningInfo'))
            );
    }

    public getPatientDrugAdministrationInfo(patientId: number) {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/PatientDrugAdministration/GetByPatientId/' +
            patientId).pipe(
                tap(getPatientDrugAdministrationInfo => this.errorHandler.log('get Drug Administration info Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientDrugAdministrationInfo'))
            );
    }

    public getPatientChronicIllnessInfo(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientChronicIllness/GetByPatientId/' +
            patientId).pipe(
                tap(getPatientChronicIllnessInfo => this.errorHandler.log('get chronic info Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientChronicIllnessInfo'))
            );
    }

    public getPatientPreventiveServiceInfo(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientService/GetByPatientId/' +
            patientId).pipe(
                tap(getPatientPreventiveServiceInfo => this.errorHandler.log('get preventive service')),
                catchError(this.errorHandler.handleError<any[]>('getPatientPreventiveServiceInfo'))
            );
    }

    public getBaselineAncProfile(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/BaselineAnc/Get/' +
            patientId).pipe(
                tap(getBaselineAncProfile => this.errorHandler.log('get ANC Profile info Data')),
                catchError(this.errorHandler.handleError<any[]>('getBaselineAncProfile'))
            );
    }

    public getPatientPartnerTestingInfo(patientId: number) {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/PatientPartnerTesting/Get/' +
            patientId).pipe(
                tap(getPatientPartnerTestingInfo => this.errorHandler.log('get Partner Testing Data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientPartnerTestingInfo'))
            );
    }

    public getPatientAppointment(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientReferralAndAppointment/GetAppointment/' +
            patientId + '/' + patientMasterVisitId).pipe(
                tap(getPatientAppointment => this.errorHandler.log('get patientappointment data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientAppointment'))
            );
    }

    public getPatientAppointmentAnc(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientReferralAndAppointment/GetAppointmentAnc/' +
            patientId + '/' + patientMasterVisitId).pipe(
                tap(getPatientAppointment => this.errorHandler.log('get patientappointment data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientAppointment'))
            );
    }



    public getPatientReferral(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/PatientReferralAndAppointment/GetReferral/' +
            patientId + '/' + patientMasterVisitId).pipe(
                tap(getPatientReferral => this.errorHandler.log('get patient Referral data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientReferral'))
            );
    }

    public getPatientVisitDetails(patientId: number, patientMasterVisitId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/AncVisitDetails/Get/' +
            patientId + '/' + patientMasterVisitId).pipe(
                tap(getPatientVisitDetails => this.errorHandler.log('get patient visit details data')),
                catchError(this.errorHandler.handleError<any[]>('getPatientVisitDetails'))
            );
    }

}

