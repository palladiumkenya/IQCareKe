import { HeiService } from './../_services/hei.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { FormGroup, FormArray } from '@angular/forms';
import { LongDateFormatKey } from 'moment';

@Component({
    selector: 'app-hei',
    templateUrl: './hei.component.html',
    styleUrls: ['./hei.component.css']
})
export class HeiComponent implements OnInit {
    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;

    deliveryOptions: any[] = [];
    maternalhistoryOptions: any[] = [];
    hivtestingOptions: any[] = [];

    motherreceivedrugsOptions: any[] = [];
    heimotherregimenOptions: any[] = [];
    yesnoOptions: any[] = [];
    motherdrugsatinfantenrollmentOptions: any[] = [];
    primarycaregiverOptions: any[] = [];
    immunizationHistoryOptions: any[] = [];
    milestoneOptions: any[] = [];

    deliveryModeOptions: LookupItemView[] = [];
    arvprophylaxisOptions: LookupItemView[] = [];
    placeofdeliveryOptions: LookupItemView[] = [];
    motherstateOptions: LookupItemView[] = [];
    infantFeedingOptions: LookupItemView[] = [];
    immunizationPeriodOptions: LookupItemView[] = [];
    immunizationGivenOptions: LookupItemView[] = [];
    milestoneAssessedOptions: LookupItemView[] = [];
    milestoneStatusOptions: LookupItemView[] = [];
    heiOutcomeOptions: LookupItemView[] = [];

    isLinear: boolean = true;
    deliveryMatFormGroup: FormArray;
    visitDetailsFormGroup: FormArray;

    infantFeedingFormGroup: FormGroup;
    heiOutcomeFormGroup: FormGroup;
    // nextAppointmentFormGroup: FormGroup;

    constructor(private route: ActivatedRoute,
        private heiService: HeiService) {
        this.deliveryMatFormGroup = new FormArray([]);
        this.visitDetailsFormGroup = new FormArray([]);
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const { patientId, personId, serviceAreaId } = params;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceAreaId = serviceAreaId;
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        this.route.data.subscribe((res) => {
            const {
                placeofdeliveryOptions,
                deliveryModeOptions,
                arvprophylaxisOptions,
                motherstateOptions,
                motherreceivedrugsOptions,
                heimotherregimenOptions,
                yesnoOptions,
                primarycaregiverOptions,
                motherdrugsatinfantenrollmentOptions,
                infantFeedingOptions,
                immunizationPeriodOptions,
                immunizationGivenOptions,
                milestoneAssessedOptions,
                milestoneStatusOptions,
                heiOutcomeOptions
            } = res;
            console.log('test options');
            console.log(res);
            this.placeofdeliveryOptions = placeofdeliveryOptions['lookupItems'];
            this.deliveryModeOptions = deliveryModeOptions['lookupItems'];
            this.arvprophylaxisOptions = arvprophylaxisOptions['lookupItems'];
            this.motherstateOptions = motherstateOptions['lookupItems'];
            this.motherreceivedrugsOptions = motherreceivedrugsOptions['lookupItems'];
            this.heimotherregimenOptions = heimotherregimenOptions['lookupItems'];
            this.yesnoOptions = yesnoOptions['lookupItems'];
            this.motherdrugsatinfantenrollmentOptions = motherdrugsatinfantenrollmentOptions['lookupItems'];
            this.primarycaregiverOptions = primarycaregiverOptions['lookupItems'];
            this.infantFeedingOptions = infantFeedingOptions['lookupItems'];
            this.immunizationPeriodOptions = immunizationPeriodOptions['lookupItems'];
            this.immunizationGivenOptions = immunizationGivenOptions['lookupItems'];
            this.milestoneAssessedOptions = milestoneAssessedOptions['lookupItems'];
            this.milestoneStatusOptions = milestoneStatusOptions['lookupItems'];
            this.heiOutcomeOptions = heiOutcomeOptions['lookupItems'];
        });

        this.deliveryOptions.push({
            'placeofdeliveryOptions': this.placeofdeliveryOptions,
            'deliveryModeOptions': this.deliveryModeOptions,
            'arvprophylaxisOptions': this.arvprophylaxisOptions
        });

        this.maternalhistoryOptions.push({
            'motherstateOptions': this.motherstateOptions,
            'motherreceivedrugsOptions': this.motherreceivedrugsOptions,
            'heimotherregimenOptions': this.heimotherregimenOptions,
            'yesnoOptions': this.yesnoOptions,
            'motherdrugsatinfantenrollmentOptions': this.motherdrugsatinfantenrollmentOptions,
            'primarycaregiverOptions': this.primarycaregiverOptions
        });

        this.immunizationHistoryOptions.push({
            'immunizationPeriod': this.immunizationPeriodOptions,
            'immunizationGiven': this.immunizationGivenOptions

        });

        this.milestoneOptions.push({
            'assessed': this.milestoneAssessedOptions,
            'status': this.milestoneStatusOptions,
            'yesnoOption': this.yesnoOptions
        });

        this.hivtestingOptions.push({

        });

    }

    onDeliveryNotify(formGroup: FormGroup): void {
        this.deliveryMatFormGroup.push(formGroup);
    }

    onMatHistoryNotify(formGroup: FormGroup): void {
        this.deliveryMatFormGroup.push(formGroup);
    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }
    onInfantFeedingNotify(formGroup: FormGroup): void {
        this.infantFeedingFormGroup = formGroup;
    }

    onMilestonesNotify(formGroup: FormGroup): void {
        // this.milestonesFormGroup.push(formGroup);
    }

    onImmunizationHistory(formGroup: FormGroup): void {
        // this.immunizationHistoryFormGroup.push(formGroup);
    }

    onCompleteEncounter() {
        console.log(this.deliveryMatFormGroup.value);
        console.log(this.visitDetailsFormGroup.value);
        this.patientMasterVisitId = 2;

        const motherRegistered = this.yesnoOptions.filter(
            obj => obj.itemId == this.deliveryMatFormGroup.value[1]['motherregisteredinclinic']
        );

        let isMotherRegistered: boolean = false;
        if (motherRegistered.length > 0) {
            if (motherRegistered[0]['itemName'] == 'Yes') {
                isMotherRegistered = true;
            } else if (motherRegistered[0]['itemName'] == 'No') {
                isMotherRegistered = false;
            }
        }

        this.heiService.saveHieDelivery(this.patientId, this.patientMasterVisitId, this.userId,
            isMotherRegistered, this.deliveryMatFormGroup.value[0], this.deliveryMatFormGroup.value[1])
            .subscribe(
                (result) => {
                    console.log(result);
                }
            );
    }
}
