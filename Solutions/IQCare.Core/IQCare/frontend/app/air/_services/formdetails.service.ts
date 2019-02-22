import {of , Observable} from 'rxjs';
import {Injectable, Inject} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {catchError, tap} from 'rxjs/operators';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';



const httpOptions ={
    headers: new HttpHeaders ({'Content-Type': 'application/json'})
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


        public submitIndicatorResults(reportingdate: string, reportingformId: number
             , createdby: number , indicatorresults: any[]): Observable<any>{
         
            const IR = [];
            if(indicatorresults.length == 0){
                return of([]);
            }
            
             for ( let i = 0; i <  indicatorresults.length; i++){
               IR.push({
                'Id': indicatorresults[i].Id,
                'ResultText': indicatorresults[i].ResultText,
                'ResultNumeric': indicatorresults[i].ResultNumeric
               });  
             }

            
            const Indata = {
                    'ReportingDate': reportingdate,
                    'ReportingFormId': reportingformId,
                    'CreatedBy': createdby,
                    'IndicatorResults': IR
                };

                console.log('inData..');
                console.log(Indata);
                console.log(JSON.stringify(Indata));

            return this.http.post<any>(this.API_AIR_URL + '/api/Indicator/AddResults', JSON.stringify(Indata), httpOptions).pipe(
                tap((submitIndicatorResults: any) => this.errorHandler.log(`Submit Indicator Results`)),
                catchError(this.errorHandler.handleError<any>('submitIndicatorResults'))
            );
        }

    
}