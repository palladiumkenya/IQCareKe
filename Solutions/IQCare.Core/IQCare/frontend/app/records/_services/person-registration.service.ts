import { ClientAddress } from './../_models/clientaddress';
import { Person } from './../_models/person';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '../../../../node_modules/@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from '../../../../node_modules/rxjs';
import { tap, catchError } from '../../../../node_modules/rxjs/operators';
import { ClientContact } from '../_models/clientcontact';
import { EmergencyContact } from '../_models/emergencycontact';

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

        if ((!clientContact.MobileNumber) && (!clientContact.AlternativeMobileNumber) && (!clientContact.EmailAddress)) {
            return of([]);
        }

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
            tap((addPersonMaritalStatus: any) => this.errorHandler.log(`added person marital status`)),
            catchError(this.errorHandler.handleError<any>('addPersonMaritalStatus'))
        );
    }

    public addPersonEducationLevel(personId: number, userId: number, educationLevelId: number): Observable<any> {
        if (!educationLevelId) {
            return of([]);
        }

        const Indata = {
            'PersonId': personId,
            'EducationalLevel': educationLevelId,
            'UserId': userId
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonEducationalLevel',
            JSON.stringify(Indata), httpOptions).pipe(
                tap((addPersonEducationLevel: any) => this.errorHandler.log(`added person education level`)),
                catchError(this.errorHandler.handleError<any>('addPersonEducationLevel'))
            );
    }

    public addPersonOccupation(personId: number, userId: number, occupationId: number): Observable<any> {
        if (!occupationId) {
            return of([]);
        }

        const Indata = {
            'PersonId': personId,
            'Occupation': occupationId,
            'UserId': userId
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonOccupation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonOccupation: any) => this.errorHandler.log(`added person occupation`)),
            catchError(this.errorHandler.handleError<any>('addPersonOccupation'))
        );
    }

    public registerPersonEmergencyContact(personId: number, userId: number, contactKin: any[]): Observable<any> {
        if (contactKin.length == 0) {
            return of([]);
        }

        const contacts = [];
        for (let i = 0; i < contactKin.length; i++) {
            contacts.push({
                'PersonId': personId,
                'firstname': contactKin[i]['firstName'],
                'middlename': contactKin[i]['middleName'],
                'lastname': contactKin[i]['lastName'],
                'gender': contactKin[i]['gender']['itemId'],
                'MobileContact': contactKin[i]['phoneno'],
                'CreatedBy': userId,
                'RelationshipType': contactKin[i]['relationship']['itemId'],
                'contactcategory': contactKin[i]['contactcategory']['itemId'],
                'consent': contactKin[i]['consent']['itemId'],
                'consentDecline': contactKin[i]['consentDecline']
            });
        }

        const Indata = {
            'Emergencycontact': contacts
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonEmergencyContact',
            JSON.stringify(Indata), httpOptions).pipe(
                tap((registerPersonEmergencyContact: any) => this.errorHandler.log(`register person emergency contact`)),
                catchError(this.errorHandler.handleError<any>('registerPersonEmergencyContact'))
            );
    }

    public getPersonKinContacts(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/records/api/Register/GetPersonKinContacts/' + personId).pipe(
            tap(getPersonKinContacts => this.errorHandler.log('get kin contacts')),
            catchError(this.errorHandler.handleError<any[]>('getPersonKinContacts'))
        );
    }

    public addPersonIdentifiers(personId: number, userId: number, identifierId: number, identifierValue: string): Observable<any> {
        const Indata = {
            'PersonId': personId,
            'IdentifierId': identifierId,
            'IdentifierValue': identifierValue,
            'UserId': userId
        };

        return this.http.post<any>(this.API_URL + '/records/api/Register/addPersonIdentifier', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonIdentifiers: any) => this.errorHandler.log(`add person identifiers`)),
            catchError(this.errorHandler.handleError<any>('addPersonIdentifiers'))
        );
    }

    getPersonIdentifiers(id: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/records/api/Register/GetPersonIdentifiers/' + id).pipe(
            tap(getPersonIdentifiers => this.errorHandler.log(`get person identifiers`)),
            catchError(this.errorHandler.handleError<any[]>('getPersonIdentifiers'))
        );
    }
}
