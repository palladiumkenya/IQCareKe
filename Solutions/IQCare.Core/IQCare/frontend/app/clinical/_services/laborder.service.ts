import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { AddLabOrderCommand } from '../_models/AddLabOrderCommand';
import { Observable } from 'rxjs';
import { CompleteLabOrderCommand } from '../_models/CompleteLabOrderCommand';
import { BehaviorSubject } from 'rxjs';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class LaborderService {
   private LabOrder_ApiUrl = environment.API_LAB_URL;

   private labTestParamsMessageSource = new BehaviorSubject([]);
   currentLabTestParams = this.labTestParamsMessageSource.asObservable();

    constructor(private httpClient: HttpClient,
        private errorHandlerService: ErrorHandlerService, ) {
    }



 public updateParams(params :any[]) {
   this.labTestParamsMessageSource.next(params);
 }

  public addLabOrder(addLabOrderCommand : AddLabOrderCommand) : Observable<any> {
    return this.httpClient.post<AddLabOrderCommand>(this.LabOrder_ApiUrl + '/api/LabOrder/AddLabOrder',addLabOrderCommand,httpOptions).pipe(
      tap(lab=> this.errorHandlerService.log('Add Lab Order request')),
      catchError(this.errorHandlerService.handleError<any[]>('addLabOrder'))
    )
  }

  public getLabOrders(patientId: number,status:string) : Observable<any> {
    return this.httpClient.get<any>(this.LabOrder_ApiUrl + '/api/LabOrder/GetLabOrdersByPatientId/' + patientId + '/' +status).pipe(
        tap(getLabOrders => this.errorHandlerService.log('get patient lab orders')),
        catchError(this.errorHandlerService.handleError<any[]>('getLabOrders'))
    ); 
    }

  public completeLabOrder(completeLabOrderCommand : CompleteLabOrderCommand ) : Observable<any> {
    return this.httpClient.post<CompleteLabOrderCommand>(this.LabOrder_ApiUrl + '/api/LabOrder/CompleteLabOrder',completeLabOrderCommand,httpOptions).pipe(
      tap(lab=> this.errorHandlerService.log('Complete lab order  request')),
      catchError(this.errorHandlerService.handleError<any[]>('CompleteLabOrder'))
    )
  }

  public getLabTestResults(patientId: number,status:string) : Observable<any> {
    var url = status == null ? this.LabOrder_ApiUrl + '/api/LabOrder/GetLabTestResults?patientId=' + patientId  : 
    this.LabOrder_ApiUrl + '/api/LabOrder/GetLabTestResults?patientId' + patientId + '&status=' + status;

    return this.httpClient.get<any>(url).pipe(
        tap(getLabTestResults => this.errorHandlerService.log('get lab order test results')),
        catchError(this.errorHandlerService.handleError<any[]>('getLabOrderTestResults'))
    );
    }

    public getConfiguredLabTests() : Observable<any> {
      return this.httpClient.get<any>(this.LabOrder_ApiUrl + '/api/LabTests/GetAllLabTests').pipe(
          tap(getConfiguredLabTests => this.errorHandlerService.log('get configured lab tests')),
          catchError(this.errorHandlerService.handleError<any[]>('getConfiguredLabTests'))
      );
    }

  
    public getLabTestParameters(labTestId : number) : Observable<any> {
      return this.httpClient.get<any>(this.LabOrder_ApiUrl + '/api/LabTests/GetLabTestPametersByLabTestId/' + labTestId)
      .pipe(tap(param=> this.errorHandlerService.log('get Lab Test Parameters')),
      catchError(this.errorHandlerService.handleError<any[]>('getLabTestParameters')))      
    }
}
