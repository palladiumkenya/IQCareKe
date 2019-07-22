import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, of} from 'rxjs';

import {catchError, tap} from 'rxjs/operators';
import {Enrollment} from '../_models/enrollment';
import {Person} from '../_models/person';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EnrollmentService {
    private API_URL = environment.API_URL;
    private _url = '/api/Register/enrollment';

    constructor(private http: HttpClient,
                private errorHandler: ErrorHandlerService) { }

    public enrollClient(clientEnrollment: Enrollment): Observable<Enrollment> {
        const Indata = {
            'ClientEnrollment': clientEnrollment
        };

        return this.http.post(this.API_URL + this._url, JSON.stringify(Indata), httpOptions).pipe(
            tap((enrolledClient: Enrollment) => this.errorHandler.log(`enrolled client w/ id`)),
            catchError(this.errorHandler.handleError<Enrollment>('clientEnrollment'))
        );
    }



    
    public reenrollPatient(patientId: number, enrollmentdate: string, userid: number, 
        serviceareaid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientId,
            'EnrollmentDate': enrollmentdate,
            'UserId': userid,
            'ServiceAreaId' : serviceareaid
        };


        return this.http.post<any>(this.API_URL + '/api/Register/AddPatientReenrollment',  JSON.stringify(Indata), httpOptions)
        .pipe(
            tap((reenrolledClient => this.errorHandler.log(`reenrolled client w/ id`)),
            catchError(this.errorHandler.handleError<any[]>('clientreEnrollment'))
        ));
    }
}
