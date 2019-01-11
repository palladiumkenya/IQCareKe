import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { LaborderService } from '../../_services/laborder.service';
import { CompleteLabOrderCommand, AddLabTestResultCommand, ResultDataType } from '../../_models/CompleteLabOrderCommand';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControlBase } from '../../../shared/_models/dynamic-form/FormControlBase';
import { FormControlService } from '../../../shared/_services/form-control.service';

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
  formControlCollection : FormControlBase<any>[] = [];

  labResultForm : FormGroup;
  @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

  constructor(private labOrderService: LaborderService,
    private  snotifyService : SnotifyService,
    private notificationService: NotificationService,
    private dialogRef: MatDialogRef<AddLabResultComponent>,
    private formControlService : FormControlService,
    @Inject(MAT_DIALOG_DATA) public data : any) 
    {   
       this.labTestParameters = data.labTestParameters;
       this.formControlCollection = data.formControlCollection;
       this.labResultForm = this.formControlService.toFormGroup(data.formControlCollection);
       this.resultDataTypes = new ResultDataType();
       this.userId = JSON.parse(localStorage.getItem('appUserId'));
       this.notify.emit(this.labResultForm);
       this.dialogTitle = 'Submit Lab Test Results';
   }

  ngOnInit() 
  {
    
  }

   labTestResults : AddLabTestResultCommand [] = [];


  get isFormValid(){
    return !this.labResultForm.invalid;
  }

   public submitLabResult() {
    if(this.labResultForm.invalid)
         return;
        console.log(JSON.stringify(this.labResultForm.value));
        
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
   save() {
    if (!this.labResultForm.valid) 
          return;
    this.dialogRef.close(this.labResultForm.value);   
}

close() {
    this.dialogRef.close();
}

}
