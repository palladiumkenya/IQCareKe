import { Component, OnInit, ViewChild } from '@angular/core';
import { LaborderService } from '../../_services/laborder.service';
import { MatPaginator, MatTableDataSource, MatDialogConfig, MatDialog, MatDialogRef } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { $ } from 'protractor';
import { AddLabResultComponent } from '../add-lab-result/add-lab-result.component';
import { FormControlBase } from '../../../shared/_models/dynamic-form/FormControlBase';
import { TextboxFormControl, NumericTextboxFormControl, CheckboxFormControl } from '../../../shared/_models/dynamic-form/TextBoxFormControl';
import { ResultDataType } from '../../_models/CompleteLabOrderCommand';
import { SelectlistFormControl } from '../../../shared/_models/dynamic-form/SelectListFormControl';
import { FormControlService } from '../../../shared/_services/form-control.service';
import { FormGroup } from '@angular/forms';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-complete-lab-order',
  templateUrl: './complete-lab-order.component.html',
  styleUrls: ['./complete-lab-order.component.css']
})
export class CompleteLabOrderComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;  
  
  labTestResults : any[] = [];
  completedLabTests : any[] = [];
  pendingLabTests : any[] = [];
  labTestParameters : any[] = [];
  formControlCollection : FormControlBase<any>[] = [];

  pending_labs_displaycolumns : any[] = ['test','orderReason','orderDate','status','action'];
  completed_labs_displaycolumns : any[] = ['test','orderReason','orderDate','result','unit'];

  completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
  pendingLabsDataSource = new MatTableDataSource(this.pendingLabsDataSource);
  
  ResultDataType: ResultDataType;
  patientId : number;

  labResultsFormGroup : FormGroup;

  constructor(private labOrderService : LaborderService,
     private route: ActivatedRoute,
     private dialog: MatDialog)
   {
        this.ResultDataType = new ResultDataType();
   }

  ngOnInit() {
    this.route.params.subscribe(params=>
      {
        this.patientId = params['patientId'];
        this.buildLabTestsGrid(this.patientId);
      });
  }


  public buildLabTestsGrid(patientId: number) {
      this.labOrderService.getLabTestResults(patientId,null).subscribe(res=>{
            if(res.length == 0)
                return;
               res.forEach(test => {               
                this.labTestResults.push({
                  labOrderTestId : test.labOrderTestId,
                  test : test.labTestName,
                  orderDate : test.orderDate,
                  orderReason : test.orderReason,
                  labTestId : test.labTestId,
                  unit : test.resultUnits,
                  resultDate : test.resultDate,
                  result : test.resultTexts,
                  status : test.resultStatus
                });                
               });
                   
             console.log(this.labTestResults.length + '>> Length');

        for (let index = 0; index < this.labTestResults.length; index++) 
        {
          console.log(this.labTestResults[index].status + '>> Status');

            if(this.labTestResults[index].status =='Completed')
               this.completedLabTests.push(this.labTestResults[index]);
            else
               this.pendingLabTests.push(this.labTestResults[index]);
        }
        
        console.log("Pending labs "+ this.pendingLabTests.length);
        this.completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
        this.pendingLabsDataSource = new MatTableDataSource(this.pendingLabTests);

        this.completedLabsDataSource.paginator = this.paginator;
        this.pendingLabsDataSource.paginator = this.paginator;

      },(error)=>
      {
          console.log(error + "An error occured while getting completed labs");
      });  
  }
 
  formControl : FormControlBase<any>[] = [];

  public addResult(pendingTest : any) {

    this.formControlCollection = [];
    this.formControl = [];
    this.labTestParameters = [];

     this.labOrderService.getLabTestParameters(pendingTest.labTestId).subscribe(result =>{
      if(result.length == 0)
         return;
         result.forEach(param => 
          {
              this.formControl.push(this.getFormContolFromParam(param));

              this.formControl.push(
              new TextboxFormControl({
                key:'paramName_' + param.id,
                label: 'Parameter Name',
                value: param.parameterName,
                required: false,
                order: 1,
                disabled: true
              }));

              this.formControl.push(new TextboxFormControl({
                key: param.unitName +'_'+param.id ,
                label: 'Result Unit',
                value: param.unitName,
                required: false,
                order: 3,
                disabled : true
              }));
              
              this.formControl.push(new CheckboxFormControl({
                 key :'undetectable_'+param.id,
                 label : 'Undetectable',
                 value :false,
                 required : false,
                 order : 4
              }));

              this.formControl.push(new TextboxFormControl({
                key:'detection_' + param.id,
                label: 'Detection Limit',
                value: 0,
                required: false,
                order: 5,
                disabled : false
              }));

              this.labTestParameters.push({
                Id : param.id,
                ParamName : param.parameterName,
                LabTestId : param.labTestId,
                DataType : param.dataType,   
                UnitId : param.unitId,
                unitName : param.unitName,
                formControls : this.formControl.sort((a,b)=> a.order - b.order)    
              });
              
              this.formControlCollection = this.formControlCollection.concat(this.formControl);
              this.formControl = [];
         });    
          const dialogConfig = new MatDialogConfig();

          dialogConfig.disableClose = true;
          dialogConfig.autoFocus = true;
          
          dialogConfig.data =  {
                                 labTestParameters : this.labTestParameters,
                                 formControlCollection : this.formControlCollection
                               };
        
          const dialogRef = this.dialog.open(AddLabResultComponent, dialogConfig);
          dialogRef.afterClosed().subscribe(
            data => 
            {
              if (!data)
                return;
                console.log(data);
            });
          });

     
  }

  private getFormContolFromParam(parameter : any) : FormControlBase<any>
  {
      switch (parameter.dataType) {
        case this.ResultDataType.Text:  
            var type = parameter.dataType == this.ResultDataType.Text ? 'text' : 'number';
            return new TextboxFormControl(
              {
                key:'resultText_' + parameter.id,
                label: 'Result Text',
                value: ' ',
                required: true,
                order: 2
              }); 
          case this.ResultDataType.Select:   
             return new SelectlistFormControl(
             {
              key:'select_' + parameter.id,
              label: 'Select Result',
              options: parameter.resultOptions,
              order: 2
             });
             case this.ResultDataType.Numeric:  
            return new TextboxFormControl(
              {
                key:'resultText_' + parameter.id,
                label: 'Result Text',
                value: ' ',
                required: true,
                order: 2
              }); 
        default:
          break;
      }
  }
}
