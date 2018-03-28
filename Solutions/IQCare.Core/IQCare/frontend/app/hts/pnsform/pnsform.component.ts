import {Component, NgZone, OnInit} from '@angular/core';
import {PartnerView, Pnsform} from '../_models/pnsform';
import {PnsService} from '../_services/pns.service';
import {ClientService} from '../../shared/_services/client.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-pnsform',
  templateUrl: './pnsform.component.html',
  styleUrls: ['./pnsform.component.css']
})
export class PnsformComponent implements OnInit {
    pnsForm: Pnsform;
    yesNoNAOptions: any[];
    yesNoOptions: any[];
    ipvOutcomeOptions: any[];
    yesNoDeclinedOptions: any[];
    pnsRelationshipOptions: any[];
    hivStatusOptions: any[];
    pnsApproachOptions: any[];
    pnsScreeningCategories: any[];

    serviceAreaId: number = 2;

    constructor(private pnsService: PnsService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone) { }

    ngOnInit() {
        this.pnsForm = new Pnsform();
        this.serviceAreaId = 2; // JSON.parse(localStorage.getItem('serviceAreaId'));

        this.getPnsOptions();
        this.getScreeningCategories();
    }

    public getScreeningCategories() {
        this.pnsService.getScreeningCategories().subscribe(data => {
            // console.log(data['lookupItems']);
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
            // console.log(data);
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                // console.log(options[i]);
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
        }, err => {
           console.log(err);
        });
    }

    public onSubmit() {
        this.pnsForm.personId = JSON.parse(localStorage.getItem('partnerId'));
        this.pnsForm.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.pnsForm.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));

        const arr = new Array();

        for (let i = 0; i < this.pnsScreeningCategories.length; i++) {
            const pnsScreening = new Object();
            pnsScreening['screeningTypeId'] = this.pnsScreeningCategories[i]['masterId'];
            pnsScreening['screeningCategoryId'] = this.pnsScreeningCategories[i]['itemId'];

            if (this.pnsScreeningCategories[i]['itemName'] == 'PnsPhysicallyHurt') {
                pnsScreening['screeningValueId'] = this.pnsForm.partnerPhysicallyHurt;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PnsThreatenedHurt') {
                pnsScreening['screeningValueId'] = this.pnsForm.partnerThreatenedHurt;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PnsForcedSexual') {
                pnsScreening['screeningValueId'] = this.pnsForm.forcedSexualUncomfortable;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'IPVOutcome') {
                pnsScreening['screeningValueId'] = this.pnsForm.ipvOutcome;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PnsRelationship') {
                pnsScreening['screeningValueId'] = this.pnsForm.pnsRelationship;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'LivingWithClient') {
                pnsScreening['screeningValueId'] = this.pnsForm.livingWithClient;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'HIVStatus') {
                pnsScreening['screeningValueId'] = this.pnsForm.hivStatus;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'PNSApproach') {
                pnsScreening['screeningValueId'] = this.pnsForm.pnsApproach;
            } else if (this.pnsScreeningCategories[i]['itemName'] == 'EligibleTesting') {
                pnsScreening['screeningValueId'] = this.pnsForm.eligibleTesting;
            }

            pnsScreening['userId'] = this.pnsForm.userId;
            arr.push(pnsScreening);
        }

        this.pnsService.addPnsScreening(this.pnsForm, arr).subscribe(data => {
            this.zone.run(() => { this.router.navigate(['/hts/pns'], {relativeTo: this.route }); });
        });
    }
}
