import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-pnc-maternalhistory',
    templateUrl: './pnc-maternalhistory.component.html',
    styleUrls: ['./pnc-maternalhistory.component.css']
})
export class PncMaternalhistoryComponent implements OnInit {
    deliveryModeOptions: LookupItemView[] = [];

    MaternalHistoryForm: FormGroup;
    @Input('matHistoryOptions') matHistoryOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    maxDate: Date;

    constructor(private _formBuilder: FormBuilder) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.MaternalHistoryForm = this._formBuilder.group({
            dateofdelivery: new FormControl('', [Validators.required]),
            modeofdelivery: new FormControl('', [Validators.required])
        });

        const { deliveryModeOptions } = this.matHistoryOptions[0];
        this.deliveryModeOptions = deliveryModeOptions;

        this.notify.emit(this.MaternalHistoryForm);
    }

}
