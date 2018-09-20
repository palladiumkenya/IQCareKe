import { HeiService } from './../_services/hei.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { FormGroup, FormArray } from '@angular/forms';

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

    deliveryOptions: any[] = [];
    maternalhistoryOptions: any[] = [];
    hivtestingOptions: any[] = [];

    motherreceivedrugsOptions: any[] = [];
    heimotherregimenOptions: any[] = [];
    yesnoOptions: any[] = [];
    motherdrugsatinfantenrollmentOptions: any[] = [];
    primarycaregiverOptions: any[] = [];

    deliveryModeOptions: LookupItemView[] = [];
    arvprophylaxisOptions: LookupItemView[] = [];
    placeofdeliveryOptions: LookupItemView[] = [];
    motherstateOptions: LookupItemView[] = [];
    infantFeedingOptions: LookupItemView[] = [];
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

        this.heiService.saveHieDelivery(this.patientId, this.patientMasterVisitId, this.deliveryMatFormGroup.value[0])
            .subscribe(
                (result) => {

                }
            );
    }
}
