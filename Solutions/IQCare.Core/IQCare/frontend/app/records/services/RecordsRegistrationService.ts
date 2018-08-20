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
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Person2,EmergencyArray,EmergencyContacts} from '../models/person'
import 'rxjs/add/observable/throw';
import { catchError, tap } from 'rxjs/operators';
import 'rxjs/add/observable/of';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Http, RequestOptions, URLSearchParams } from '@angular/http'
import {  HttpParams } from '@angular/common/http';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class RegistrationService {
    arrayemergency: EmergencyArray[] = [];
    Emerg: EmergencyArray;
    
    private API_URL = environment.API_URL;
    private _lookupurl = '/api/lookup';
    private _url = '/records/api/Register';
    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getGenderOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getGenderOptions').pipe(
            tap(registrationoptions => this.errorHandler.log('fetched all  gender options')),
            catchError(this.errorHandler.handleError<any[]>('getGenderOptions'))
        );
    }

    public getRelOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getRelOptions').pipe(
            tap(registrationoptions => this.errorHandler.log('fetched all  relationship options')),
            catchError(this.errorHandler.handleError<any[]>('getRelOptions'))
        );
    }

    public getContactTypeOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getContactType').pipe(
            tap(contacttypeoptions => this.errorHandler.log('fetched all contact type')),
            catchError(this.errorHandler.handleError<any[]>('getContactType'))
        );
    }

    public getMaritalStatusOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getMaritalStatusOptions').pipe(
            tap(OppEduConsentOptions => this.errorHandler.log('fetched marital status options ')),
            catchError(this.errorHandler.handleError<any[]>('getMaritalStatusOptions'))
        );


    }


    public getOppOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getocc').pipe(
            tap(OppEduConsentOptions => this.errorHandler.log('fetched all Occupation ')),
            catchError(this.errorHandler.handleError<any[]>('getocc'))
        );


    }

    public getOppConsentOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getRegConsentOptions').pipe(
            tap(OppEduConsentOptions => this.errorHandler.log('fetched Education and consent options')),
            catchError(this.errorHandler.handleError<any[]>('getRegConsentOptions'))
        );


    }

    public getOppEducationOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getEducation').pipe(
            tap(OppEduConsentOptions => this.errorHandler.log('fetched Education  options')),
            catchError(this.errorHandler.handleError<any[]>('getEducation'))
        );


    }


    public getPersonDetails(personId: number): Observable<any[]> {
        
        return this.http.get<any[]>(this.API_URL + this._url + '/GetPersonDetails?personId=' +personId , httpOptions).pipe(
            tap((personDetails: any) => this.errorHandler.log(`get person details`)),
            catchError(this.errorHandler.handleError<any>('Get personal details'))
            );
    }

    public getSubCounty(county: string):Observable<any[]>{
        let params = new HttpParams().set('countyid', county).set('subcountyid', '0');
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getSubCountylist', { params }).pipe(tap(OppSubCounties => this.errorHandler.log("fetched all subcounties")),
            catchError(this.errorHandler.handleError<any[]>('getSubCounty'))
        );

    }

    public getWardList(subcounty: string): Observable<any[]>{
        let params = new HttpParams().set('countyid', '0').set('subcountyid', subcounty);
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getWardlist', { params }).pipe(tap(OppWardLisr => this.errorHandler.log("fetched all wardlist")),
            catchError(this.errorHandler.handleError<any[]>('getWardList'))
        );
    }
    
    public getCounties():Observable<any[]>{
        let params = new HttpParams().set('countyid', '0').set('subcountyid','0');

     return this.http.get<any[]>(this.API_URL + this._lookupurl + '/getCountylist', { params }).pipe(tap(OppCounties => this.errorHandler.log("fetched all counties")),
            catchError(this.errorHandler.handleError<any[]>('getCounties'))
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


    public registerClient(person: Person2): Observable<Person2> {
        const Indata = {
            'Person': person
        };
        return this.http.post(this.API_URL + this._url +'/addPerson', JSON.stringify(Indata), httpOptions).pipe(
            tap((registeredClient: Person2) => this.errorHandler.log(`added client w/ id`)),
            catchError(this.errorHandler.handleError<Person2>('addPerson'))
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

    public addPatientRegistration(personId:number,dateofBirth:string,userId:number,nationalId:string,registrationDate:string): Observable<any> {
        const Indata = {
            PersonId: personId,
            DateOfBirth: dateofBirth,
            UserId: userId,
            NationalId: nationalId,
            RegistrationDate:registrationDate
        };
        return this.http.post<any>(this.API_URL + this._url + '/addPatientRegistration', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonOccupation: any) => this.errorHandler.log(`added patient registration w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPatientRegistration'))
        );

    }

    public addPersonContact(personId: number, physicalAddress: string, mobileNumber: string,
        alternativeNumber: string, emailAddress: string, userId: number): Observable<any> {

        if (!mobileNumber) {
            return Observable.of([]);
        }

        const Indata = {
            PersonId: personId,
            PhysicalAddress: physicalAddress,
            MobileNumber: mobileNumber,
            AlternativeNumber: alternativeNumber,
            EmailAddress: emailAddress,
            UserId: userId
        };
        console.log(Indata);

        return this.http.post<any>(this.API_URL + this._url + '/addPersonContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonContact: any) => this.errorHandler.log(`added person contact w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonContact'))
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
        wardId: number, userId: number, landMark: string,nearestHealthCenter:string): Observable<any> {


        const Indata = {
            PersonId: personId,
            CountyId: countyId,
            SubCountyId: subCountyId,
            WardId: wardId,
            Village: ' ',
            LandMark: landMark,
            UserId: userId,
            NearestHealthCentre :nearestHealthCenter
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonLocation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonLocation: any) => this.errorHandler.log(`added person location w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonLocation')
            )
            
        );
    }


    public addPersonEmergencyContact(): Observable<any> {

        const InData = {
            EmergencyContacts: this.arrayemergency
        }
       

         return this.http.post<any>(this.API_URL + this._url + '/addPersonEmergencyContact', JSON.stringify(InData), httpOptions).pipe(
            tap((addPersonEmergencyContact: any) => this.errorHandler.log(`added person  emergencycontact w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonEmergencyContact'))

            
        );
        

    }


    public cleararray() {
        this.arrayemergency.length = 0;
    }
    public addPersonEmergencyContactArray(personId: number, firstname: string, middlename: string, lastname: string,
        gender: number, emergencycontactpersonid: number, mobilecontact: string, createdby: number, deleteflag: boolean, relationshiptype: number, consentType: number, ConsentValue: number
        , ConsentReason: string,RegisteredtoClinic:number,emgEmergencyContactType:number,emgNextofKinContactType:number): EmergencyArray[] {
        this.Emerg = new EmergencyArray();
        
        this.Emerg.PersonId = personId;
        this.Emerg.firstname = firstname;
        this.Emerg.middlename = middlename;
        this.Emerg.lastname = lastname;
        this.Emerg.gender = gender;
        this.Emerg.EmergencyContactPersonId = emergencycontactpersonid;
        this.Emerg.MobileContact = mobilecontact;
        this.Emerg.CreatedBy = createdby;
        this.Emerg.DeleteFlag = deleteflag;
        this.Emerg.RelationshipType = relationshiptype;
        this.Emerg.ConsentType = consentType
        this.Emerg.ConsentValue = ConsentValue;
        this.Emerg.ConsentReason = ConsentReason;
       
        this.Emerg.emgEmergencyContactType = emgEmergencyContactType;
        this.Emerg.emgNextofKinContactType = emgNextofKinContactType;
        if (RegisteredtoClinic = 1) {
            this.Emerg.RegisteredToClinic = true;
        }
        else if (RegisteredtoClinic = 0) {
            this.Emerg.RegisteredToClinic = false;
        }

        this.arrayemergency.push(this.Emerg);

        return this.arrayemergency;
       

    }

}