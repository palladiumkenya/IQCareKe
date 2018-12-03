import { Component, NgZone, OnInit } from '@angular/core';
import { FamilyScreening } from '../_models/familyScreening';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { FamilyService } from '../_services/family.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import * as moment from 'moment';

@Component({
    selector: 'app-family-screening',
    templateUrl: './family-screening.component.html',
    styleUrls: ['./family-screening.component.css']
})
export class FamilyScreeningComponent implements OnInit {
    familyScreening: FamilyScreening;
    formGroup: FormGroup;
    eligibleTestingOptions: LookupItemView[];
    hivStatusOptions: LookupItemView[];
    familyScreeningCategories: LookupItemView[];

    constructor(private _formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private familyService: FamilyService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private router: Router,
        public zone: NgZone) { }
    ngOnInit() {
        this.familyScreening = new FamilyScreening();
        this.familyScreening.userId = JSON.parse(localStorage.getItem('appUserId'));

        this.formGroup = this._formBuilder.group({
            dateOfScreening: new FormControl(this.familyScreening.dateOfScreening, [Validators.required]),
            hivStatus: new FormControl(this.familyScreening.hivStatus, [Validators.required]),
            eligibleForTesting: new FormControl(this.familyScreening.eligibleForTesting, [Validators.required]),
            dateBooked: new FormControl(this.familyScreening.dateBooked, [Validators.required]),
        });

        this.route.data.subscribe((res) => {
            // console.log(res);
            const options = res['options']['lookupItems'];

            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'YesNo') {
                    this.eligibleTestingOptions = options[i].value;
                } else if (options[i].key == 'ScreeningHivStatus') {
                    this.hivStatusOptions = options[i].value;
                } else if (options[i].key == 'FamilyScreening') {
                    this.familyScreeningCategories = options[i].value;
                }
            }
        });
    }

    onSubmit() {
        console.log(this.formGroup);
        if (this.formGroup.valid) {
            console.log('valid');
            this.familyScreening = Object.assign(this.familyScreening, this.formGroup.value);

            this.familyScreening.personId = JSON.parse(localStorage.getItem('partnerId'));
            this.familyScreening.patientId = JSON.parse(localStorage.getItem('patientId'));
            this.familyScreening.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));

            if (!this.familyScreening.dateBooked) {
                this.familyScreening.dateBooked = '';
            } else {
                this.familyScreening.dateBooked = moment(this.familyScreening.dateBooked).toDate().toDateString();
            }

            const arr = new Array();

            for (let i = 0; i < this.familyScreeningCategories.length; i++) {
                const familyScreening = new Object();
                familyScreening['screeningTypeId'] = this.familyScreeningCategories[i]['masterId'];
                familyScreening['screeningCategoryId'] = this.familyScreeningCategories[i]['itemId'];

                if (this.familyScreeningCategories[i]['itemName'] == 'ScreeningHivStatus') {
                    familyScreening['screeningValueId'] = this.familyScreening.hivStatus;
                } else if (this.familyScreeningCategories[i]['itemName'] == 'EligibleTesting') {
                    familyScreening['screeningValueId'] = this.familyScreening.eligibleForTesting;
                }

                familyScreening['userId'] = this.familyScreening.userId;
                arr.push(familyScreening);
            }

            this.familyScreening.dateOfScreening = moment(this.familyScreening.dateOfScreening).toDate().toDateString();

            this.familyService.addFamilyScreening(this.familyScreening, arr).subscribe(res => {
                console.log('res');
                this.snotifyService.success('Successfully saved family screening',
                    'Family Screening', this.notificationService.getConfig());
                this.zone.run(() => { this.router.navigate(['/hts/family'], { relativeTo: this.route }); });
            }, err => {
                this.snotifyService.error('Error saving family screening ' + err,
                    'Family Screening', this.notificationService.getConfig());
            });
            console.log(this.familyScreening);
        } else {
            console.log('invalid');
            return;
        }
    }

    onHivStatusSelected() {
        const hivStatus = this.formGroup.value.hivStatus;
        console.log(hivStatus);
        const optionHivStatus = this.hivStatusOptions.filter(function (obj) {
            return obj.itemId == hivStatus;
        });

        if (optionHivStatus[0]['itemName'] == 'Positive') {
            this.formGroup.controls.eligibleForTesting.disable({ onlySelf: true });
            this.formGroup.controls.eligibleForTesting.setValue(null);
            this.formGroup.controls.dateBooked.disable({ onlySelf: true });
            this.formGroup.controls.dateBooked.setValue(null);
        } else {
            this.formGroup.controls.eligibleForTesting.enable({ onlySelf: true });
            this.formGroup.controls.dateBooked.enable({ onlySelf: true });
        }
    }
}
