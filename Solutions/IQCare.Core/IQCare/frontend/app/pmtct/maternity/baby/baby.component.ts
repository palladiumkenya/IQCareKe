import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { MaternityService } from '../../_services/maternity.service';

@Component({
    selector: 'app-baby',
    templateUrl: './baby.component.html',
    styleUrls: ['./baby.component.css']
})
export class BabyComponent implements OnInit {

    @Input() babySectionOptions: any[] = [];
    @Input() PatientId: number;
    @Input() isEdit: boolean;
    @Input() PatientMasterVisitId: number;
    BabyData: any[] = [];

    @Output() notifyBabyData: EventEmitter<any[]> = new EventEmitter<any[]>();

    constructor() {
    }

    ngOnInit() {
        // this.notifyBabyData.emit(this.BabyData);
    }

    onBabyNotify($event) {
        this.BabyData = $event;
        this.notifyBabyData.emit(this.BabyData);
    }

}
