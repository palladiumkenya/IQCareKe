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
import { LabOrderTestResultsComponent } from '../lab-order-test-results/lab-order-test-results.component';

@Component({
  selector: 'app-complete-lab-order',
  templateUrl: './complete-lab-order.component.html',
  styleUrls: ['./complete-lab-order.component.css']
})
export class CompleteLabOrderComponent implements OnInit {
  
   patientId : number;
   personId : string;
  
  constructor(private route: ActivatedRoute)
   {
        this.route.params.subscribe(params=>
          {
            this.patientId = params['patientId'];
            this.personId = params['personId'];
            localStorage.setItem('partnerId', this.personId);
          });      
   }

  ngOnInit() {
   
  }

}
