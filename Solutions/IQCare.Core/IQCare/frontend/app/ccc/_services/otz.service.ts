import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import {catchError, tap} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {OtzActivityFormCommand} from '../otz/activity-form/activity-form.component';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class OtzService {
    private API_URL = environment.API_URL;
    
    constructor(private http: HttpClient,
                private errorHandler: ErrorHandlerService) {}
                
    public getProviders(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Setup/getUsers').pipe(
            tap(getProviders => this.errorHandler.log('fetched all providers')),
            catchError(this.errorHandler.handleError<any[]>('getProviders', []))
        );
    }

    public getByGroupName(groupName: string): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetbyGroupName/' + groupName).pipe(
            tap(getByGroupName => this.errorHandler.log('get ' + groupName + 'options by Name')),
            catchError(this.errorHandler.handleError<LookupItemView[]>('getbyGroupName', []))
        );
    }
    
    public saveOtzEnrollment(saveCommand: OtzActivityFormCommand): Observable<any> {
        return this.http.post(this.API_URL + '/api/Otz/SaveOtzActivityForm', JSON.stringify(saveCommand), httpOptions).pipe(
            tap(saveOtzEnrollment => this.errorHandler.log('save Otz Enrollment')),
            catchError(this.errorHandler.handleError<LookupItemView[]>('saveOtzEnrollment', []))
        );
    }
}
