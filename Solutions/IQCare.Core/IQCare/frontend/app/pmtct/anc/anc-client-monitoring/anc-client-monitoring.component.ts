import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-anc-client-monitoring',
    templateUrl: './anc-client-monitoring.component.html',
    styleUrls: ['./anc-client-monitoring.component.css']
})
export class AncClientMonitoringComponent implements OnInit {
    public clientMonitoringFormGroup: FormGroup;

    constructor(private fb: FormBuilder, private lookupItemService: LookupItemService, private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
    }

    ngOnInit() {
        this.clientMonitoringFormGroup = this.fb.group({

            cacxComments: ['', Validators.required]

        });
    }

}
