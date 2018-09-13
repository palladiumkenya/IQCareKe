import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-maternalhistory',
    templateUrl: './maternalhistory.component.html',
    styleUrls: ['./maternalhistory.component.css']
})
export class MaternalhistoryComponent implements OnInit {
    MaternalHistoryForm: FormGroup;
    motherstateOptions: any[] = [];
    motherreceivedrugsOptions: any[] = [];
    heimotherregimenOptions: any[] = [];
    yesnoOptions: any[] = [];
    motherdrugsatinfantenrollmentOptions: any[] = [];

    @Input('maternalhistoryOptions') maternalhistoryOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService) { }

    ngOnInit() {
        this.MaternalHistoryForm = this._formBuilder.group({
            motherregisteredinclinic: new FormControl('', [Validators.required]),
            stateofmother: new FormControl('', [Validators.required]),
            nameofmother: new FormControl('', [Validators.required]),
            cccno: new FormControl('', [Validators.required]),
            pmtctheimotherreceivedrugs: new FormControl('', [Validators.required]),
            pmtctheimotherregimen: new FormControl('', [Validators.required]),
            otherspecify: new FormControl('', [Validators.required]),
            motheronartatinfantenrollment: new FormControl('', [Validators.required]),
            pmtctheimotherdrugsatinfantenrollment: new FormControl('', [Validators.required]),
        });

        const {
            motherstateOptions,
            motherreceivedrugsOptions,
            heimotherregimenOptions,
            yesnoOptions,
            motherdrugsatinfantenrollmentOptions } = this.maternalhistoryOptions[0];
        this.motherstateOptions = motherstateOptions;
        this.motherreceivedrugsOptions = motherreceivedrugsOptions;
        this.heimotherregimenOptions = heimotherregimenOptions;
        this.yesnoOptions = yesnoOptions;
        this.motherdrugsatinfantenrollmentOptions = motherdrugsatinfantenrollmentOptions;

        this.notify.emit(this.MaternalHistoryForm);
    }

    onMotherReceivedDrugsChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Other') {
            this.MaternalHistoryForm.controls['otherspecify'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.MaternalHistoryForm.controls['otherspecify'].disable({ onlySelf: true });
        }

        if (event.isUserInput && event.source.selected && event.source.viewValue == 'HAART') {
            this.MaternalHistoryForm.controls['pmtctheimotherregimen'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.MaternalHistoryForm.controls['pmtctheimotherregimen'].disable();
        }
    }

    onMotherOnArtAtInfantEnrollmentChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.MaternalHistoryForm.controls['pmtctheimotherdrugsatinfantenrollment'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.MaternalHistoryForm.controls['pmtctheimotherdrugsatinfantenrollment'].disable();
        }
    }
}
