import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import {catchError, tap} from 'rxjs/operators';
import {Observable} from 'rxjs/index';
import {PatientMasterVisitEncounter} from '../_models/PatientMasterVisitEncounter';

const httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
};


@Injectable({
    providedIn: 'root'
})
export class MaternityService {
    private API_URL = environment.API_URL;
    private API_MATERNITY_URL = environment.API_MATERNITY_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {
    }

    public saveMaternityMasterVisit(patientMasterVisitEncounter: PatientMasterVisitEncounter): Observable<any> {
        return this.http.post<PatientMasterVisitEncounter>(this.API_URL + '/api/PatientMasterVisit',
            JSON.stringify(patientMasterVisitEncounter), httpOptions).pipe(
            tap(saveMaternityMasterVisit => this.errorHandler.log(`successfully added maternity patientmastervisit`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity patientmastervisit'))
        );
    }

    public getCurrentVisitDetails(patientId: number) {
        return this.http.get<any>(this.API_URL + '/api/VisitDetails/GetCurrentVisit/' + patientId).pipe(
            tap(getCurrentVisitDetails => this.errorHandler.log('get current visit data')),
            catchError(this.errorHandler.handleError<any[]>('getCurrentVisitDetails'))
        );
    }

    public  getPregnancyDetails(patientId: number) {
        return this.http.get<any>(this.API_URL + '/api/VisitDetails/GetPregnancyProfile/' + patientId).pipe(
            tap(getPregnancyDetails => this.errorHandler.log('get current pregnancy details')),
            catchError(this.errorHandler.handleError<any[]>('getPregnancyDetails'))
        );
    }

    public getMaternityEncounter(patientId: number) {
        return this.http.get<any>(this.API_MATERNITY_URL + '/api/MaternityEncounter/' + patientId).pipe(
            tap(getMaternityEncounter => this.errorHandler.log('get current maternity details')),
            catchError(this.errorHandler.handleError<any[]>('GetMaternityEncounter'))
        );
    }

    public saveVisitDetails(visitDetails: any): Observable<any>  {
        return this.http.post(this.API_URL + '/api/VisitDetails/AddPNCVisitDetails', JSON.stringify(visitDetails), httpOptions).pipe(
            tap(saveVisitDetals => this.errorHandler.log(`successfully added maternity visits`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity visit'))
        );
    }

    public savePregnancyProfile(motherProfile: any): Observable<any>  {
        return this.http.post(this.API_URL + '/api/VisitDetails/postPregnancy', JSON.stringify(motherProfile), httpOptions).pipe(
            tap(savePregnancyProfile => this.errorHandler.log(`successfully added pregnancy profile`)),
            catchError(this.errorHandler.handleError<any>('Error pregnancy profile'))
        );
    }

    public saveDiagnosis(diagnosis: any): Observable<any> {
        return this.http.post(this.API_MATERNITY_URL + '/api/PatientDiagnosis/AddDiagnosis', JSON.stringify(diagnosis), httpOptions).pipe(
            tap(saveVisitDetals => this.errorHandler.log(`successfully added maternity diagnosis`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity diagnosis'))
        );
    }

    public savePatientDelivery(delivery: any): Observable<any> {
        return this.http.post(this.API_MATERNITY_URL + '/api/MaternityPatientDeliveryInfo/AddPatientDeliveryInfo', JSON.stringify(delivery),
            httpOptions).pipe(
            tap(savePatientDelivery => this.errorHandler.log(`successfully added maternity delivery info`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity Delivery info'))
        );
    }

    public saveBabySection(babysection: any): Observable<any> {
        return this.http.post(this.API_MATERNITY_URL + '/api/MaternityPatientDeliveryInfo/AddDeliveredBabyBirthInfoCollection',
            JSON.stringify(babysection),
            httpOptions).pipe(
            tap(saveBabySection => this.errorHandler.log(`successfully added maternity baby section`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity baby section'))
        );
    }

    public saveMaternalDrugAdministration(drug: any): Observable<any> {
        return this.http.post(this.API_MATERNITY_URL + '/api/PatientDiagnosis/AddDrugAdministrationInfo',
            JSON.stringify(drug), httpOptions).pipe(
            tap(saveMaternalDrugAdministration => this.errorHandler.log(`successfully added maternal drug administration`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternal drug administration'))
        );
    }


    public savePartnerTesting(partner: any): Observable<any> {
        return this.http.post(this.API_MATERNITY_URL + '/api/MaternityPatientDeliveryInfo', JSON.stringify(partner), httpOptions).pipe(
            tap(savePartnerTesting => this.errorHandler.log(`successfully added Partner testing details`)),
            catchError(this.errorHandler.handleError<any>('Error saving Partner testing details'))
        );
    }

    public savePatientEducation(patientEducation: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientEducationExamination/AddPatientCounsellingInfo',
            JSON.stringify(patientEducation), httpOptions).pipe(
            tap(savePatientEducation => this.errorHandler.log(`successfully added Partner testing details`)),
            catchError(this.errorHandler.handleError<any>('Error saving Partner testing details'))
        );
    }

    public saveDischarge(discharge: any): Observable<any> {
        return this.http.post(this.API_MATERNITY_URL + '/api/MaternityPatientDeliveryInfo/DischargePatient',
            JSON.stringify(discharge), httpOptions).pipe(
            tap(saveDischarge => this.errorHandler.log(`successfully added discharge details`)),
            catchError(this.errorHandler.handleError<any>('Error saving Partner discharge details'))
        );
    }

    public saveReferrals(referral: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/postReferral', JSON.stringify(referral), httpOptions).pipe(
            tap(saveReferrals => this.errorHandler.log(`successfully added Referral details`)),
            catchError(this.errorHandler.handleError<any>('Error saving Referral details'))
        );
    }

    public saveNextAppointment(appointment: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/postNextAppointment', JSON.stringify(appointment),
            httpOptions).pipe(
            tap(saveReferrals => this.errorHandler.log(`successfully added Referral details`)),
            catchError(this.errorHandler.handleError<any>('Error saving Referral details'))
        );
    }

}
