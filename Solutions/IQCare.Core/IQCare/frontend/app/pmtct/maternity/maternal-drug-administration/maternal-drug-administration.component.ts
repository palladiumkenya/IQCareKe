import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';

@Component({
    selector: 'app-maternal-drug-administration',
    templateUrl: './maternal-drug-administration.component.html',
    styleUrls: ['./maternal-drug-administration.component.css']
})
export class MaternalDrugAdministrationComponent implements OnInit {

    maternalDrugAdministrationFormGroup: FormGroup;
    @Input() drugAdministrationOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    public yesnoOptions: any[] = [];
    public finaResultOptions: any[] = [];
    public yesnonaOptions: any[] = [];


    constructor(private formBuilder: FormBuilder,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.maternalDrugAdministrationFormGroup = this.formBuilder.group({
            vitaminASupplement: new FormControl('', [Validators.required]),
            HaartANC: new FormControl('', [Validators.required]),
            ARVStartedMaternity: new FormControl('', [Validators.required]),
            cotrimoxazole: new FormControl('', [Validators.required]),
            infantARVProphylaxis: new FormControl('', [Validators.required])
        });

        const {
            yesNo,
            finalResult,
            yesNoNa
        } = this.drugAdministrationOptions[0];
        this.yesnoOptions = yesNo;
        this.yesnonaOptions = yesNoNa;
        this.finaResultOptions = finalResult;

        this.notify.emit(this.maternalDrugAdministrationFormGroup);
    }

}
