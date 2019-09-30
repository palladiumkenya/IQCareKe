import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import {Observable} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import {PersonView} from '../../records/_models/personView';

@Injectable({
  providedIn: 'root'
})
export class FacilityService {
    private API_URL = environment.API_URL;
    
    constructor(private http: HttpClient,
                private errorHandler: ErrorHandlerService) { }
                
    public getAppointmentStatistics(summaryDate: string): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Facility/GetAppointmentStatistics/' + summaryDate).pipe(
            tap(getAppointmentStatistics => this.errorHandler.log('get appointment summary date')),
            catchError(this.errorHandler.handleError<any>('error fetching Appointment Statistics'))
        );
    }
    
    public getTotalCCCVisits(summaryDate: string): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Facility/GetAllCCCVisitCount/' + summaryDate).pipe(
            tap(getTotalCCCVisits => this.errorHandler.log('get total ccc visits')),
            catchError(this.errorHandler.handleError<any>('error fetching ccc visits'))
        );
    }
    
    public getILStatistics(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Facility/GetILStatistics').pipe(
            tap(getILStatistics => this.errorHandler.log('get IL statistics')),
            catchError(this.errorHandler.handleError<any>('error fetching IL statistics'))
        );
    }

    getCareEndingSummary(): Observable<any> {
        return this.http.get(this.API_URL + '/api/Facility/GetFacilityCareEndingSummary').pipe(
            tap(getCareEndingSummary => this.errorHandler.log('successfully fetched care ending summary')),
            catchError(this.errorHandler.handleError<any>('error fetching care ending summary'))
        );
    }
}
