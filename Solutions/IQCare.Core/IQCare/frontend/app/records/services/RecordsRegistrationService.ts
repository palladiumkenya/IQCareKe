import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Person} from '../models/person'
import 'rxjs/add/observable/throw';
import { catchError, tap } from 'rxjs/operators';
import 'rxjs/add/observable/of';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class RegistrationService {
    private API_URL = environment.API_URL;
    private _lookupurl = '/api/lookup';
    private _url = '/records/api/Register';
    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getRegistrationOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/registrationOptions').pipe(
            tap(registrationoptions => this.errorHandler.log('fetched all registration options')),
            catchError(this.errorHandler.handleError<any[]>('getRegistrationOptions'))
        );
    }

    public getOppConsentEduOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getRegOccConsentEducationOptions').pipe(
            tap(OppEduConsentOptions => this.errorHandler.log('fetched all Occupation and Education options')),
            catchError(this.errorHandler.handleError<any[]>('getRegOccConsentEducationOptions'))
        );


    }

    public getOppConsentType(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getConsentType').pipe(
            tap(OppEduConsentOptions => this.errorHandler.log('fetched the consenttype')),
            catchError(this.errorHandler.handleError<any[]>('getConsentType'))
        );


    }


    public getIdentifierType(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getIdentifyerTypes').pipe(
            tap(IdentifiertypeOptions => this.errorHandler.log('fetched all identifyer options')),
            catchError(this.errorHandler.handleError<any[]>('getIdentifyerTypes'))
        );


    }


    public registerClient(person: Person): Observable<Person> {
        const Indata = {
            'Person': person
        };
        return this.http.post(this.API_URL + this._url +'/addPerson', JSON.stringify(Indata), httpOptions).pipe(
            tap((registeredClient: Person) => this.errorHandler.log(`added client w/ id`)),
            catchError(this.errorHandler.handleError<Person>('addPerson'))
        );

    }

    public addPersonEducationalLevel(personId: number, educ: number, userId: number): Observable<any> {
        const Indata = {
            PersonId: personId,
            EducationalLevel: educ,
            UserID: userId
        };
        return this.http.post<any>(this.API_URL + this._url + '/addPersonEducationalLevel', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonEducationalLevel: any) => this.errorHandler.log(`added person educational lvel w/ id`)),
            catchError(this.errorHandler.handleError<any>('addEducationLevel'))
        );

    }


    public addPersonOccupation(personId: number, occ: number, userId: number): Observable<any> {
        const Indata = {
            PersonId: personId,
            occupation: occ,
            UserID: userId
        };
        return this.http.post<any>(this.API_URL + this._url + '/addPersonOccupation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonOccupation: any) => this.errorHandler.log(`added person occupation w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonOccupation'))
        );

    }


  


    public addPersonIdentifier(personId: number, identifierid: number, idvalue: string,userId: number): Observable<any> {
        const Indata = {
            PersonId: personId,
            IdentifierId: identifierid,
            IdentifierValue:idvalue,
            UserID: userId
        };
        return this.http.post<any>(this.API_URL + this._url + '/addPersonIdentifier', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonIdentifier: any) => this.errorHandler.log(`added person identifierType w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonIdentifier'))
        );

    }

    public addPersonLocation(personId: number, countyId: number, subCountyId: number,
        wardId: number, userId: number, landMark: string): Observable<any> {


        const Indata = {
            PersonId: personId,
            CountyId: countyId,
            SubCountyId: subCountyId,
            WardId: wardId,
            Village: ' ',
            LandMark: landMark,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonLocation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonLocation: any) => this.errorHandler.log(`added person location w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonLocation'))
        );
    }


    public addPersonEmergencyContact(personId: number, firstname: string, middlename: string, lastname: string,
        gender: number, emergencycontactpersonid: number, mobilecontact: string, createdby: number, deleteflag: boolean, relationshiptype: number, consentType: number, ConsentValue: number
        , ConsentReason:string): Observable<any> {

        const Indata = {
            personId: personId,
            firstname: firstname,
            lastname: lastname,
            middlename: middlename,
            gender: gender,
            emergencycontactpersonid: emergencycontactpersonid,
            mobilecontact: mobilecontact,
            createdby: createdby,
            deleteflag: deleteflag,
            relationshiptype: relationshiptype,
            ConsentValue:ConsentValue,
            ConsentReason:ConsentReason,
            consentType:consentType

        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonEmergencyContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonEmergencyContact: any) => this.errorHandler.log(`added person  emergencycontact w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonEmergencyContact'))
        );

    }

}