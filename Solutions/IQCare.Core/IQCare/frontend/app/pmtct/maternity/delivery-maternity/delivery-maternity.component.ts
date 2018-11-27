import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import * as moment from 'moment';
import { MaternityService } from '../../_services/maternity.service';
import { Subscription } from 'rxjs/index';
import { isEmpty } from 'rxjs/internal/operators';

@Component({
    selector: 'app-delivery-maternity',
    templateUrl: './delivery-maternity.component.html',
    styleUrls: ['./delivery-maternity.component.css']
})
export class DeliveryMaternityComponent implements OnInit {

    deliveryFormGroup: FormGroup;
    @Input() diagnosisOptions: any[] = [];
    @Input('PatientId') PatientId: number;
    @Input('isEdit') isEdit: boolean;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    public deliveryModeOptions: any[] = [];
    public bloodlossOptions: any[] = [];
    public motherStateOptions: any[] = [];
    public yesnoOptions: any[] = [];
    public deliveryDate: Date;
    public visitDetails: Subscription;
    public motherProfile: Subscription;
    public dateLMP: Date;
    public isMotherAlive: boolean = true;
    public maxDate: Date = moment().toDate();

    constructor(private formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private _matService: MaternityService) {

    }

    ngOnInit() {
        this.deliveryFormGroup = this.formBuilder.group({
            ancVisits: new FormControl('', [Validators.required]),
            deliveryDate: new FormControl('', [Validators.required]),
            gestationAtBirth: new FormControl('', [Validators.required]),
            deliveryTime: new FormControl('', [Validators.required]),
            labourDuration: new FormControl('', [Validators.required]),

            deliveryMode: new FormControl('', [Validators.required]),
            bloodLoss: new FormControl('', [Validators.required]),
            bloodLossCount: new FormControl('', [Validators.required]),
            deliveryCondition: new FormControl('', [Validators.required]),

            placentaComplete: new FormControl('', [Validators.required]),
            maternalDeathsAudited: new FormControl('', [Validators.required]),
            auditDate: new FormControl('', [Validators.required]),
            deliveryComplications: new FormControl('', [Validators.required]),
            deliveryComplicationNotes: new FormControl('', [Validators.required]),
            deliveryConductedBy: new FormControl('', [Validators.required])
        });

        this.deliveryFormGroup.controls['bloodLossCount'].disable({ onlySelf: true });
        this.deliveryFormGroup.get('maternalDeathsAudited').disable({ onlySelf: true });
        this.deliveryFormGroup.get('auditDate').disable({ onlySelf: true });
        this.deliveryFormGroup.get('deliveryComplicationNotes').disable({ onlySelf: true });




        const {
            deliveryModes,
            bloodLoss,
            motherStates,
            yesNos
        } = this.diagnosisOptions[0];
        this.deliveryModeOptions = deliveryModes;
        this.bloodlossOptions = bloodLoss;
        this.motherStateOptions = motherStates;
        this.yesnoOptions = yesNos;

        this.getCurrentVisitDetails(this.PatientId);
        this.getPregnancyDetails(this.PatientId);
         if(this.isEdit){
             this.getPatientDeliveryInfo(this.PatientMasterVisitId);
         }
        this.notify.emit(this.deliveryFormGroup);
    }

    public onDeliveryDateChange() {
        this.deliveryDate = this.deliveryFormGroup.controls['deliveryDate'].value;

        const gestation = this.calculateGestation(this.deliveryDate,this.dateLMP)
        this.deliveryFormGroup.controls['gestationAtBirth'].setValue(gestation);
        this.deliveryFormGroup.controls['gestationAtBirth'].disable({ onlySelf: true });
    }

    public calculateGestation(deliveryDate: Date, dateLmp : Date): string{
        const now = moment(deliveryDate);
        const gestation = moment.duration(now.diff(dateLmp)).asWeeks().toFixed(1);
        return gestation;
    }

    onBloodLossChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'None') {
            this.deliveryFormGroup.get('bloodLossCount').disable({ onlySelf: true });
        } else {
            this.deliveryFormGroup.get('bloodLossCount').enable({ onlySelf: true });
        }
    }

    onChangeDeliveryConditions(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Dead') {
            this.deliveryFormGroup.get('maternalDeathsAudited').enable({ onlySelf: true });
            this.deliveryFormGroup.get('auditDate').enable({ onlySelf: true });
            this.isMotherAlive = false;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'Alive') {
            this.deliveryFormGroup.get('maternalDeathsAudited').disable({ onlySelf: true });
            this.deliveryFormGroup.get('auditDate').disable({ onlySelf: true });
            this.isMotherAlive = true;
        }
    }

    onDeliveryComplicationsChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.deliveryFormGroup.get('deliveryComplicationNotes').enable({ onlySelf: true });
        } else {
            this.deliveryFormGroup.get('deliveryComplicationNotes').disable({ onlySelf: true });
        }
    }

    public getPregnancyDetails(patientId: number) {
        this.motherProfile = this._matService.getPregnancyDetails(patientId)
            .subscribe(
                p => {
                    if (p) {
                        this.dateLMP = p.lmp;
                        console.log('lmp date' + this.dateLMP);
                    }

                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error fetching previous pregnacy Profile' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                    console.log(this.motherProfile);
                });
    }

    public getCurrentVisitDetails(patientId: number): void {
        this.visitDetails = this._matService.getCurrentVisitDetails(patientId)
            .subscribe(
                p => {
                    const visit = p;
                    if (visit && visit.visitNumber > 1) {
                        this.deliveryFormGroup.controls['ancVisits'].setValue(visit.visitNumber);
                        this.deliveryFormGroup.get('ancVisits').disable({ onlySelf: true });
                    } else {
                        this.deliveryFormGroup.controls['ancVisits'].setValue(1);
                    }
                },
                (err) => {
                    this.snotifyService.error('Error fetching visit details' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                });
    }

    public getPatientDeliveryInfo(masterVisitId: number): void {
        this._matService.GetPatientDeliveryInfo(masterVisitId)
            .subscribe(
                del => {
                    console.log(del)
                    if(del==null)
                      return;                        
                    this.deliveryFormGroup.controls['gestationAtBirth'].setValue( this.calculateGestation(del.dateOfDelivery,this.dateLMP));
                    this.deliveryFormGroup.controls['gestationAtBirth'].disable({ onlySelf: true });
                    this.deliveryFormGroup.controls['deliveryDate'].setValue(del.dateOfDelivery);
                    this.deliveryFormGroup.controls['deliveryTime'].setValue(del.timeOfDelivery);
                    this.deliveryFormGroup.controls['labourDuration'].setValue(del.durationOfLabour);
                    this.deliveryFormGroup.controls['deliveryMode'].setValue(del.modeOfDeliveryId);
                    this.deliveryFormGroup.controls['bloodLoss'].setValue(del.bloodLossClassificationId);
                    this.deliveryFormGroup.controls['bloodLossCount'].setValue(del.bloodLossCapacity);
                    this.deliveryFormGroup.controls['deliveryCondition'].setValue(del.motherConditionId);
                    this.deliveryFormGroup.controls['placentaComplete'].setValue(del.placentaCompleteId);
                    this.deliveryFormGroup.controls['maternalDeathsAudited'].setValue(del.maternalDeathAuditedId);
                    this.deliveryFormGroup.controls['auditDate'].setValue(del.maternalDeathAuditDate);
                    this.deliveryFormGroup.controls['deliveryComplications'].setValue(del.deliveryComplicationsExperiencedId);
                    this.deliveryFormGroup.controls['deliveryComplicationNotes'].setValue(del.deliveryComplicationNotes);
                    this.deliveryFormGroup.controls['deliveryConductedBy'].setValue(del.deliveryConductedBy);
                },
                (err) => {
                    this.snotifyService.error('Error fetching patient delivery info details' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                });
    }

}
