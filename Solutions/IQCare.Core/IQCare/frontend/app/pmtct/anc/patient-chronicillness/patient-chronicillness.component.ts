import { LookupItemView } from '../../../shared/_models/LookupItemView'
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import * as moment from 'moment';
import { DataService } from '../../../shared/_services/data.service';

@Component({
    selector: 'app-patient-chronicillness',
    templateUrl: './patient-chronicillness.component.html',
    styleUrls: ['./patient-chronicillness.component.css']
})
export class PatientChronicillnessComponent implements OnInit {
    public PatientChronicillnessForm: FormGroup;
    public chronicIllnessOptions: any[] = [];
    isEdit: boolean;
    public maxDate: Date;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    constructor(private _formBuilder: FormBuilder, 
        private  dataService: DataService,private dialogRef: MatDialogRef<PatientChronicillnessComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any
       ) {

        this.isEdit = data.isEdit;
        this.chronicIllnessOptions = data.chronicIllnessOptions;
        this.dataService.visitDate.subscribe(date => {

            this.maxDate = date;
          
        });
        this.maxDate = data.maxDate;
    }

    ngOnInit() {


        this.PatientChronicillnessForm = this._formBuilder.group({

            illness: ['', (this.isEdit) ? [] : Validators.required],
            onSetDate: ['', (this.isEdit) ? [] : Validators.required],
            currentTreatment: ['', (this.isEdit) ? [] : Validators.required] // ,
            // dose: ['', Validators.required]
        });
    }



    save() {
        if (this.PatientChronicillnessForm.valid) {
            this.dialogRef.close(this.PatientChronicillnessForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }

}
