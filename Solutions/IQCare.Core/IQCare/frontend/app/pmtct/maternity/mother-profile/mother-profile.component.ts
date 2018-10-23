import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import * as moment from 'moment';

@Component({
    selector: 'app-mother-profile',
    templateUrl: './mother-profile.component.html',
    styleUrls: ['./mother-profile.component.css']
})
export class MotherProfileComponent implements OnInit {

    motherProfileFormGroup: FormGroup;
    dateLMP: Date;

    constructor(private _formBuilder: FormBuilder,
                private _lookupItemService: LookupItemService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
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
}
