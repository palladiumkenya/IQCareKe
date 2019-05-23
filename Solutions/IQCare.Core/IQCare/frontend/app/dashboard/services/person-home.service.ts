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
    public getServiceArea(name: string): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/ServiceArea/GetServiceArea?name=' + name).pipe(
            tap(getServiceArea => this.errorHandler.log(`get service area`)),
            catchError(this.errorHandler.handleError<any>('getServiceArea'))
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

    public getPatientAllergies(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientAllergy/GetPatientAllergy?patientId=' + patientId).pipe(
            tap(getPatientAllergies => this.errorHandler.log('get patient Allergy')),
            catchError(this.errorHandler.handleError<any[]>('getPatientAllergies'))
        );
    }

    public getChronicIllnessesByPatientId(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientChronicIllness/GetByPatientId/' + patientId).pipe(
            tap(getChronicIllnessesByPatientId => this.errorHandler.log(`get patient chronic illnesses`)),
            catchError(this.errorHandler.handleError<any>('getChronicIllnessesByPatientId'))
        );
    }

    public getRelationshipsByPatientId(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetRelationshipsByPatientId/' + patientId).pipe(
            tap(getRelationshipsByPatientId => this.errorHandler.log(`get patient relationships`)),
            catchError(this.errorHandler.handleError<any>('getRelationshipsByPatientId'))
        );
    }


    public GetPatientAppoitment(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientReferralAndAppointment/GetPatientAppoitment/' + patientId).pipe(
            tap(getPatientAllergies => this.errorHandler.log('get patient Allergy')),
            catchError(this.errorHandler.handleError<any[]>('getPatientAllergies'))
        );
    }

    public getPatientById(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientById/' + patientId).pipe(
            tap(getPatientById => this.errorHandler.log(`get patient details`)),
            catchError(this.errorHandler.handleError<any>('getPatientById'))
        );
    }

    public getPatientModelByPersonId(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientByPersonId/' + personId).pipe(
            tap(getPatientModelByPersonId => this.errorHandler.log(`get patient model by personId ` + personId)),
            catchError(this.errorHandler.handleError<any>('getPatientModelByPersonId'))
        );
    }

    public getPersonPopulationType(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Registration/Person/GetPersonPopulationDetails/' + personId).pipe(
            tap(getPersonPopulationType => this.errorHandler.log(`get person population type ` + personId)),
            catchError(this.errorHandler.handleError<any>('getPersonPopulationType'))
        );
    }

    public getPatientServiceAreaEntryPoints(serviceAreaId: number, patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/getServiceEntryPoint/' + serviceAreaId + '/' + patientId).pipe(
            tap(getPatientServiceAreaEntryPoints => this.errorHandler.log(`get patient serviceentrypoints for patientId:  `
                + patientId + ` and serviceAreaId: ` + serviceAreaId)),
            catchError(this.errorHandler.handleError<any>('getPatientServiceAreaEntryPoints'))
        );
    }

    public getPatientEnrollmentDateByServiceAreaId(patientId: number, serviceAreaId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientEnrollmentByServiceAreaId/'
            + patientId + '/' + serviceAreaId).pipe(
                tap(getPatientEnrollmentDateByServiceAreaId => this.errorHandler.log(`get patient enrollment date for patientId:  `
                    + patientId + ` and serviceAreaId: ` + serviceAreaId)),
                catchError(this.errorHandler.handleError<any>('getPatientEnrollmentDateByServiceAreaId'))
            );
    }

    public getPersonPriorityTypes(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Registration/Person/GetPersonPriorityDetails/' + personId).pipe(
            tap(getPersonPriorityTypes => this.errorHandler.log(`get person priority type for personId: ` + personId)),
            catchError(this.errorHandler.handleError<any>('getPersonPriorityTypes'))
        );
    }
}
