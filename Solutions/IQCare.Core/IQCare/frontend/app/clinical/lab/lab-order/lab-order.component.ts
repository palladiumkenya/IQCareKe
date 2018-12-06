import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-lab-order',
  templateUrl: './lab-order.component.html',
  styleUrls: ['./lab-order.component.css']
})
export class LabOrderComponent implements OnInit {

labOrderFormGroup : FormGroup;
labTestData : any[];
@Input() labTestReasons: any[] = [];
@Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
@Output() notifyData: EventEmitter<any[]> = new EventEmitter<any[]>();

labTestReasonOptions : any[];

  constructor(private formBuilder :FormBuilder) {

     this.labOrderFormGroup = this.formBuilder.group({
      labTestId: new FormControl('', [Validators.required]),
      labtestReasonId: new FormControl('', [Validators.required]),
      labTestNotes: new FormControl('', [Validators.required]),
      orderDate: new FormControl('', [Validators.required]),
      clinicalOrderNotes: new FormControl('', [Validators.required])
    });
    this.notify.emit(this.labOrderFormGroup);
    this.notifyData.emit(this.labTestData);
   }

  ngOnInit() {
    this.labTestReasonOptions = this.labTestReasons;
  }


  
  public  AddLabTest() {
    this.labTestData.push({
      testId: this.labOrderFormGroup.get('labTestId').value.id,
      test: this.labOrderFormGroup.get('labTestId').value.itemName,
      orderReason: this.labOrderFormGroup.get('labtestReasonId').value.itemName,
      orderReasonId: this.labOrderFormGroup.get('labtestReasonId').value.itemId,
      testNotes: this.labOrderFormGroup.get('labTestNotes').value
    });
  }

}
