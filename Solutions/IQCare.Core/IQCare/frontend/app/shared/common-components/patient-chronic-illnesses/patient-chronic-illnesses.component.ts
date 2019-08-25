import { LookupItemService } from './../../_services/lookup-item.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../_models/LookupItemView';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-patient-chronic-illnesses',
    templateUrl: './patient-chronic-illnesses.component.html',
    styleUrls: ['./patient-chronic-illnesses.component.css'],
    providers: [LookupItemService]
})
export class PatientChronicIllnessesComponent implements OnInit {
    ChronicIllnessesForm: FormGroup;
    maxDate: Date;

    illnessesOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private lookupItemService: LookupItemService,
        private dialogRef: MatDialogRef<PatientChronicIllnessesComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.ChronicIllnessesForm = this._formBuilder.group({
            illness: new FormControl('', [Validators.required]),
            onsetDate: new FormControl('', [Validators.required]),
            currentTreatment: new FormControl('', [Validators.required]),
        });

        this.loadChronicIllnessFormOptions();
    }

    loadChronicIllnessFormOptions() {
        this.lookupItemService.getByGroupName('ChronicIllness').subscribe(
            (res) => {
                this.illnessesOptions = res['lookupItems'];
            },
            (error) => {
                console.log(error);
            }
        );
    }

    save() {
        if (this.ChronicIllnessesForm.valid) {
            this.dialogRef.close(this.ChronicIllnessesForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }
}
