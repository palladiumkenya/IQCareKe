import { PrepService } from './../../_services/prep.service';
import { PncService } from './../../../pmtct/_services/pnc.service';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import * as moment from 'moment';

@Component({
    selector: 'app-fertility-intention',
    templateUrl: './fertility-intention.component.html',
    styleUrls: ['./fertility-intention.component.css'],
    providers: [PncService]
})
export class FertilityIntentionComponent implements OnInit {
    FertilityIntentionForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    fpMethods: LookupItemView[] = [];
    planningPregnancy: LookupItemView[] = [];
    pregnancyStatusOptions: LookupItemView[] = [];

    maxDate: Date;
    dateLMP: Date;
    minLMpDate: Date;

    @Input() FertilityIntentionsOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientMasterVisitId: number;
    @Input() isEdit: number;
    @Input() visitDate: Date;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncservice: PncService,
        private prepservice: PrepService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.FertilityIntentionForm = this._formBuilder.group({
            lmp: new FormControl('', [Validators.required]),
            pregnant: new FormControl('', [Validators.required]),
            pregnancyPlanned: new FormControl('', [Validators.required]),
            breastFeeding: new FormControl('', [Validators.required]),
            onFamilyPlanning: new FormControl('', [Validators.required]),
            familyPlanningMethods: new FormControl('', [Validators.required]),
            planningToGetPregnant: new FormControl('', [Validators.required]),
            id_familyPlanning: new FormControl(),
            EDD: new FormControl(),
            fpMethodId: new FormControl()
        });

        // emit form to the stepper 
        this.notify.emit(this.FertilityIntentionForm);

        // set default form state
        this.FertilityIntentionForm.controls.familyPlanningMethods.disable({ onlySelf: true });
        this.FertilityIntentionForm.controls.pregnancyPlanned.disable({ onlySelf: true });
        this.FertilityIntentionForm.controls.onFamilyPlanning.disable({ onlySelf: true });
        this.FertilityIntentionForm.controls.familyPlanningMethods.disable({ onlySelf: true });
        this.FertilityIntentionForm.controls.planningToGetPregnant.disable({ onlySelf: true });
        this.FertilityIntentionForm.controls.EDD.disable({ onlySelf: true });

        const { yesnoOptions, fpMethods, planningPregnancy, pregnancyStatusOptions } = this.FertilityIntentionsOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.fpMethods = fpMethods;
        this.planningPregnancy = planningPregnancy;
        this.pregnancyStatusOptions = pregnancyStatusOptions;

        if (this.isEdit == 1) {
            this.loadPregnancyIndicator();
            this.loadPregnancyIntention();
            this.loadFamilyPlanning();
            this.loadFamilyPlanningMethod();
        }
    }

    public onLMPDateChange() {
        this.dateLMP = this.FertilityIntentionForm.controls.lmp.value;

        this.visitDate = new Date(localStorage.getItem('visitDate'));
        if (this.visitDate !== undefined && this.visitDate !== null) {
            this.minLMpDate = moment(moment(this.visitDate).subtract(42, 'weeks').format('')).toDate();

            if (moment(this.dateLMP).isBefore(this.minLMpDate)) {

                this.snotifyService.error('Current LMP Date CANNOT be More than 9 months before the VisitDate', 'Mother Profile',
                    this.notificationService.getConfig());
                this.FertilityIntentionForm.controls.lmp.setValue('');
                return false;
            }


        }


    }

    loadPregnancyIndicator(): void {
        this.prepservice.getPregnancyIndicator(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                if (res) {
                    this.FertilityIntentionForm.controls.lmp.setValue(res.lmp);
                    this.FertilityIntentionForm.controls.pregnancyPlanned.setValue(res.pregnancyPlanned);
                    this.FertilityIntentionForm.controls.planningToGetPregnant.setValue(res.planningToGetPregnant);
                    this.FertilityIntentionForm.controls.breastFeeding.setValue(res.breastFeeding);

                    // set pregnancy status
                    const pregnancyStatus = this.pregnancyStatusOptions.filter(obj => obj.itemId == res.pregnancyStatusId);
                    if (pregnancyStatus[0].displayName == 'Pregnant') {
                        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                        this.FertilityIntentionForm.controls.pregnant.setValue(yesOption[0].itemId);
                    } else {
                        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                        this.FertilityIntentionForm.controls.pregnant.setValue(noOption[0].itemId);
                    }
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadFamilyPlanningMethod(): void {
        this.pncservice.getFamilyPlanningMethod(this.patientId).subscribe(
            (res) => {
                // console.log(res);
                if (res.length > 0) {
                    res.forEach(element => {
                        this.FertilityIntentionForm.controls.familyPlanningMethods.setValue(element.fpMethodId);
                        this.FertilityIntentionForm.controls.fpMethodId.setValue(element.id);
                    });
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadFamilyPlanning(): void {
        this.pncservice.getFamilyPlanning(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                if (res.length > 0) {
                    res.forEach(element => {
                        if (element.patientMasterVisitId == this.patientMasterVisitId) {
                            this.FertilityIntentionForm.controls.onFamilyPlanning.setValue(element.familyPlanningStatusId);
                            this.FertilityIntentionForm.controls.id_familyPlanning.setValue(element.id);
                        }
                    });
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadPregnancyIntention(): void {

    }

    onClientFPSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.FertilityIntentionForm.controls.familyPlanningMethods.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.FertilityIntentionForm.controls.familyPlanningMethods.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.familyPlanningMethods.setValue('');
        }
    }

    onPregnancySelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.FertilityIntentionForm.controls.pregnancyPlanned.enable({ onlySelf: true });

            // disable
            this.FertilityIntentionForm.controls.onFamilyPlanning.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.familyPlanningMethods.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.planningToGetPregnant.disable({ onlySelf: true });
            // this.FertilityIntentionForm.controls.EDD.enable({ onlySelf: true });


            if (this.FertilityIntentionForm.controls.lmp.value !== null &&
                this.FertilityIntentionForm.controls.lmp.value !== undefined &&
                this.FertilityIntentionForm.controls.lmp.value !== '') {
                this.FertilityIntentionForm.controls.EDD.setValue(
                    moment(this.FertilityIntentionForm.controls.lmp.value
                        , 'DD-MM-YYYY').add(280, 'days').toDate());

            }
            // Reset values
            this.FertilityIntentionForm.controls.onFamilyPlanning.setValue('');
            this.FertilityIntentionForm.controls.familyPlanningMethods.setValue('');
            this.FertilityIntentionForm.controls.planningToGetPregnant.setValue('');
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.FertilityIntentionForm.controls.pregnancyPlanned.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.pregnancyPlanned.setValue('');

            this.FertilityIntentionForm.controls.EDD.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.EDD.setValue('');
            // enable
            this.FertilityIntentionForm.controls.onFamilyPlanning.enable({ onlySelf: true });
            // this.FertilityIntentionForm.controls.familyPlanningMethods.enable({ onlySelf: true });
            this.FertilityIntentionForm.controls.planningToGetPregnant.enable({ onlySelf: true });
        }
        else  {
            this.FertilityIntentionForm.controls.EDD.disable({ onlySelf: true });
            this.FertilityIntentionForm.controls.EDD.setValue('');
        }

    }
}
