import { Validators } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig, MatDialog } from '@angular/material';
import { NextOfKin } from '../../_models/nextofkin';
import { InlineSearchComponent } from '../../inline-search/inline-search.component';

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
    yesno: any;
    nextOfKin: NextOfKin;

    contactRegisteredInClinic: boolean = false;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<PersoncontactsComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private dialog: MatDialog) {
        this.title = 'Next of Kin & Emergency Contacts';

        this.nextOfKin = new NextOfKin();

        this.gender = data.gender;
        this.relationship = data.relationship;
        this.consentSms = data.consentSms;
        this.contactCategory = data.contactCategory;
        this.yesno = data.yesno;
    }

    ngOnInit() {
        this.form = this.fb.group({
            firstName: new FormControl(this.nextOfKin.firstName, [Validators.required]),
            middleName: new FormControl(this.nextOfKin.middleName),
            lastName: new FormControl(this.nextOfKin.lastName, [Validators.required]),
            sex: new FormControl(this.nextOfKin.sex, [Validators.required]),
            isRegisteredInFacility: new FormControl([Validators.required]),
            kinContactRelationship: new FormControl(this.nextOfKin.kinContactRelationship, [Validators.required]),
            kinMobileNumber: new FormControl(this.nextOfKin.kinMobileNumber),
            kinConsentToSMS: new FormControl(this.nextOfKin.kinConsentToSMS, [Validators.required]),
            consentDeclineReason: new FormControl(this.nextOfKin.consentDeclineReason, [Validators.required]),
            kinContactType: new FormControl(this.nextOfKin.kinContactType, [Validators.required]),
            registeredPersonId: new FormControl(0)
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

    onContactRegisteredInClinicChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.contactRegisteredInClinic = true;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.contactRegisteredInClinic = false;
        }
    }

    openDialog() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '85%';
        dialogConfig.width = '80%';

        dialogConfig.data = {
        };


        const dialogRef = this.dialog.open(InlineSearchComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                console.log(data);

                this.form.controls.firstName.setValue(data[0]['firstName']);
                this.form.controls.middleName.setValue(data[0]['middleName']);
                this.form.controls.lastName.setValue(data[0]['lastName']);


                const sexOption = this.gender.filter(obj => obj.itemId == data[0]['sex']);
                this.form.controls.sex.setValue(sexOption[0]);
                this.form.controls.registeredPersonId.setValue(data[0]['id']);
            }
        );
    }
}
