import {of , Observable} from 'rxjs';
import {Injectable, Inject} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {catchError, tap} from 'rxjs/operators';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/abstract_emitter';



const httpOptions ={
    headers: new HttpHeaders ({'Content-Type': 'application/json'})
}

@Injectable()
export class FormDetailsService {
    private API_AIR_URL = environment.API_AIR_URL;
    constructor (private http: HttpClient,
        private errorHandler: ErrorHandlerService){
 
        }
        public checkIfPeriodExists(reportingdate: string): Observable<any[]>{
            const Indata = {
                'ReportingDate': reportingdate
            }
            return this.http.post<any>(this.API_AIR_URL + '/api/ReportingForm/PeriodExists', JSON.stringify(Indata), httpOptions)
            .pipe (tap (checkIfPeriodExists => this.errorHandler.log('checked if Period Exists' + reportingdate)),
              catchError(this.errorHandler.handleError<any[]>('checkIfPeriodExists'))
            );
        }


        public getFormDetails(formId: number): Observable<any[]>{
            return this.http.get<any[]>(this.API_AIR_URL + '/api/ReportingForm/GetReportingFormDetails/' + formId).pipe(tap
                (getFormDetails => this.errorHandler.log(`fetched Form Details for` + formId)),
                catchError(this.errorHandler.handleError<any[]>('getFormDetails'))
            );

        }
       public getFormdata(formreportingid: number): Observable<any[]>{
        return this.http.get<any[]>(this.API_AIR_URL + '/api/ReportingForm/GetFormData/' + formreportingid).pipe(tap
            (getFormdata => this.errorHandler.log(`fetched Existing FormData  Details for` + formreportingid)),
            catchError(this.errorHandler.handleError<any[]>('getExisting FormData  Details'))
        );
       }

       public getConfiguredReportingForms() : Observable<any[]>{
        return this.http.get<any[]>(this.API_AIR_URL + '/api/ReportingForm/GetConfiguredReportingForms').pipe(tap
            (getFormdata => this.errorHandler.log(`Get configured reporting forms`)),
            catchError(this.errorHandler.handleError<any[]>('getExisting FormData  Details'))
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
        public EditIndicatorResults(reportingdate: string, reportingformId: number, reportingperiodid: number
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
                   'ReportingPeriodId': reportingperiodid,
                   'ReportingFormId': reportingformId,
                   'CreatedBy': createdby,
                   'IndicatorResults': IR
               };

               console.log('inData..');
               console.log(Indata);
               console.log(JSON.stringify(Indata));

           return this.http.post<any>(this.API_AIR_URL + '/api/Indicator/EditResults', JSON.stringify(Indata), httpOptions).pipe(
               tap((EditIndicatorResults: any) => this.errorHandler.log(`Edited Indicator Results`)),
               catchError(this.errorHandler.handleError<any>('EditIndicatorResults'))
           );
       }
   

       public EditReportSettings(SectionList: any[]):Observable<any>{
        const sections =[];
       for (let i = 0  ; i < SectionList.length; i++)
       {
            sections.push({
                'Id': SectionList[i].Id,
                'Name': SectionList[i].SectionName,
                'Active': SectionList[i].Active
            });
       }

       const Indata={
           'SectionList': sections
       };

       return this.http.post<any>(this.API_AIR_URL + '/api/ReportSection/EditReportSection', JSON.stringify(Indata), httpOptions).pipe(
         tap((EditReportSettings:any)=>this.errorHandler.log(`Edited Section Settings`)),
         catchError(this.errorHandler.handleError<any>('EditSectionSettings'))  
       );
        

       }
    
}