import { VisitDetails } from './../_models/visitDetails';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { PatientProfile } from '../_models/patientProfile';
import { PatientPregnancy } from '../_models/PatientPregnancy';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class VisitDetailsService {
    private API_URL = environment.API_URL;
    private _url = '/api/VisitDetails/';
    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public savePatientDetails(visitDetails: VisitDetails): Observable<any> {
        return this.http.post<any>(this.API_URL + '' + this._url, JSON.stringify(visitDetails), httpOptions).pipe(
            tap(savePatientDetails => this.errorHandler.log('Error posting visitDetails')),
            catchError(this.errorHandler.handleError<any>('VisitDetailsController'))
        );
    }

    public getAncInitialProfile(patientId: number, pregnancyId): Observable<PatientProfile> {
        return this.http.get<PatientProfile>(this.API_URL + '' + this._url + 'GetAncProfile/' + patientId + '/' + pregnancyId).pipe(
            tap(getAncInitialProfile => this.errorHandler.log('Error in fetching anc visit details')),
            catchError(this.errorHandler.handleError<PatientProfile>('getANCInitialProfile'))
        );
    }

    public getPregnancyProfile(patientId: number): Observable<PatientPregnancy> {
        return this.http.get<PatientPregnancy>(this.API_URL + '' + this._url + 'GetPregnancyProfile/' + patientId).pipe(
            tap(getPregnancyProfile => this.errorHandler.log('Error in fetching pregnancy Profile')),
            catchError(this.errorHandler.handleError<PatientPregnancy>('getPregnancyProfile'))
        );
    }

    public getConsentOptions(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/optionsByGroupandItemName/YesNo/Yes').pipe(
            tap(getConsentOptions => this.errorHandler.log('get consent options')),
            catchError(this.errorHandler.handleError<PatientPregnancy>('getConsentOptions'))
        );
    }

    public getTestEntryPointANC(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/optionsByGroupandItemName/HTSEntryPoints/ANC').pipe(
            tap(getTestEntryPointANC => this.errorHandler.log('get anc entrypoint options')),
            catchError(this.errorHandler.handleError<PatientPregnancy>('getTestEntryPointANC'))
        );
    }
}
