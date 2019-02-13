import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { AddLabOrderCommand } from '../_models/AddLabOrderCommand';
import { Observable } from 'rxjs';
import { CompleteLabOrderCommand, ResultDataType } from '../_models/CompleteLabOrderCommand';
import { BehaviorSubject } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { FormControlService } from '../../shared/_services/form-control.service';
import { FormControlBase } from '../../shared/_models/dynamic-form/FormControlBase';
import { TextboxFormControl, NumericTextboxFormControl } from '../../shared/_models/dynamic-form/TextBoxFormControl';
import { SelectlistFormControl } from '../../shared/_models/dynamic-form/SelectListFormControl';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class LaborderService {
   private LabOrder_ApiUrl = environment.API_LAB_URL;

   private labResultFormGroupSubject = new BehaviorSubject<FormControlBase<any>[]>([]);
   labTestResultForm = this.labResultFormGroupSubject.asObservable();

    constructor(private httpClient: HttpClient,
        private errorHandlerService: ErrorHandlerService) 
        {

        }



 public updateResultsForm(formControls : FormControlBase<any>[]) {
   this.labResultFormGroupSubject.next(formControls);
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
    this.LabOrder_ApiUrl + '/api/LabOrder/GetLabTestResults?patientId=' + patientId + '&status=' + status;

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


    public getLabOrderTestResults(labOrderTestId : number) : Observable<any> {
      return this.httpClient.get<any>(this.LabOrder_ApiUrl + '/api/LabTests/GetTestResultsByLabOrderTestId/' + labOrderTestId)
      .pipe(tap(param=> this.errorHandlerService.log('get lab order test results')),
      catchError(this.errorHandlerService.handleError<any[]>('getLabOrderTestResults')))      
    }

     resultDataType = new ResultDataType();

    public getFormContolFromParam(parameter : any) : FormControlBase<any>
     {
        switch (parameter.dataType) {
          case this.resultDataType.Text:  
              return new TextboxFormControl(
                {
                  key:'ResultText_' + parameter.id,
                  label: 'Result Text',
                  required: true,
                  value:null,
                  order: 2
                }); 
            case this.resultDataType.Select:   
              return new SelectlistFormControl(
              {
                key:'ResultOptionId_' + parameter.id,
                label: 'Select Result',
                options: parameter.resultOptions,
                order: 2
              });
              case this.resultDataType.Numeric:  
              return new TextboxFormControl(
                {
                  key:'ResultValue_' + parameter.id,
                  label: 'Result Text',
                  required: true,
                  order: 2,
                  value : null,
                  pattern : "^\\d*\\.?\\d+$"
                }); 
          default:
            break;
    }
}
}
