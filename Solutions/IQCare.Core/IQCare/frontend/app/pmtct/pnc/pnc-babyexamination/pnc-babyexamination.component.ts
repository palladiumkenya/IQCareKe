import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { PncService } from '../../_services/pnc.service';

@Component({
    selector: 'app-pnc-babyexamination',
    templateUrl: './pnc-babyexamination.component.html',
    styleUrls: ['./pnc-babyexamination.component.css']
})
export class PncBabyexaminationComponent implements OnInit {
    BabyExaminationForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    babyConditionOptions: LookupItemView[] = [];
    babyExaminationControls: LookupItemView[] = [];

    @Input('babyExaminationOptions') babyExaminationOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncService: PncService) { }

    ngOnInit() {
        this.BabyExaminationForm = this._formBuilder.group({
            babycondition: new FormControl('', [Validators.required]),
            breastfeeding: new FormControl('', [Validators.required])
        });

        const { yesnoOptions, babyConditionOptions, babyExaminationControls } = this.babyExaminationOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.babyConditionOptions = babyConditionOptions;
        this.babyExaminationControls = babyExaminationControls;

        this.notify.emit(this.BabyExaminationForm);

        if (this.isEdit) {
            this.getBabyExamination();
        }
    }

    public getBabyExamination(): void {
        this.pncService.getPncPostNatalExamBabyExaminationHistory(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                const babyconditionValue = result.filter(obj =>
                    obj.examId == this.babyExaminationControls.filter(x => x.itemName == 'babycondition')[0].itemId);
                const BreastfeedingValue = result.filter(obj =>
                    obj.examId == this.babyExaminationControls.filter(x => x.itemName == 'Breastfeeding')[0].itemId);

                this.BabyExaminationForm.get('babycondition').setValue(babyconditionValue[0].findingId);
                this.BabyExaminationForm.get('breastfeeding').setValue(BreastfeedingValue[0].findingId);
            }
        );
    }

}
