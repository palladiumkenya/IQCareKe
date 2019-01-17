import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Component, NgZone, OnInit } from '@angular/core';
import { PartnerView, Pnsform } from '../_models/pnsform';
import { PnsService } from '../_services/pns.service';
import { ClientService } from '../../shared/_services/client.service';
import { ActivatedRoute, Router } from '@angular/router';
import * as Consent from '../../shared/reducers/app.states';
import { select, Store } from '@ngrx/store';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { AppStateService } from '../../shared/_services/appstate.service';
import { AppEnum } from '../../shared/reducers/app.enum';

@Component({
    selector: 'app-pnsform',
    templateUrl: './pnsform.component.html',
    styleUrls: ['./pnsform.component.css']
})
export class PnsformComponent implements OnInit {
    pnsForm: Pnsform;
    yesNoNAOptions: any[];
    yesNoOptions: LookupItemView[];
    ipvOutcomeOptions: any[];
    yesNoDeclinedOptions: any[];
    pnsRelationshipOptions: any[];
    hivStatusOptions: LookupItemView[];
    pnsApproachOptions: any[];
    pnsScreeningCategories: any[];
    isNotIPVDone: boolean = false;
    ishivStatusPositive: boolean = false;
    isPnsAcceptedDisabled: boolean = true;

    serviceAreaId: number = 2;
    maxDate: any;
    minDate: any;

    constructor(private pnsService: PnsService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private store: Store<AppState>,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private appStateService: AppStateService) {
        this.maxDate = new Date();
        this.minDate = new Date();
    }

    ngOnInit() {
        this.pnsForm = new Pnsform();
        this.serviceAreaId = 2; // JSON.parse(localStorage.getItem('serviceAreaId'));

        this.getPnsOptions();
        this.getScreeningCategories();
    }

