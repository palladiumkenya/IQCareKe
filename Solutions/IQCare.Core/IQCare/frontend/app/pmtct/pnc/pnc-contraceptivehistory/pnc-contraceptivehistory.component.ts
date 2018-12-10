import { NotificationService } from './../../../shared/_services/notification.service';
import { PncService } from './../../_services/pnc.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-pnc-contraceptivehistory',
    templateUrl: './pnc-contraceptivehistory.component.html',
    styleUrls: ['./pnc-contraceptivehistory.component.css']
})
export class PncContraceptivehistoryComponent implements OnInit {
    ContraceptiveHistoryForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    familyPlanningMethodOptions: LookupItemView[] = [];

    @Input('contraceptiveHistoryExercise') contraceptiveHistoryExercise: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncservice: PncService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.ContraceptiveHistoryForm = this._formBuilder.group({
            onFamilyPlanning: new FormControl('', [Validators.required]),
            familyPlanningMethod: new FormControl('', [Validators.required]),
            pncExercisesGiven: new FormControl('', [Validators.required]),
            id: new FormControl(''),
            fpMethodId: new FormControl('')
        });

        const { yesnoOptions, familyPlanningMethodOptions } = this.contraceptiveHistoryExercise[0];
        this.yesnoOptions = yesnoOptions;
        this.familyPlanningMethodOptions = familyPlanningMethodOptions;

        this.notify.emit(this.ContraceptiveHistoryForm);

        if (this.isEdit) {
            this.loadPncContraceptiviveHistory();
        }
    }

    loadPncContraceptiviveHistory(): void {
        this.pncservice.getFamilyPlanning(this.patientId).subscribe(
            (result) => {
                for (let i = 0; i < result.length; i++) {
                    if (result[i].patientMasterVisitId == this.patientMasterVisitId) {
                        this.ContraceptiveHistoryForm.get('onFamilyPlanning').setValue(result[i].familyPlanningStatusId);
                        this.ContraceptiveHistoryForm.get('id').setValue(result[i].id);
                        this.pncservice.getFamilyPlanningMethod(this.patientId).subscribe(
                            (res) => {
                                for (let j = 0; j < res.length; j++) {
                                    if (res[j].patientFPId == result[i].id) {
                                        this.ContraceptiveHistoryForm.get('familyPlanningMethod').setValue(res[j].fpMethodId);
                                        this.ContraceptiveHistoryForm.get('fpMethodId').setValue(res[j].id);
                                    }
                                }
                            },
                            (error) => {
                                this.snotifyService.success('Error fetching family planning methods ' + error, 'PNC Encounter',
                                    this.notificationService.getConfig());
                            },
                            () => { }
                        );
                    }
                }
            },
            (error) => {
                this.snotifyService.success('Error fetching family planning ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            },
            () => { }
        );
    }

    onFamilyPlanningChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.ContraceptiveHistoryForm.controls['familyPlanningMethod'].enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.ContraceptiveHistoryForm.controls['familyPlanningMethod'].disable({ onlySelf: false });
            this.ContraceptiveHistoryForm.controls['familyPlanningMethod'].setValue('');
        }
    }
}
