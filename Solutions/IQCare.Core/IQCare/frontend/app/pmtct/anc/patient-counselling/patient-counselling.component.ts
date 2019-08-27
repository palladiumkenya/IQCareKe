import { LookupItemView } from '../../../shared/_models/LookupItemView'
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import * as moment from 'moment';

@Component({
    selector: 'app-patient-counselling',
    templateUrl: './patient-counselling.component.html',
    styleUrls: ['./patient-counselling.component.css']
})
export class PatientCounsellingComponent implements OnInit {
    PatientCounsellingForm: FormGroup;
    public counsellingOptions: any[] = [];
    isEdit: boolean = false;
    public maxDate: Date;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    visitDate: Date;
    
    constructor(private _formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<PatientCounsellingComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
        this.isEdit = data.isEdit;
        this.counsellingOptions = data.counsellingOptions;
        this.maxDate = data.maxDate;
       

    }

    ngOnInit() {
        this.PatientCounsellingForm = this._formBuilder.group({
            counsellingDate: ['', (this.isEdit) ? [] : Validators.required],
            counselledOn: ['', (this.isEdit) ? [] : Validators.required]
        });

        if (this.isEdit) {

            this.PatientCounsellingForm.get('counsellingDate').clearValidators();
            this.PatientCounsellingForm.get('counselledOn').clearValidators();
        }


    }



    save() {
        if (this.PatientCounsellingForm.valid) {
            this.dialogRef.close(this.PatientCounsellingForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }
}
