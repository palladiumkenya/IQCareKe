import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PersonView } from '../../records/_models/personView';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PersonHomeService {
    private API_URL = environment.API_URL;
    private _url = '/api/PatientServices/GetPatientByPersonId';
    public person: PersonView;
    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getPatientByPersonId(personId: Number): Observable<PersonView> {
        console.log(personId);
        return this.http.get<PersonView>(this.API_URL + '' + this._url + '/' + personId).pipe(
            tap(getPatientByPersonId => this.errorHandler.log('get ' + personId + 'options by Name')),
            catchError(this.errorHandler.handleError<PersonView>('getPatientByPersonId'))
        );
    }

    public getAllServices(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/ServiceArea/GetAllServices').pipe(
            tap(getAllServices => this.errorHandler.log(`get all facility services`)),
            catchError(this.errorHandler.handleError<PersonView>('getAllServices'))
        );
    }

    public getPersonEnrolledServices(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetEnrolledServicesByPersonId/' + personId).pipe(
            tap(getPersonEnrolledServices => this.errorHandler.log(`get person enrolled services`)),
            catchError(this.errorHandler.handleError<any>('getPersonEnrolledServices'))
        );
    }

    public getServiceAreaIdentifiers(serviceAreaId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/GetServiceAreaIdentifiers/' + serviceAreaId).pipe(
            tap(getServiceAreaIdentifiers => this.errorHandler.log(`get service area identifiers`)),
            catchError(this.errorHandler.handleError<any>('getServiceAreaIdentifiers'))
        );
    }

    public getPatientTypes(): Observable<any[]> {
        const options = JSON.stringify(['PatientType']);

        return this.http.post<any[]>(this.API_URL + '/api/Lookup/getCustomOptions', options, httpOptions).pipe(
            tap(getPatientType => this.errorHandler.log('fetched patien type options')),
            catchError(this.errorHandler.handleError<any[]>('getPatientType'))
        );
    }


    public getChronicIllnessesByPatientId(patientId:number) : Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientChronicIllness/GetByPatientId/' + patientId).pipe(
            tap(getChronicIllnessesByPatientId => this.errorHandler.log(`get patient chronic illnesses`)),
            catchError(this.errorHandler.handleError<any>('getChronicIllnessesByPatientId'))
        );
    }
    public getRelationshipsByPatientId(patientId:number) : Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetRelationshipsByPatientId/' + patientId).pipe(
            tap(getRelationshipsByPatientId => this.errorHandler.log(`get patient relationships`)),
            catchError(this.errorHandler.handleError<any>('getRelationshipsByPatientId'))
        );
    }

}
