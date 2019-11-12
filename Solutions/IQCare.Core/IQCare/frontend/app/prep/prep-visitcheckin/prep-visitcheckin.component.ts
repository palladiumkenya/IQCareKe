import { Component, OnInit, Output, EventEmitter, Inject , NgZone, ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import * as moment from 'moment';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MatStepper } from '@angular/material';
@Component({
    selector: 'app-prep-visitcheckin',
    templateUrl: './prep-visitcheckin.component.html',
    styleUrls: ['./prep-visitcheckin.component.css']
})
export class PrepVisitcheckinComponent implements OnInit {

    EnrollmentDate: Date;
    patientId: number;
    serviceId: number;
    formGroup: FormGroup;
    maxDate: Date;
    get formArray(): AbstractControl | null { return this.formGroup.get('formArray'); }

    @ViewChild('stepper') stepper: MatStepper;
    constructor(
        private personHomeService: PersonHomeService,
        private _formBuilder: FormBuilder

    ) {
        this.maxDate = new Date();

    }

    ngOnInit() {


        this.formGroup = this._formBuilder.group({
            formArray: this._formBuilder.array([
                this._formBuilder.group({
                       PatientCheckinStartTime: new FormControl('',[Validators.required]), 
                
                })
            ]),
        });
    }


    LoadPrepEnrollmentDate(patientId: number, serviceId: number): void {
        this.personHomeService.getPatientEnrollmentDateByServiceAreaId(patientId, serviceId).subscribe(
            (result) => {
                if (result != null) {
                    this.EnrollmentDate = result.enrollmentDate;
                }
            }, (error) => {
                console.log(error);
            });
    }

}
