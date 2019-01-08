import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { LaborderService } from '../../_services/laborder.service';
import { CompleteLabOrderCommand, AddLabTestResultCommand, ResultDataType } from '../../_models/CompleteLabOrderCommand';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-add-lab-result',
  templateUrl: './add-lab-result.component.html',
  styleUrls: ['./add-lab-result.component.css']
})
export class AddLabResultComponent implements OnInit {
  
  resultDataType: string;
  resultUnit : string;
  labTest : string;
  labOrderId : any;
  labTestId : any;
  labOrderTestId : any;
  userId : any;
  isText : boolean;
  isNumeric : boolean;
  isSelect : boolean;
  labTestResultOptions : any[];
  resultDataTypes : ResultDataType;
  labTestParameters : any[] = [];
  dialogTitle : string;

  addLabResultForm : FormGroup;
  @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

  constructor(private formBuilder :FormBuilder, 
    private labOrderService: LaborderService,
    private  snotifyService : SnotifyService,
    private notificationService: NotificationService,
    private dialogRef: MatDialogRef<AddLabResultComponent>,
    @Inject(MAT_DIALOG_DATA) data) 
    {
       
       this.addLabResultForm = this.formBuilder.group({
            resultValue : new FormControl(''),
            resultText :  new FormControl(''),
            resultOption : new FormControl(''),
            resultUnit :   new FormControl(''),
            undetectable :  new FormControl(''),
            detectionLimit : new FormControl(''),
            parameter : new FormControl('')
       });

       this.labTestParameters = data;
       this.resultDataTypes = new ResultDataType();
       this.notify.emit(this.addLabResultForm);
       this.userId = JSON.parse(localStorage.getItem('appUserId'));
       this.labOrderService.currentLabTestParams.subscribe(param => this.labTestParameters = param);
       this.dialogTitle = 'Submit Lab Test Results';
   }

  ngOnInit() 
  {
     this.isNumeric = this.resultDataType == this.resultDataTypes.Numeric;
     this.isText = this.resultDataType == this.resultDataTypes.Text;
     this.isSelect = this.resultDataType == this.resultDataTypes.Select;
  }

   labTestResults : AddLabTestResultCommand [] = [];

   public submitLabResult() {
    if(this.addLabResultForm.invalid)
         return;

      this.labTestResults.push(this.buildLabTestResultModel());
      const completeLabCommand : CompleteLabOrderCommand = {
        LabOrderId : this.labOrderId,
        LabOrderTestId : this.labOrderTestId,
        LabTestId : this.labTestId,
        LabTestResults : this.labTestResults,
        UserId : this.userId
      };

      this.labOrderService.completeLabOrder(completeLabCommand).subscribe(res=>
      {
          this.snotifyService.success("Lab test results submitted sucessfully","Lab",this.notificationService.getConfig());

      },(err)=>
      {
            this.snotifyService.error('An error occured while completing lab order', 'Lab', this.notificationService.getConfig());
            console.log(err,'complete lab order error');
      },()=>{

      })
     
   }

   private buildLabTestResultModel() : AddLabTestResultCommand {
    const labResultCommand :  AddLabTestResultCommand =
        {
        DetectionLimit :this.addLabResultForm.get('detectionLimit').value,
        ParameterId : this.addLabResultForm.get('parameterId').value,
        ResultOption : this.addLabResultForm.get('resultOption').value,
        ResultOptionId : this.addLabResultForm.get('resultOption').value,
        ResultText : this.addLabResultForm.get('resultText').value,
        ResultUnit : this.addLabResultForm.get('resultUnit').value,
        ResultUnitId : this.addLabResultForm.get('resultUnit').value,
        ResultValue :  this.addLabResultForm.get('resultValue').value,
        Undetectable :  this.addLabResultForm.get('undetectable').value,
        }
      return labResultCommand;
   }

   save() {
    if (!this.addLabResultForm.valid) 
          return;
    this.dialogRef.close(this.addLabResultForm.value);   
}

close() {
    this.dialogRef.close();
}

}
