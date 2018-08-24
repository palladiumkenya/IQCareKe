import { Validators } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NextOfKin } from '../../_models/nextofkin';

@Component({
    selector: 'app-personcontacts',
    templateUrl: './personcontacts.component.html',
    styleUrls: ['./personcontacts.component.css']
})
export class PersoncontactsComponent implements OnInit {
    title: string;
    form: FormGroup;
    gender: any;
    relationship: any;
    consentSms: any;
    contactCategory: any;
    nextOfKin: NextOfKin;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<PersoncontactsComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = 'Next of Kin & Emergency Contacts';

        this.nextOfKin = new NextOfKin();

        this.gender = data.gender;
        this.relationship = data.relationship;
        this.consentSms = data.consentSms;
        this.contactCategory = data.contactCategory;
    }

    ngOnInit() {
        this.form = this.fb.group({
            firstName: new FormControl(this.nextOfKin.firstName, [Validators.required]),
            middleName: new FormControl(this.nextOfKin.middleName),
            lastName: new FormControl(this.nextOfKin.lastName, [Validators.required]),
            sex: new FormControl(this.nextOfKin.sex, [Validators.required]),
            kinContactRelationship: new FormControl(this.nextOfKin.kinContactRelationship, [Validators.required]),
            kinMobileNumber: new FormControl(this.nextOfKin.kinMobileNumber),
            kinConsentToSMS: new FormControl(this.nextOfKin.kinConsentToSMS, [Validators.required]),
            consentDeclineReason: new FormControl(this.nextOfKin.consentDeclineReason, [Validators.required]),
            kinContactType: new FormControl(this.nextOfKin.kinContactType, [Validators.required])
        });

        this.form.controls.consentDeclineReason.disable({ onlySelf: true });
    }

    save() {
        if (this.form.valid) {
            this.dialogRef.close(this.form.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }

    onConsentChange() {
        if (this.form.controls.kinConsentToSMS.value.itemName != 'Granted') {
            this.form.controls.consentDeclineReason.enable({ onlySelf: false });
        } else {
            this.form.controls.consentDeclineReason.disable({ onlySelf: true });
        }
    }
}
