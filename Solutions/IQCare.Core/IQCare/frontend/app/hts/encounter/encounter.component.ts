import {Component, NgZone, OnInit} from '@angular/core';
import {Encounter} from '../_models/encounter';
import {EncounterService} from '../_services/encounter.service';
import {FinalTestingResults, Testing} from '../_models/testing';
import {ActivatedRoute, Router} from '@angular/router';
import {Store} from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import {AppStateService} from '../../shared/_services/appstate.service';
import {AppEnum} from '../../shared/reducers/app.enum';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';

declare var $: any;

@Component({
  selector: 'app-encounter',
  templateUrl: './encounter.component.html',
  styleUrls: ['./encounter.component.css']
})
export class EncounterComponent implements OnInit {
    encounter: Encounter;

    isNoOfMonths: boolean = true;
    isDisabilitiesEnabled: boolean = true;
    isClientTestedDisabled: boolean = false;
    isStrategyDisabled: boolean = false;

    entryPoints: any[];
    yesNoOptions: any[];
    disabilities: any[];
    testedAs: any[];
    strategyOptions: any[];
    tbStatus: any[];
    hivResultsOptions: any[];
    hivFinalResultsOptions: any[];
    hivTestKits: any[];

    maxDate: any;


    constructor(private _encounterService: EncounterService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private store: Store<AppState>,
                private appStateService: AppStateService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.encounter = new Encounter();
        // set default values
        this.encounter.PersonId = JSON.parse(localStorage.getItem('personId'));
        this.encounter.PatientId = JSON.parse(localStorage.getItem('patientId'));
        this.encounter.ServiceAreaId = 2;

        this.encounter.ProviderId = JSON.parse(localStorage.getItem('appUserId'));
        this.encounter.PatientEncounterID = 1;
        this.encounter.MonthSinceSelfTest = null;
        this.encounter.GeoLocation = null;

        this.getHtsOptions();
        this.getEncounterType();
    }

    validate() {
        const self = this;
        setTimeout(() => {
            $('#form').parsley().destroy();
            $('#form').parsley({
                excluded: 'input[type=button], input[type=submit], input[type=reset], input[type=hidden],[max], [disabled], :hidden'
            });

            $('#form').parsley().validate();
            if ($('#form').parsley().isValid()) {
                console.log('valid');
                self.onSubmitForm();
            } else {
                console.log('not valid');
                return false;
            }
        }, 0);
    }

    getHtsOptions() {
        this._encounterService.getHtsEncounterOptions().subscribe(res => {
            const options = res['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                // console.log(options[i]);
                if (options[i].key == 'HTSEntryPoints') {
                    this.entryPoints = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'Disabilities') {
                    this.disabilities = options[i].value;
                } else if (options[i].key == 'TestedAs') {
                    this.testedAs = options[i].value;
                } else if (options[i].key == 'Strategy') {
                    this.strategyOptions = options[i].value;
                } else if (options[i].key == 'TBStatus') {
                    this.tbStatus = options[i].value;
                }
            }
        });
    }

    getEncounterType() {
        this._encounterService.getEncounterType().subscribe(res => {
            this.encounter.EncounterTypeId = res['itemId'];
        });
    }

    onSubmitForm() {
        // console.log(this.encounter);
        const isConsented = this.encounter.Consent;
        const testedAs = this.encounter.TestedAs;

        this._encounterService.addEncounter(this.encounter).subscribe(data => {
            console.log(data);
            localStorage.setItem('htsEncounterId', data['htsEncounterId']);
            localStorage.setItem('patientMasterVisitId', data['patientMasterVisitId']);

            const optionSelected = this.yesNoOptions.filter(function( obj ) {
                return obj.itemId == isConsented;
            });

            if (testedAs) {
                const optionTestedAs = this.testedAs.filter(function (obj) {
                    return obj.itemId == testedAs;
                });

                if (optionTestedAs[0]['itemName'] == 'I: Individual') {
                    this.store.dispatch(new Consent.TestedAs(true));
                    this.appStateService.addAppState(AppEnum.TESTED_AS, this.encounter.PersonId,
                        this.encounter.PatientId, data['patientMasterVisitId'], data['htsEncounterId']).subscribe();
                }
            }

            this.snotifyService.success('Successfully saved encounter', 'Encounter', this.notificationService.getConfig());

            if (optionSelected[0]['itemName'] == 'Yes') {
                this.store.dispatch(new Consent.ConsentTesting(true));
                this.appStateService.addAppState(AppEnum.CONSENT_TESTING, this.encounter.PersonId,
                    this.encounter.PatientId, data['patientMasterVisitId'], data['htsEncounterId']).subscribe();


                this.zone.run(() => { this.router.navigate(['/hts/testing'], {relativeTo: this.route }); });
            } else {
                this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
            }

        }, err => {
            console.log(err);
            this.snotifyService.error('Error saving encounter', 'Encounter', this.notificationService.getConfig());
        });
    }

    everTestedChanged(everTested: number) {
        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == everTested;
        });

        if (optionSelected[0].itemName == 'Yes') {
            this.isNoOfMonths = false;
        } else {
            this.isNoOfMonths = true;
            this.encounter.MonthsSinceLastTest = null;
        }
    }

    hasDisabilityChanged(hasDisability: number) {
        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == hasDisability;
        });

        if (optionSelected[0].itemName == 'Yes') {
            this.isDisabilitiesEnabled = false;
        } else {
            this.isDisabilitiesEnabled = true;
            this.encounter.Disabilities = [];
        }
    }

    onConsentChanged(consent: number) {

        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == consent;
        });

        if (optionSelected[0].itemName == 'No') {
            this.encounter.TestedAs = null;
            this.encounter.TestingStrategy = null;

            this.isClientTestedDisabled = true;
            this.isStrategyDisabled = true;
        } else {
            this.isClientTestedDisabled = false;
            this.isStrategyDisabled = false;
        }
    }
}
