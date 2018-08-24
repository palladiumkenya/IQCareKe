import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { tap, catchError } from 'rxjs/operators';
import { LookupItemView } from '../../shared/_models/LookupItemView';

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

    public getPersonDetails(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/records/api/Register/GetPersonDetails/' + personId).pipe(
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
}
