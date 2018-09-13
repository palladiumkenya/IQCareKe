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
    deliveryOptions: any[] = [];
    maternalhistoryoptions: any[] = [];

    deliveryModeOptions: LookupItemView[] = [];
    arvprophylaxisOptions: LookupItemView[] = [];
    placeofdeliveryOptions: LookupItemView[] = [];
    motherstateOptions: LookupItemView[] = [];

    isLinear: boolean = true;
    deliveryMatFormGroup: FormArray;


    constructor(private route: ActivatedRoute) {
        this.deliveryMatFormGroup = new FormArray([]);
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                console.log(params);
            }
        );

        this.route.data.subscribe((res) => {
            const { placeofdeliveryOptions, deliveryModeOptions, arvprophylaxisOptions, motherstateOptions } = res;
            this.placeofdeliveryOptions = placeofdeliveryOptions['lookupItems'];
            this.deliveryModeOptions = deliveryModeOptions['lookupItems'];
            this.arvprophylaxisOptions = arvprophylaxisOptions['lookupItems'];
            this.motherstateOptions = motherstateOptions['lookupItems'];
        });

        this.deliveryOptions.push({
            'placeofdeliveryOptions': this.placeofdeliveryOptions,
            'deliveryModeOptions': this.deliveryModeOptions,
            'arvprophylaxisOptions': this.arvprophylaxisOptions,
            'motherstateOptions': this.motherstateOptions
        });

        console.log(this.deliveryMatFormGroup);
        console.log(this.deliveryMatFormGroup.value);
    }

    onNotify(formGroup: FormGroup): void {
        this.deliveryMatFormGroup.push(formGroup);
    }

    onMatHistoryNotify(formGroup: FormGroup): void {
        this.deliveryMatFormGroup.push(formGroup);
    }
}
