import { ClientAddress } from './../_models/clientaddress';
import { Person } from './../_models/person';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '../../../../node_modules/@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable } from '../../../../node_modules/rxjs';
import { tap, catchError } from '../../../../node_modules/rxjs/operators';
import { ClientContact } from '../_models/clientcontact';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PersonRegistrationService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {
    }

    public registerPerson(person: Person): Observable<any> {
        const Indata = {
            'Person': person
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPerson', JSON.stringify(Indata), httpOptions).pipe(
            tap((registerPerson: any) => this.errorHandler.log(`registered person`)),
            catchError(this.errorHandler.handleError<any>('registerPerson'))
        );
    }

    public addPersonContact(personId: number, userId: number, clientContact: ClientContact): Observable<any> {
        const Indata = {
            'PersonId': personId,
            'PhysicalAddress': '',
            'MobileNumber': clientContact.MobileNumber,
            'AlternativeNumber': clientContact.AlternativeMobileNumber,
            'EmailAddress': clientContact.EmailAddress,
            'UserId': userId
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonContact: any) => this.errorHandler.log(`add person contact`)),
            catchError(this.errorHandler.handleError<any>('addPersonContact'))
        );
    }

    public addPersonAddress(personId: number, userId: number, clientAddress: ClientAddress): Observable<any> {
        const Indata = {
            'PersonId': personId,
            'CountyId': clientAddress.County,
            'SubCountyId': clientAddress.SubCounty,
            'WardId': clientAddress.Ward,
            'NearestHealthCentre': clientAddress.NearestHealthCenter,
            'LandMark': clientAddress.Landmark,
            'UserId': userId
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonLocation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonAddress: any) => this.errorHandler.log(`add person address`)),
            catchError(this.errorHandler.handleError<any>('addPersonAddress'))
        );
    }

    public addPersonMaritalStatus(personId: number, userId: number, maritalStatusId: number): Observable<any> {
        const Indata = {
            'PersonId': personId,
            'MaritalStatusId': maritalStatusId,
            'CreatedBy': userId
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonMaritalStatus', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonMaritalStatus: any) => this.errorHandler.log(`add person marital status`)),
            catchError(this.errorHandler.handleError<any>('addPersonMaritalStatus'))
        );
    }
}
