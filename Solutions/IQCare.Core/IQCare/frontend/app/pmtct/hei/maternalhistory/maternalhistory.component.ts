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

        const { motherstateOptions, motherreceivedrugsOptions, heimotherregimenOptions } = this.maternalhistoryOptions[0];
        this.motherstateOptions = motherstateOptions;
        this.motherreceivedrugsOptions = motherreceivedrugsOptions;
        this.heimotherregimenOptions = heimotherregimenOptions;

        console.log(this.heimotherregimenOptions);

        this.notify.emit(this.MaternalHistoryForm);
    }
}
