import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { catchError, tap } from 'rxjs/operators';
import { Person } from '../_models/person';
import 'rxjs/add/observable/of';
import { PersonPopulation } from '../_models/personPopulation';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';

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

    public getRegistrationOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl + '/registrationOptions').pipe(
            tap(registrationoptions => this.errorHandler.log('fetched all registration options')),
            catchError(this.errorHandler.handleError<any[]>('getRegistrationOptions'))
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

    public addPatient(personId: number, dateOfBirth: string): Observable<any> {
        const Indata = {
            PersonId: personId,
            DateOfBirth: dateOfBirth
        };

        return this.http.post<any>(this.API_URL + this._url + '/addPatient', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPatient: any) => this.errorHandler.log(`added patient w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPatient'))
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

        return this.http.post<any>(this.API_URL + this._url + '/addPersonContact', JSON.stringify(Indata), httpOptions).pipe(
            tap((addPersonContact: any) => this.errorHandler.log(`added person contact w/ id`)),
            catchError(this.errorHandler.handleError<any>('addPersonContact'))
        );
    }

    public addPersonMaritalStatus(personId: number, maritalStatusId: number, userId: number): Observable<any> {
        if (!maritalStatusId) {
            return Observable.of([]);
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

    public addPersonLocation(personId: number, countyId: number, subCountyId: number,
        wardId: number, userId: number, landMark: string): Observable<any> {
        if (!landMark) {
            return Observable.of([]);
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
        if (!personRelationship) {
            return Observable.of([]);
        }

        return this.http.post<any>(this.API_URL + this._url + '/addPersonRelationship',
            JSON.stringify(personRelationship), httpOptions).pipe(
                tap((addPersonRelationship: any) => this.errorHandler.log(`added new person relationship w/ id`)),
                catchError(this.errorHandler.handleError<any>('addPersonRelationship'))
            );
    }

    public addPersonPopulationType(personId: number, userId: number, populations: PersonPopulation): Observable<any> {
        console.log(populations);
        const pops = [];
        let priority = [];
        if (populations.populationType == 1) {
            const item = {
                PopulationType: 'General Population',
                PopulationCategory: 0
            };
            pops.push(item);
        } else {
            for (let i = 0; i < populations.KeyPopulation.length; i++) {
                const item = {
                    PopulationType: 'Key Population',
                    PopulationCategory: populations.KeyPopulation[i]
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
}
