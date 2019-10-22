import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import * as moment from 'moment';
import { MaternityService } from '../../_services/maternity.service';
import { Input } from '@angular/core';
import { Subscription } from 'rxjs/index';
import {DataService} from '../../_services/data.service';

@Component({
    selector: 'app-mother-profile',
    templateUrl: './mother-profile.component.html',
    styleUrls: ['./mother-profile.component.css']
})
export class MotherProfileComponent implements OnInit {

    motherProfileFormGroup: FormGroup;
    dateLMP: Date;
    minLMpDate: Date;
    gestation: number;
    motherProfile: Subscription;
    visitDetails: Subscription;
   
    @Input() patientId: number;
    @Input() visitDate: Date;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    public maxDate: Date = moment().toDate();
    public minLmpDate: Date = moment().subtract(1, 'years').toDate();
    public minAgeMenarche: number = 9;


    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _matServices: MaternityService,
        private dataservice: DataService) {
    }

    ngOnInit() {
        this.motherProfileFormGroup = this._formBuilder.group({
            dateLMP: new FormControl('', [Validators.required]),
            dateEDD: new FormControl('', [Validators.required]),
            gestation: new FormControl('', [Validators.max(42), Validators.required]),
            ageAtMenarche: new FormControl('', [Validators.min(8), Validators.max(20)]),
            parityOne: new FormControl('', [Validators.min(0), Validators.max(20), Validators.required]),
            parityTwo: new FormControl('', [Validators.min(0), Validators.max(20), Validators.required]),
            gravidae: new FormControl('', [Validators.required]),
        });

        this.getPregnancyDetails(this.patientId);
        // this.getCurrentVisitDetails(this.patientId, 'ANC');


        this.notify.emit(this.motherProfileFormGroup);
    }

    public onLMPDateChange() {
        this.dateLMP = this.motherProfileFormGroup.controls['dateLMP'].value;
        this.dataservice.setDateLmp(this.dateLMP);
        this.minLMpDate = moment(moment(this.visitDate).subtract(42, 'weeks').format('')).toDate();

        if (moment(this.dateLMP).isBefore(this.minLMpDate)) {

            this.snotifyService.error('Current LMP Date CANNOT be More than 9 months before the VisitDate', 'Mother Profile',
                this.notificationService.getConfig());
            this.motherProfileFormGroup.get('dateLMP').setValue('');
            return false;
        }

        if (moment(this.dateLMP).isAfter(this.visitDate)) {
            this.snotifyService.error('LMP Date CANNOT be before VisitDate', 'Mother Profile',
                this.notificationService.getConfig());
            this.motherProfileFormGroup.get('dateLMP').setValue('');
            this.motherProfileFormGroup.get('dateEDD').setValue('');
            this.motherProfileFormGroup.get('gestation').setValue('');

            return false;
        }

        const lmpDate = new Date(moment(this.motherProfileFormGroup.controls['dateLMP'].value).add(280, 'days').format(''));
        const eddDate = new Date(moment(this.dateLMP).add(7, 'days').add(9, 'months').format(''));
        this.motherProfileFormGroup.controls['dateEDD'].setValue(eddDate);

        this.gestation = parseInt(moment.duration(moment(this.visitDate).diff(this.dateLMP)).asWeeks().toFixed(1), 10);
        if (this.gestation > 42) { this.gestation = 42; }
        if (this.gestation < 1) { this.gestation = 0; }
        this.motherProfileFormGroup.controls['gestation'].setValue(this.gestation);
    }

    public onParityTwoChange() {
        const parityOne: number = this.motherProfileFormGroup.controls['parityOne'].value;
        const parityTwo: number = this.motherProfileFormGroup.controls['parityTwo'].value;
        const gravidae: number = parseInt(parityOne.toString(), 10) + parseInt(String(parityTwo), 10);
        this.motherProfileFormGroup.controls['gravidae'].setValue(gravidae + parseInt('1', 10));
        this.motherProfileFormGroup.controls['gravidae'].disable({ onlySelf: true });
    }

    public getPregnancyDetails(patientId: number) {
        this.motherProfile = this._matServices.getPregnancyDetails(patientId)
            .subscribe(
                p => {
                    this.motherProfileFormGroup.controls['gestation'].setValue(p.gestation);
                    this.motherProfileFormGroup.controls['dateLMP'].setValue(p.lmp);
                    this.dataservice.setDateLmp(p.lmp);
                    this.motherProfileFormGroup.controls['dateEDD'].setValue(p.edd);
                    this.motherProfileFormGroup.controls['parityOne'].setValue(p.parity);
                    this.motherProfileFormGroup.controls['parityTwo'].setValue(p.parity2);
                    this.motherProfileFormGroup.controls['gravidae'].setValue(p.gravidae);
                    this.motherProfileFormGroup.controls['ageAtMenarche'].setValue(p.ageAtMenarche);
                },
                (err) => {
                    this.snotifyService.error('Error fetching previous pregnacy Profile' + err,
                        'Encounter', this.notificationService.getConfig());
                });
    }

    public getCurrentVisitDetails(patientId: number, serviceAreaName: string): void {
        this.visitDetails = this._matServices.getCurrentVisitDetails(patientId, serviceAreaName)
            .subscribe(
                p => {
                    if (p) {
                        console.log('agetmenarche' + p.ageMenarche);
                        const visitNumber = p.length;

                        //  this.motherProfileFormGroup.controls['ageAtMenarche'].setValue(p.ageMenarche);

                    }
                },
                (err) => {
                    this.snotifyService.error('Error fetching visit details' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                });
    }



}
