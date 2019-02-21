import {of as observableof,Observable} from 'rxjs';
import {Injectable, Inject} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient,HttpHeaders} from '@angular/common/http';
import {catchError,tap} from 'rxjs/operators';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service'

const httpOptions ={
    headers:new HttpHeaders ({'Content-Type':'application/json'})
}

@Injectable()
export class FormDetailsService {
    private API_AIR_URL = environment.API_AIR_URL;
    constructor (private http: HttpClient,
        private errorHandler: ErrorHandlerService){
 
        }


        public getFormDetails(formId: number): Observable<any[]>{
            return this.http.get<any[]>(this.API_AIR_URL + '/api/ReportingForm/GetReportingFormDetails/' + formId).pipe(tap
                (getFormDetails => this.errorHandler.log(`fetched Form Details for` + formId)),
                catchError(this.errorHandler.handleError<any[]>('getFormDetails'))
            );

        }
}