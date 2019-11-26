import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { tap, catchError } from 'rxjs/operators';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import {MatchDuplicatePerson} from '../_models/matchduplicate';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class RecordsService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public getGenderOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'Gender').pipe(
            tap(getGenderOptions => this.errorHandler.log('get gender options')),
            catchError(this.errorHandler.handleError<any[]>('getGenderOptions'))
        );
    }

    public getMaritalStatusOptions(): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'MaritalStatus').pipe(
            tap(getMaritalStatusOptions => this.errorHandler.log('get marital status options')),
            catchError(this.errorHandler.handleError<any[]>('getMaritalStatusOptions'))
        );
    }

    public getEducationLevelOptions(): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'EducationalLevel').pipe(
            tap(getEducationLevelOptions => this.errorHandler.log('get education level options')),
            catchError(this.errorHandler.handleError<any[]>('getEducationLevelOptions'))
        );
    }

    public getOccupationOptions(): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'Occupation').pipe(
            tap(getOccupationOptions => this.errorHandler.log('get occupation options')),
            catchError(this.errorHandler.handleError<any[]>('getOccupationOptions'))
        );
    }

    public getRelationshipOptions(): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'KinRelationship').pipe(
            tap(getRelationshipOptions => this.errorHandler.log('get relationship options')),
            catchError(this.errorHandler.handleError<any[]>('getRelationshipOptions'))
        );
    }

    public getPersonDetails(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/records/api/Register/GetPersonDetails/' + personId).pipe(
            tap(getPersonDetails => this.errorHandler.log('get person details')),
            catchError(this.errorHandler.handleError<any[]>('getPersonDetails'))
        );
    }

    public getConsentToSms(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'ConsentOptions').pipe(
            tap(getConsentToSms => this.errorHandler.log('get consent to sms options')),
            catchError(this.errorHandler.handleError<any[]>('getConsentToSms'))
        );
    }

    public getContactCategory(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/GetOptionsByMasterName/' + 'ContactCategory').pipe(
            tap(getContactCategory => this.errorHandler.log('get contact categories options')),
            catchError(this.errorHandler.handleError<any[]>('getContactCategory'))
        );
    }

    public getPersonIdentifiers(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/getIdentifyerTypes').pipe(
            tap(getPersonIdentifiers => this.errorHandler.log('get person identifiers options')),
            catchError(this.errorHandler.handleError<any[]>('getPersonIdentifiers'))
        );
    }


    public getPersonNHIFIdentifiers(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/getNHIFIdentifyerTypes').pipe(
            tap(getPersonNHIFIdentifiers => this.errorHandler.log('get person NHIF identifiers options')),
            catchError(this.errorHandler.handleError<any[]>('getPersonNHIFIdentifiers'))
        );

    }

    public getPatientIdentifiersList(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Register/GetPatientIdentifiers/' + patientId).pipe(
            tap(getPatientIdentifiersList => this.errorHandler.log('get patient identifiers list')),
            catchError(this.errorHandler.handleError<any[]>('getPatientIdentifiersList'))
        );
    }

    public personEnrollmentDetails(patientId: number, serviceAreaId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Register?patientId=' + patientId + '&serviceAreaId=' + serviceAreaId).pipe(
            tap(personEnrollmentDetails => this.errorHandler.log('get person enrollment details list')),
            catchError(this.errorHandler.handleError<any[]>('personEnrollmentDetails'))
        );
    }
    
    public getDuplicatePersons(matchDuplicatePerson: MatchDuplicatePerson): Observable<any[]> {
        return this.http.post<any>(this.API_URL + '/api/Facility/GetDuplicatePersons', 
            JSON.stringify(matchDuplicatePerson), httpOptions).pipe();
    }
    
    public getAllIdentifiers(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/ServiceArea/GetAllIdentifiers').pipe();
    }
    
    public getPersonContacts(personId: number): Observable<any> {
        return this.http.get(this.API_URL + '/api/PatientServices/GetContactByPersonId/' + personId).pipe();
    }
}
