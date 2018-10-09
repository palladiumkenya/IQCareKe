import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-delivery',
    templateUrl: './delivery.component.html',
    styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent implements OnInit {
    DeliveryForm: FormGroup;
    placeofdeliveryOptions: any[] = [];
    deliveryModeOptions: any[] = [];
    arvprophylaxisOptions: any[] = [];

    @Input('deliveryOptions') deliveryOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.DeliveryForm = this._formBuilder.group({
            placeofdelivery: new FormControl('', [Validators.required]),
            modeofdelivery: new FormControl('', [Validators.required]),
            birthweight: new FormControl('', [Validators.required]),
            arvprophylaxisreceived: new FormControl('', [Validators.required]),
            arvprophylaxisother: new FormControl('', [Validators.required])
        });

        const { placeofdeliveryOptions, deliveryModeOptions, arvprophylaxisOptions } = this.deliveryOptions[0];
        this.placeofdeliveryOptions = placeofdeliveryOptions;
        this.deliveryModeOptions = deliveryModeOptions;
        this.arvprophylaxisOptions = arvprophylaxisOptions;


        this.notify.emit(this.DeliveryForm);
    }

    onArvProphylaxisReceivedChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Other') {
            this.DeliveryForm.controls['arvprophylaxisother'].enable({ onlySelf: false });
        } else if (event.source.selected) {
            this.DeliveryForm.controls.arvprophylaxisother.setValue('');
            this.DeliveryForm.controls['arvprophylaxisother'].disable();
        }
    }
}
