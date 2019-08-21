import { HivReConfirmatoryTestsCommand } from './../../dashboard/_model/HivReConfirmatoryTestsCommand';

import { of as observableOf, Observable } from 'rxjs';
import { PersonDetails } from './../_models/persondetails';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, tap } from 'rxjs/operators';
import { Person } from '../_models/person';

import { PersonPopulation } from '../_models/personPopulation';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { PersonPopulationDetails } from '../_models/personpopulationdetails';
import { ServiceEntryPointCommand } from '../../dashboard/_model/ServiceEntryPointCommand';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class RegistrationService {
    private API_URL = environment.API_URL;
    private _lookupurl = '/api/lookup';
    private _url = '/api/Register';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getPersonPriorityDetails(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Registration/Person/GetPersonPriorityDetails/' + personId).pipe(
            tap(getPersonPriorityDetails => this.errorHandler.log(`fetched person priority list details for personId` + personId)),
            catchError(this.errorHandler.handleError<any[]>('getPersonPriorityDetails'))
        );
    }

    public getPersonPopulationDetails(personId: number): Observable<PersonPopulationDetails[]> {
        return this.http.get<PersonPopulationDetails[]>(this.API_URL + '/api/Registration/Person/GetPersonPopulationDetails/' +
            personId).pipe(
                tap(getPersonPopulationDetails => this.errorHandler.log(`fetched person population list details for personId` + personId)),
                catchError(this.errorHandler.handleError<any[]>('getPersonPopulationDetails'))
            );
    }

    public getPersonDetails(personId: number): Observable<PersonDetails> {
        return this.http.get<any>(this.API_URL + '/api/Registration/Person/getPersonDetails/' + personId).pipe(
            tap(getPersonDetails => this.errorHandler.log(`fetched person details for ` + personId)),
            catchError(this.errorHandler.handleError<any[]>('getPersonDetails'))
        );
    }

    public getRegistrationOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/registrationOptions').pipe(
            tap(registrationoptions => this.errorHandler.log('fetched all registration options')),
            catchError(this.errorHandler.handleError<any[]>('getRegistrationOptions'))
        );
    }

    public updatePersonDetails(person: Person): Observable<Person> {
        const Indata = {
            'PersonId': person.Id,
            'FirstName': person.FirstName,
            'MiddleName': person.MiddleName,
            'LastName': person.LastName,
            'DateOfBirth': person.DateOfBirth,
            'MaritalStatus': person.MaritalStatus,
            'Sex': person.Sex,
            'IsPartner': person.isPartner,
            'PatientId': person.patientId,
            'CreatedBy': person.createdBy
        };

        return this.http.post<Person>(this.API_URL + '/api/Register/UpdatePerson', JSON.stringify(Indata), httpOptions).pipe(
            tap((client: Person) => this.errorHandler.log(`added client w/ id` + client.Id)),
            catchError(this.errorHandler.handleError<Person>('registerClient'))
        );
    }

    public registerClient(person: Person): Observable<Person> {

        const Indata = {
            'Person': person
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((client: Person) => this.errorHandler.log(`added client w/ id` + client.Id)),
            catchError(this.errorHandler.handleError<Person>('registerClient'))
        );
    }

    public addPatient(personId: number, userId: number, enrollmentDate: string, posId: string): Observable<any> {
        const Indata = {
            PersonId: personId,
            UserId: userId,
            EnrollmentDate: enrollmentDate,
            PosId: posId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPatient', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPatient: any) => this.errorHandler.log(`added patient w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPatient'))
        );
    }

    public updatePersonContact(personId: number, physicalAddress: string, mobileNumber: string,
        alternativeNumber: string, emailAddress: string, userId: number): Observable<any> {
        if (!mobileNumber) {
            return observableOf([]);
        }

        const Indata = {
            PersonId: personId,
            PhysicalAddress: physicalAddress,
            MobileNumber: mobileNumber,
            AlternativeNumber: alternativeNumber,
            EmailAddress: emailAddress,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/updatePersonContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((updatePersonContact: any) => this.errorHandler.log(`updated person contact`)),
            catchError(this.errorHandler.handleError<any>('updatePersonContact'))
        );
    }

    public addPersonContact(personId: number, physicalAddress: string, mobileNumber: string,
        alternativeNumber: string, emailAddress: string, userId: number): Observable<any> {

        if (!mobileNumber) {
            return observableOf([]);
        }

        const Indata = {
            PersonId: personId,
            PhysicalAddress: physicalAddress,
            MobileNumber: mobileNumber,
            AlternativeNumber: alternativeNumber,
            EmailAddress: emailAddress,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonContact: any) => this.errorHandler.log(`added person contact w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonContact'))
        );
    }

    public updatePersonMaritalStatus(personId: number, maritalStatusId: number, userId: number): Observable<any> {
        if (!maritalStatusId) {
            return observableOf([]);
        }

        const Indata = {
            PersonId: personId,
            MaritalStatusId: maritalStatusId,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/updatePersonMaritalStatus', JSON.stringify(Indata), httpOptions).pipe(
            tap((updatePersonMaritalStatus: any) => this.errorHandler.log(`updated person marital status w/ id`)),
            catchError(this.errorHandler.handleError<any>('updatePersonMaritalStatus'))
        );
    }

    public addPersonMaritalStatus(personId: number, maritalStatusId: number, userId: number): Observable<any> {
        if (!maritalStatusId) {
            return observableOf([]);
        }

        const Indata = {
            PersonId: personId,
            MaritalStatusId: maritalStatusId,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPersonMaritalStatus', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonMaritalStatus: any) => this.errorHandler.log(`added person marital status w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonMaritalStatus'))
        );
    }

    public updatePersonLocation(personId: number, countyId: number, subCountyId: number,
        wardId: number, userId: number, landMark: string): Observable<any> {
        if (!landMark) {
            return observableOf([]);
        }

        const Indata = {
            PersonId: personId,
            CountyId: countyId,
            SubCountyId: subCountyId,
            WardId: wardId,
            Village: ' ',
            LandMark: landMark,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/UpdatePersonLocation', JSON.stringify(Indata), httpOptions).pipe(
            tap((updatePersonLocation: any) => this.errorHandler.log(`updated person location w/ id`)),
            catchError(this.errorHandler.handleError<any>('updatePersonLocation'))
        );

    }

    public addPersonLocation(personId: number, countyId: number, subCountyId: number,
        wardId: number, userId: number, landMark: string): Observable<any> {
        if (!landMark) {
            return observableOf([]);
        }

        const Indata = {
            PersonId: personId,
            CountyId: countyId,
            SubCountyId: subCountyId,
            WardId: wardId,
            Village: ' ',
            LandMark: landMark,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/AddPersonLocation', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonLocation: any) => this.errorHandler.log(`added person location w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonLocation'))
        );
    }

    public addPersonRelationship(personRelationship: any): Observable<any> {
        if (!personRelationship['RelationshipTypeId']) {
            return observableOf([]);
        }
        return this.http.post<any>(this.API_URL + this._url + '/addPersonRelationship',
            JSON.stringify(personRelationship), httpOptions).pipe(
                tap((addPersonRelationship: any) => this.errorHandler.log(`added new person relationship w/ id`)),
                catchError(this.errorHandler.handleError<any>('addPersonRelationship'))
            );
    }

    public addPatientARVHistory(patientId: number, serviceId: number, treatmentType: string,
        purpose: string, regimen: string, createdby: number, deleteflag: boolean
        , months: number, initiationDate?: Date, datelastused?: Date) {
        const Indata = {
            PatientId: patientId,
            ServiceId: serviceId,
            TreatmentType: treatmentType,
            Purpose: purpose,
            Regimen: regimen,
            DeleteFlag: deleteflag,
            CreatedBy: createdby,
            Months: months,
            InitiationDate: initiationDate,
            DateLastUsed: datelastused

        }
        return this.http.post<any>(this.API_URL + this._url + '/addPatientARVHistory', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPatientARVHistory: any) => this.errorHandler.log(`added patient ARV history w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPatientARVHistory'))
        );

    }

    public addPatientTransferIn(patientId: number, serviceId: number, transferInDate: Date,
        treatmentstartdate: Date, currenttreatment: string, facilityfrom: string
        , mflcode: number, countyfrom: string, transferinnotes: string, createdby: number, deleteflag: boolean) {
        const Indata = {
            PatientId: patientId,
            ServiceId: serviceId,
            TransferInDate: transferInDate,
            TreatmentStartDate: treatmentstartdate,
            CurrentTreatment: currenttreatment,
            FacilityFrom: facilityfrom,
            MflCode: mflcode,
            CountyFrom: countyfrom,
            TransferInNotes: transferinnotes,
            CreatedBy: createdby,
            DeleteFlag: deleteflag
        }
        return this.http.post<any>(this.API_URL + this._url + '/addPatientTransferIn', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPatientTransferIn: any) => this.errorHandler.log(`added patient transfer in details w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPatientTransferIn'))
        );

    }

    public addPatientOvcStatus(personId: number, orphan: number, inschool: number,
        active: boolean, deleteflag: boolean, createdby: number) {

        const Indata = {
            PersonId: personId,
            Orphan: orphan,
            InSchool: inschool,
            Active: active,
            Deleteflag: deleteflag,
            CreatedBy: createdby
        }
        return this.http.post<any>(this.API_URL + this._url + '/addPatientOVCStatus', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPatientOVCStatus: any) => this.errorHandler.log(`added patient OVC status in details w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPatientOVCStatus'))
        );


    }



    public addPersonPopulationType(personId: number, userId: number, populations: PersonPopulation): Observable<any> {
        // console.log(populations);
        const pops = [];
        let priority = [];
        if (populations.populationType == 1) {
            const item = {
                PopulationType: 'General Population',
                PopulationCategory: 0
            };
            pops.push(item);
        }
        if (populations.populationType == 2) {
            for (let i = 0; i < populations.KeyPopulation.length; i++) {
                const item = {
                    PopulationType: 'Key Population',
                    PopulationCategory: populations.KeyPopulation[i]
                };
                pops.push(item);
            }
        }

        if (populations.populationType == 3) {
            for (let i = 0; i < populations.DiscordantCouplePopulation.length; i++) {
                const item = {
                    PopulationType: 'Discordant Couple',
                    PopulationCategory: 0
                };
                pops.push(item);
            }
        }


        if (populations.priorityPop === 1) {
            priority = populations.priorityPopulation.map(priorityId => ({ priorityId }));
        }

        const Indata = {
            PersonId: personId,
            Population: pops,
            Priority: priority,
            UserId: userId
        };

        return this.http.post<any>(this.API_URL + this._url + '/AddPersonPopulationType', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonPopulationType: any) => this.errorHandler.log(`added person population type`)),
            catchError(this.errorHandler.handleError<any>('addPersonPopulationType'))
        );
    }

    public addServiceEntryPoint(serviceEntryPoint: ServiceEntryPointCommand): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/Register/postServiceEntryPoint',
            JSON.stringify(serviceEntryPoint), httpOptions).pipe(
                tap((addServiceEntryPoint: any) => this.errorHandler.log(`added service entry point`)),
                catchError(this.errorHandler.handleError<any>('addServiceEntryPoint'))
            );
    }



    public addReConfirmatoryTest(hivReConfirmatoryTests: HivReConfirmatoryTestsCommand): Observable<any> {
        if (!hivReConfirmatoryTests.TypeOfTest) {
            return observableOf([]);
        }
        return this.http.post<any>(this.API_URL + '/api/Register/postConfirmatoryTests',
            JSON.stringify(hivReConfirmatoryTests), httpOptions).pipe(
                tap((addReConfirmatoryTest: any) => this.errorHandler.log(`added re-confirmatory test`)),
                catchError(this.errorHandler.handleError<any>('addReConfirmatoryTest'))
            );
    }
}
