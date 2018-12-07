import { Injectable } from '@angular/core';
import {SnotifyService} from 'ng-snotify';
import {environment} from '../../../environments/environment';
import {ErrorHandlerService} from './errorhandler.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/index';
import {catchError, tap} from 'rxjs/operators';
import {PatientMasterVisitEncounter} from '../../pmtct/_models/PatientMasterVisitEncounter';
import { AddPatientOrdVisitCommand } from '../_models/patientordvisit';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class EncounterService {
    private API_URL = environment.API_URL;

  constructor(private snotifyService: SnotifyService,
              private http: HttpClient,
              private errorHandler: ErrorHandlerService) { }

    public savePatientMasterVisit(patientMasterVisitEncounter: PatientMasterVisitEncounter): Observable<any> {
        return this.http.post<PatientMasterVisitEncounter>(this.API_URL + '/api/PatientMasterVisit',
            JSON.stringify(patientMasterVisitEncounter), httpOptions).pipe(
            tap(saveMaternityMasterVisit => this.errorHandler.log(`successfully added  patientmastervisit`)),
            catchError(this.errorHandler.handleError<any>('Error saving  patientmastervisit'))
        );
    }

    public savePatientOrdVisit(ordVisit: AddPatientOrdVisitCommand): Observable<any> {
        return this.http.post<AddPatientOrdVisitCommand>(this.API_URL + '/api/PatientMasterVisit/addOrdVisit',
            JSON.stringify(ordVisit), httpOptions).pipe(
            tap(savePatientOrdVisit => this.errorHandler.log(`successfully added  patient ord visit`)),
            catchError(this.errorHandler.handleError<any>('Error saving  patientmastervisit'))
        );
    }
}


