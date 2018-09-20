import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Tracing} from '../_models/tracing';
import {ActivatedRoute, Router} from '@angular/router';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {FamilyTracing} from '../_models/familyTracing';
import {FamilyService} from '../_services/family.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';

@Component({
  selector: 'app-family-tracing',
  templateUrl: './family-tracing.component.html',
  styleUrls: ['./family-tracing.component.css']
})
export class FamilyTracingComponent implements OnInit {
    formGroup: FormGroup;
    tracing: FamilyTracing;

    tracingModeOptions: LookupItemView[];
    tracingOutcomeOptions: LookupItemView[];
    consentToTestingOptions: LookupItemView[];
    tracingTypeOptions: LookupItemView[];


    constructor(private _formBuilder: FormBuilder,
                private route: ActivatedRoute,
                private familyService: FamilyService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                private router: Router,
                public zone: NgZone) { }

    ngOnInit() {
        this.tracing = new FamilyTracing();
        this.formGroup = this._formBuilder.group({
            mode: new FormControl(this.tracing.mode, [Validators.required]),
            outcome: new FormControl(this.tracing.outcome, [Validators.required]),
            dateFamilyContacted: new FormControl(this.tracing.dateFamilyContacted, [Validators.required]),
            dateReminded: new FormControl(this.tracing.dateReminded, [Validators.required]),
            consent: new FormControl(this.tracing.consent, [Validators.required]),
            dateBooked: new FormControl(this.tracing.dateBooked, [Validators.required]),
        });

        this.route.data.subscribe((res) => {
            // console.log(res);
            const options = res['options']['lookupItems'];

            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'TracingMode') {
                    this.tracingModeOptions = options[i].value;
                } else if (options[i].key == 'FamilyTracingOutcome') {
                    this.tracingOutcomeOptions = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.consentToTestingOptions = options[i].value;
                } else if (options[i].key == 'TracingType') {
                    this.tracingTypeOptions = options[i].value;
                }
            }
        });

        this.tracing.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.tracing.personId = JSON.parse(localStorage.getItem('partnerId'));
    }

    onSubmit() {
        // console.log(this.formGroup);
        if (this.formGroup.valid) {
            this.tracing = {...this.tracing, ...this.formGroup.value};

            const tracingTypeValue = this.tracingTypeOptions.filter(function (obj) {
                return obj.itemName == 'Family';
            });

            const tracingType = tracingTypeValue[0]['itemId'];

            this.familyService.addFamilyTracing(this.tracing, tracingType).subscribe((res) => {
                this.snotifyService.success('Successfully Traced Family Member', 'Family Tracing',
                    this.notificationService.getConfig());

                this.zone.run(() => { this.router.navigate(['/hts/family'], { relativeTo: this.route}); });
            }, err => {
                this.snotifyService.error('Error saving family tracing ' + err,
                    'Family Tracing', this.notificationService.getConfig());
            });
        } else {
            // not valid
            return;
        }
    }

    onTracingOutcomeChange() {
        console.log(this.formGroup.value.outcome);
        const selectedItem = this.tracingOutcomeOptions.filter(obj => obj.itemId == this.formGroup.value.outcome);
        if (selectedItem.length > 0 && selectedItem[0].itemName == 'Not Contacted') {
            this.formGroup.controls.consent.disable({onlySelf: true});
            this.formGroup.controls.consent.setValue('');
            this.formGroup.controls.dateBooked.disable({onlySelf: true});
            this.formGroup.controls.dateBooked.setValue('');
        } else {
            this.formGroup.controls.consent.enable({onlySelf: false});
            this.formGroup.controls.dateBooked.enable({onlySelf: false});
        }
    }
}
