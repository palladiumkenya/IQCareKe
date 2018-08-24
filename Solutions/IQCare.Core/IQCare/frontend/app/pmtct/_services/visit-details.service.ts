import { VisitDetails } from './../_models/visitDetails';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {environment} from '../../../environments/environment';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import {PatientProfile} from '../_models/patientProfile';
import {PatientPregnancy} from '../_models/PatientPregnancy';

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

    public savePatientDetails(visitDetails: VisitDetails): Observable<VisitDetails> {
        return this.http.post<any>(this.API_URL + '' + this._url, JSON.stringify(visitDetails), httpOptions).pipe(
            tap(savePatientDetails => this.errorHandler.log('Error posting visitDetails')),
            catchError(this.errorHandler.handleError<any>('VisitDetailsController', ), )
        );
    }

    public getAncInitialProfile(patientId: number): Observable<PatientProfile> {
      return this.http.get<PatientProfile>(this.API_URL + '' + this._url + 'GetPregnancyProfile/' + patientId).pipe(
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

}