    public getScreeningCategories() {
        this.pnsService.getScreeningCategories().subscribe(data => {
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'PnsScreening') {
                    this.pnsScreeningCategories = options[i].value;
                }
            }
        });
    }

    public getPnsOptions() {
        this.pnsService.getCustomOptions().subscribe(data => {
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'YesNoNA') {
                    this.yesNoNAOptions = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'IpvOutcome') {
                    this.ipvOutcomeOptions = options[i].value;
                } else if (options[i].key == 'YesNoDeclined') {
                    this.yesNoDeclinedOptions = options[i].value;
                } else if (options[i].key == 'PNSRelationship') {
                    this.pnsRelationshipOptions = options[i].value;
                } else if (options[i].key == 'HivStatus') {
                    this.hivStatusOptions = options[i].value;
                } else if (options[i].key == 'PnsApproach') {
                    this.pnsApproachOptions = options[i].value;
                }
            }

            const pnsAcceptedOption = this.yesNoOptions.filter(function (obj) {
                return obj.itemName == 'Yes';
            });

            this.pnsForm.pnsAccepted = pnsAcceptedOption[0]['itemId'];

        }, err => {
            console.log(err);
        });
    }

    public onSubmit() {
        this.pnsForm.personId = JSON.parse(localStorage.getItem('partnerId'));
        this.pnsForm.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.pnsForm.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
        this.pnsForm.userId = JSON.parse(localStorage.getItem('appUserId'));

        const arr = new Array();

        for (let i = 0; i < this.pnsScreeningCategories.length; i++) {
            const pnsScreening = new Object();
            pnsScreening['screeningTypeId'] = this.pnsScreeningCategories[i]['masterId'];
            pnsScreening['screeningCategoryId'] = this.pnsScreeningCategories[i]['itemId'];

            if (this.pnsScreeningCategories[i]['itemName'] == 'PnsPhysicallyHurt' && this.pnsForm.partnerPhysicallyHurt &&
                this.pnsForm.partnerPhysicallyHurt != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.partnerPhysicallyHurt;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PnsThreatenedHurt' && this.pnsForm.partnerThreatenedHurt &&
                this.pnsForm.partnerThreatenedHurt != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.partnerThreatenedHurt;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PnsForcedSexual' && this.pnsForm.forcedSexualUncomfortable &&
                this.pnsForm.forcedSexualUncomfortable != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.forcedSexualUncomfortable;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'IPVOutcome' && this.pnsForm.ipvOutcome &&
                this.pnsForm.ipvOutcome != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.ipvOutcome;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PnsRelationship' && this.pnsForm.pnsRelationship &&
                this.pnsForm.pnsRelationship != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.pnsRelationship;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'LivingWithClient' && this.pnsForm.livingWithClient &&
                this.pnsForm.livingWithClient != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.livingWithClient;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'HIVStatus' && this.pnsForm.hivStatus &&
                this.pnsForm.hivStatus != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.hivStatus;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PNSApproach' && this.pnsForm.pnsApproach &&
                this.pnsForm.pnsApproach != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.pnsApproach;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'EligibleTesting' && this.pnsForm.eligibleTesting &&
                this.pnsForm.eligibleTesting != 0) {
                pnsScreening['screeningValueId'] = this.pnsForm.eligibleTesting;
            }

            pnsScreening['userId'] = this.pnsForm.userId;
            arr.push(pnsScreening);
        }

        this.pnsService.addPnsScreening(this.pnsForm, arr).subscribe(data => {
            const partnerPnsScreened = {
                'partnerId': this.pnsForm.personId,
                'pnsScreened': true
            };

            const hivStatusSelected = this.hivStatusOptions.filter(obj => obj.itemId == this.pnsForm.hivStatus);
            if (hivStatusSelected.length > 0 && hivStatusSelected[0].itemName == 'Positive') {
                const partnerPnsTraced = {
                    'partnerId': this.pnsForm.personId,
                    'pnsScreenedPositive': true
                };

                this.store.dispatch(new Consent.PnsScreenedPositive(JSON.stringify(partnerPnsTraced)));
                this.appStateService.addAppState(AppEnum.PNS_SCREENED_POSITIVE, JSON.parse(localStorage.getItem('personId')),
                    JSON.parse(localStorage.getItem('patientId')), null, null, JSON.stringify({
                        'partnerId': this.pnsForm.personId,
                        'pnsScreenedPositive': true
                    })).subscribe();
            }

            this.store.dispatch(new Consent.IsPnsScreened(JSON.stringify(partnerPnsScreened)));
            this.appStateService.addAppState(AppEnum.PNS_SCREENED, JSON.parse(localStorage.getItem('personId')),
                JSON.parse(localStorage.getItem('patientId')), null, null, JSON.stringify({
                    'partnerId': this.pnsForm.personId,
                    'pnsScreened': true
                })).subscribe();

            this.store.pipe(select('app')).subscribe(res => {
                localStorage.setItem('store', JSON.stringify(res));
            });

            this.snotifyService.success('Successfully pns screening',
                'PNS Screening', this.notificationService.getConfig());

            this.zone.run(() => { this.router.navigate(['/hts/pns'], { relativeTo: this.route }); });
        }, (err) => {
            this.snotifyService.error('Error saving PNS screening ' + err,
                'PNS Screening', this.notificationService.getConfig());
        });
    }

    public onIpvScreeningChange(val: number) {
        const optionSelected = this.yesNoNAOptions.filter(function (obj) {
            return obj.itemId == val;
        });

        if (optionSelected.length > 0 && optionSelected[0]['itemName'] !== 'Yes') {
            this.pnsForm.forcedSexualUncomfortable = null;
            this.pnsForm.partnerPhysicallyHurt = null;
            this.pnsForm.partnerThreatenedHurt = null;

            this.isNotIPVDone = true;
        } else {
            this.isNotIPVDone = false;
        }
    }

    public onHivStatus(val: number) {
        const optionSelected = this.hivStatusOptions.filter(function (obj) {
            return obj.itemId == val;
        });

        if (optionSelected[0]['itemName'] == 'Positive') {
            this.pnsForm.pnsApproach = null;
            this.pnsForm.eligibleTesting = null;
            this.pnsForm.bookingDate = null;

            this.ishivStatusPositive = true;
        } else {
            this.ishivStatusPositive = false;
        }
    }
}
