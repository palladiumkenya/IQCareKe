import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

import { catchError, tap } from 'rxjs/operators';
import { Search, SearchList, SearchContact, SearchRegList } from '../models/search';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class SearchService {
    private API_URL = environment.API_URL;
    private _url = '/records/api/Register/search';
    private _urlcontact = '/records/api/Register/SearchContact';
    private _urlpersonlist = '/records/api/Register/searchpersonlist';
    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public searchPerson(personsearch: Search): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._url + '?identificationNumber=' + personsearch.identifierValue +
            '&firstName=' + personsearch.firstName + '&middleName=' + personsearch.midName + '&lastName='
            + personsearch.lastName + '&enrollmentNumber=' + personsearch.EnrollmentNumber + '&notClient='
            + personsearch.NotClient, httpOptions).pipe(
                tap((searchClient: any) => this.errorHandler.log(`search client`)),
                catchError(this.errorHandler.handleError<any>('searchClients'))
            );
    }

    public searchPersonReg(personsearch: SearchRegList): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._urlpersonlist + '?identificationNumber=' + personsearch.identifierValue +
            '&firstName=' + personsearch.firstName + '&middleName=' + personsearch.midName +
            '&lastName=' + personsearch.lastName + '&dateofBirth=' + personsearch.DateofBirth, httpOptions).pipe(
                tap((searchClient: any) => this.errorHandler.log(`search client`)),
                catchError(this.errorHandler.handleError<any>('searchClients'))
            );
    }

    public searchPersonContact(personsearch: SearchContact): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._urlcontact + '?identificationNumber=' + personsearch.identifierValue +
            '&firstName=' + personsearch.firstName + '&middleName=' + personsearch.midName +
            '&lastName=' + personsearch.lastName + '&enrollmentNumber=' + personsearch.EnrollmentNumber, httpOptions).pipe(
                tap((searchClient: any) => this.errorHandler.log(`search client`)),
                catchError(this.errorHandler.handleError<any>('searchClients'))
            );
    }

}

