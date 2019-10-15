import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';

@Component({
    selector: 'app-testdialog',
    templateUrl: './testdialog.component.html',
    styleUrls: ['./testdialog.component.css']
})
export class TestDialogComponent implements OnInit {
    form: FormGroup;
    kitName: string;
    lotNumber: string;
    expiryDate: string;
    hivResult: number;
    title: string;

    hivTestKits: any[];
    hivResultsOptions: any[];

    screeningHIVTestKits: LookupItemView[];
    syphilisResults: LookupItemView[];

    otherLotNumber: string;
    determineLotNumber: string;
    firstResponseLotNumber: string;
    duoKitLotNumber: string;

    duoKitexpiryDate: Date;
    otherKitexpiryDate: Date;
    determineKitexpiryDate: Date;
    firstResponseKitexpiryDate: Date;
    htsEncounterDate: Date;
    clientGender: string;

    minDate: any;

    constructor(private fb: FormBuilder,
        private dialogRef: MatDialogRef<TestDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.title = data.screeningType;
        this.hivTestKits = data.hivTestKits;
        this.hivResultsOptions = data.hivResultsOptions;

        // Last used kits
        this.otherLotNumber = data.otherLotNumber;
        this.determineLotNumber = data.determineLotNumber;
        this.firstResponseLotNumber = data.firstResponseLotNumber;
        this.duoKitLotNumber = data.duoKitLotNumber;

        // Expiry Dates
        this.otherKitexpiryDate = data.otherKitexpiryDate;
        this.determineKitexpiryDate = data.determineKitexpiryDate;
        this.firstResponseKitexpiryDate = data.firstResponseKitexpiryDate;
        this.duoKitexpiryDate = data.duoKitexpiryDate;

        this.htsEncounterDate = data.htsEncounterDate;
        this.clientGender = data.clientGender;

        this.screeningHIVTestKits = data.screeningHIVTestKits;
        this.syphilisResults = data.syphilisResults;
        if (data.screeningType == 'Screening Test') {
            this.hivTestKits = this.screeningHIVTestKits;
        }
        this.minDate = new Date();
    }

    async ngOnInit() {
        this.form = this.fb.group({
            kitName: new FormControl(this.kitName, [Validators.required]),
            lotNumber: new FormControl(this.lotNumber, [Validators.required]),
            expiryDate: new FormControl(this.expiryDate, [Validators.required]),
            hivResult: new FormControl(this.hivResult, [Validators.required]),
            syphilis: new FormControl('', [Validators.required])
        });
        this.form.get('syphilis').disable({onlySelf: true});
    }

    onTestKitChange(event) {        
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Other') {
            this.form.get('lotNumber').setValue(this.otherLotNumber);
            this.form.get('expiryDate').setValue(this.otherKitexpiryDate);
            this.form.get('syphilis').disable({onlySelf: true});
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Determine') {
            this.form.get('lotNumber').setValue(this.determineLotNumber);
            this.form.get('expiryDate').setValue(this.determineKitexpiryDate);
            this.form.get('syphilis').disable({onlySelf: true});
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'First Response') {
            this.form.get('lotNumber').setValue(this.firstResponseLotNumber);
            this.form.get('expiryDate').setValue(this.firstResponseKitexpiryDate);
            this.form.get('syphilis').disable({onlySelf: true});
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'HIV/Syphilis Duo') {
            if (this.clientGender != 'Female') {
                this.snotifyService.info('HIV/Syphilis Duo should be used for female clients only', 
                    'Testing', this.notificationService.getConfig());
                this.form.get('kitName').setValue('');
                return;
            }
            this.form.get('syphilis').enable({onlySelf: true});
            this.form.get('lotNumber').setValue(this.duoKitLotNumber);
            this.form.get('expiryDate').setValue(this.duoKitexpiryDate);
        }
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

}
