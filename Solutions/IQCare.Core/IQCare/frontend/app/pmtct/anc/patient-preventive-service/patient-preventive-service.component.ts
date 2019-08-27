import { LookupItemView } from '../../../shared/_models/LookupItemView'
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { Component, OnInit, Inject, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DataService } from '../../../shared/_services/data.service';
import * as moment from 'moment';


@Component({
    selector: 'app-patient-preventive-service',
    templateUrl: './patient-preventive-service.component.html',
    styleUrls: ['./patient-preventive-service.component.css']
})
export class PatientPreventiveServiceComponent implements OnInit {

    public PatientPreventiveServicesForm: FormGroup;
    preventiveServicesOptions: any[] = [];
    isEdit: boolean;
    public minDate: Date;
    public maxDate:Date;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    constructor(private _formBuilder: FormBuilder,
         private dataService: DataService, 
         private dialogRef: MatDialogRef<PatientPreventiveServiceComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) {

        this.preventiveServicesOptions = data.preventiveServicesOptions;
        this.isEdit = data.isEdit;
        
    }

    ngOnInit() {


        this.dataService.visitDate.subscribe(date => {
            this.minDate = date;
            this.maxDate = date;
        });

        this.PatientPreventiveServicesForm = this._formBuilder.group({
            preventiveServices: ['', (this.isEdit) ? [] : Validators.required],
            dateGiven: ['', (this.isEdit) ? [] : Validators.required],
            comments: ['', []],
            nextSchedule: ['', []]
        });




        if (this.isEdit) {
            // this.getPatientPreventiveServiceInfo(this.patientId, this.patientMasterVisitId);

            this.PatientPreventiveServicesForm.get('preventiveServices').clearValidators();
            this.PatientPreventiveServicesForm.get('dateGiven').clearValidators();
            this.PatientPreventiveServicesForm.get('comments').clearValidators();
            this.PatientPreventiveServicesForm.get('nextSchedule').clearValidators();

        }
    }


    save() {
        if (this.PatientPreventiveServicesForm.valid) {
            this.dialogRef.close(this.PatientPreventiveServicesForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }




}
