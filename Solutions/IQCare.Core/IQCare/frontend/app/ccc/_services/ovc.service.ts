import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { group } from '@angular/animations';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class OvcService {
    private API_URL = environment.API_URL;
    private url = '/api/Register';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    public getByGroupName(groupName: string): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetByGroupName/' + groupName)
            .pipe(tap(getByGroupName => this.errorHandler.log('get ' + groupName + 'options by Name')),
                catchError(this.errorHandler.handleError<LookupItemView[]>('getByGroupName', []))
            );
    }

    public getByGroupMasterName(groupName: string): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + groupName)
            .pipe(tap(getByGroupMasterName =>
                this.errorHandler.log('get' + groupName + 'options by Name')),
                catchError(this.errorHandler.handleError<LookupItemView[]>('getByGroupMasterName', [])));
    }

    public getPatientCareEndDetails(patientmasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientServices/GetPatientCareEndedDetails/' + patientmasterVisitId).pipe(
            tap(getPatientCareEndDetails => this.errorHandler.log(`successfully fetch careended details`)),
            catchError(this.errorHandler.handleError<any>('Error fetching careend details'))
        );
    }

    public careEndPatientdetails(patientId: number, serviceAreaId: number,
        patientmasterVisitId: number, careEndDate: string, specify: string
        , disclosurereason: number, deathdate: string, userId: number): Observable<any> {

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
        return this.http.post(this.API_URL + '/api/PatientServices/CareEndPatient', JSON.stringify(Indata),
         httpOptions).pipe(tap(careEndPatientdetails => this.errorHandler.log(`CareEnding the patient`)),
                catchError(this.errorHandler.handleError<any[]>('careEndPatientdetails'))
            );
    }

    public getClientFamily(patientId: number): Observable<any[]> {


        return this.http.get<any[]>(this.API_URL + this.url + '/getCaregiver/'
            + patientId).pipe(
                tap(getClientFamily => this.errorHandler.log('fetched all family')),
                catchError(this.errorHandler.handleError<any[]>('getClientFamily'))
            );
    }

    public enrollOVC(personId: number, partnerOvcServices: string,
        cpmis: number, enrollmentdate: string, createdby: number): Observable<any> {
        const Indata = {
            'PersonId': personId,
            'PartnerOVCServices': partnerOvcServices,
            'CPMISEnrolled': cpmis,
            'EnrollmentDate': enrollmentdate,
            'CreatedBy': createdby
        };

        return this.http.post(this.API_URL + this.url + '/ovcEnrollment'
            , JSON.stringify(Indata), httpOptions).pipe(
                tap((enrollOVC => this.errorHandler.log(`Enroll OVC client w/ id`)),
                    catchError(this.errorHandler.handleError<any[]>('clientOVC  Enrollment'))
                ));
    }

    public getEnrollOVCInformation(personId: number): Observable<any[]> {

        return this.http.get<any[]>(this.API_URL + this.url + '/GetOvcEnrollmentDetails/'
            + personId).pipe(
                tap(getClientFamily => this.errorHandler.log('fetched all details')),
                catchError(this.errorHandler.handleError<any[]>('getEnrollmentDetails'))
            );

    }


}







