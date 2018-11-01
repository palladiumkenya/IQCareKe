import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import * as moment from 'moment';
import {MaternityService} from '../../_services/maternity.service';
import { Input } from '@angular/core';
import {Subscription} from 'rxjs/index';

@Component({
    selector: 'app-mother-profile',
    templateUrl: './mother-profile.component.html',
    styleUrls: ['./mother-profile.component.css']
})
export class MotherProfileComponent implements OnInit {

    motherProfileFormGroup: FormGroup;
    dateLMP: Date;
    motherProfile: Subscription;
    visitDetails: Subscription;
    @Input('patientId') patientId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
                private _lookupItemService: LookupItemService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                private _matServices: MaternityService) {
    }

    ngOnInit() {
        this.motherProfileFormGroup = this._formBuilder.group({
            dateLMP: new FormControl('', [Validators.required]),
            dateEDD: new FormControl('', [Validators.required]),
            ancVisitNumber: new FormControl('', [Validators.required]),
            gestation: new FormControl('', [Validators.required]),
            ageAtMenarche: new FormControl('', [Validators.required]),
            parityOne: new FormControl('', [Validators.required]),
            parityTwo:  new FormControl('', [Validators.required]),
            gravidae:  new FormControl('', [Validators.required]),
        });

        this.getPregnancyDetails(this.patientId);
        this.getCurrentVisitDetails(this.patientId);

        this.notify.emit(this.motherProfileFormGroup);
    }

    public onLMPDateChange() {
        this.dateLMP = this.motherProfileFormGroup.controls['dateLMP'].value;

        this.motherProfileFormGroup.controls['dateEDD'].setValue(moment(this.motherProfileFormGroup.controls['dateLMP'].value,
            'DD-MM-YYYY').add(280, 'days').format(''));

        const now = moment(new Date());
        const gestation = moment.duration(now.diff( this.dateLMP)).asWeeks().toFixed(1);
        this.motherProfileFormGroup.controls['gestation'].setValue(gestation);

        this.motherProfileFormGroup.controls['dateEDD'].disable({ onlySelf: true });
        console.log(moment(this.motherProfileFormGroup.controls['dateLMP'].value, 'DD-MM-YYYY').add(280, 'days'));
    }

    public onParityTwoChange() {
        const parityOne: number = this.motherProfileFormGroup.controls['parityOne'].value;
        const parityTwo: number = this.motherProfileFormGroup.controls['parityTwo'].value;
        const gravidae: number = parseInt(parityOne.toString(), 10 ) + parseInt(String(parityTwo), 10);
        this.motherProfileFormGroup.controls['gravidae'].setValue(gravidae + parseInt('1', 10));
        this.motherProfileFormGroup.controls['gravidae'].disable({ onlySelf: true });
    }

    public  getPregnancyDetails(patientId: number) {
        this.motherProfile = this._matServices.getPregnancyDetails(patientId)
            .subscribe(
                p => {
                    this.motherProfileFormGroup.controls['gestation'].setValue(p.gestation);
                    this.motherProfileFormGroup.controls['dateLMP'].setValue(p.lmp);
                    this.motherProfileFormGroup.controls['dateEDD'].setValue(p.edd);
                    this.motherProfileFormGroup.controls['parityOne'].setValue(p.parity);
                    this.motherProfileFormGroup.controls['parityTwo'].setValue(p.parity2);
                    this.motherProfileFormGroup.controls['gravidae'].setValue(p.gravidae);
                    console.log('pregnancy details');
                    console.log(p);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching previous pregnacy Profile' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                    console.log(this.motherProfile);
                });
    }

    public getCurrentVisitDetails(patientId: number): void {
        this.visitDetails = this._matServices.getCurrentVisitDetails(patientId)
            .subscribe(
                p => {
                    console.log('agetmenarche' + p.ageMenarche)
                    this.motherProfileFormGroup.controls['ageAtMenarche'].setValue(p.ageMenarche);
                },
                (err) => {
                    this.snotifyService.error('Error fetching visit details' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                });
    }

}
