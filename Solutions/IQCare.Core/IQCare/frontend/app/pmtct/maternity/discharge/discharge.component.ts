import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {NotificationService} from '../../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import * as moment from 'moment';
import { MaternityService } from '../../_services/maternity.service';
import { DataService } from '../../../shared/_services/data.service';

@Component({
    selector: 'app-discharge',
    templateUrl: './discharge.component.html',
    styleUrls: ['./discharge.component.css']
})
export class DischargeComponent implements OnInit {
    dischargeFormGroup: FormGroup;
    @Input() dischargeOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    public deliveryStateOptions: any[] = [];
    public referralOptions: any[] = [];
    public yesnoOptions: any[] = [];
    public maxDate: Date = moment().toDate();
    public minDate : Date;

    constructor(private formBuilder: FormBuilder,
        private maternityService : MaternityService,
                private dataService : DataService,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService) {
    }

    ngOnInit() {
        this.dischargeFormGroup = this.formBuilder.group({
            dischargeDate: new FormControl('', [Validators.required]),
            babyStatus: new FormControl('', [Validators.required]),
            id : new FormControl('')
        });
        const {
            deliveryStates,
            referrals,
            yesNos
        } = this.dischargeOptions[0];
        this.yesnoOptions = yesNos;
        this.deliveryStateOptions = deliveryStates;
        this.referralOptions = referrals;

        this.notify.emit(this.dischargeFormGroup);
        this.dataService.visitDate.subscribe(date=>{
            this.minDate = date
        })
        if(this.isEdit)
         this.getPatientDischargeInfo(this.patientMasterVisitId)

        
    }


    private getPatientDischargeInfo(masterVisitId : any){
        this.maternityService.getPatientDischargeInfo(masterVisitId).subscribe(res=>{
             this.dischargeFormGroup.get('id').setValue(res.id)
             this.dischargeFormGroup.get('dischargeDate').setValue(res.dateDischarged)
             this.dischargeFormGroup.get('babyStatus').setValue(res.outcomeStatusId)
        })
    }


}
