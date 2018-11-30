import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { PncService } from '../../_services/pnc.service';

@Component({
    selector: 'app-pnc-postnatalexam',
    templateUrl: './pnc-postnatalexam.component.html',
    styleUrls: ['./pnc-postnatalexam.component.css']
})
export class PncPostnatalexamComponent implements OnInit {
    PostNatalForm: FormGroup;
    breastOptions: LookupItemView[] = [];
    uterusOptions: LookupItemView[] = [];
    lochiaOptions: LookupItemView[] = [];
    postpartumhaemorrhageOptions: LookupItemView[] = [];
    episiotomyOptions: LookupItemView[] = [];
    cSectionSiteOptions: LookupItemView[] = [];
    fistulaScreeningOptions: LookupItemView[] = [];
    motherExaminationOptions: LookupItemView[] = [];

    @Input('postNatalExamOptions') postNatalExamOptions: any;
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private pncService: PncService) { }

    ngOnInit() {
        this.PostNatalForm = this._formBuilder.group({
            breast: new FormControl('', [Validators.required]),
            uterus: new FormControl('', [Validators.required]),
            lochia: new FormControl('', [Validators.required]),
            postpartumhaemorrhage: new FormControl('', [Validators.required]),
            episiotomy: new FormControl('', [Validators.required]),
            c_sectionsite: new FormControl('', [Validators.required]),
            fistula_screening: new FormControl('', [Validators.required])
        });

        const { breastOptions, uterusOptions,
            lochiaOptions, postpartumhaemorrhageOptions,
            episiotomyOptions, cSectionSiteOptions,
            fistulaScreeningOptions, motherExaminationOptions } = this.postNatalExamOptions[0];

        this.breastOptions = breastOptions;
        this.uterusOptions = uterusOptions;
        this.lochiaOptions = lochiaOptions;
        this.postpartumhaemorrhageOptions = postpartumhaemorrhageOptions;
        this.episiotomyOptions = episiotomyOptions;
        this.cSectionSiteOptions = cSectionSiteOptions;
        this.fistulaScreeningOptions = fistulaScreeningOptions;
        this.motherExaminationOptions = motherExaminationOptions;

        this.notify.emit(this.PostNatalForm);

        if (this.isEdit) {
            this.getPncPostNatalExam();
        }
    }

    public getPncPostNatalExam(): void {
        console.log(`edit`);
        this.pncService.getPncPostNatalExamBabyExaminationHistory(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                const breastValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'Breast')[0].itemId);
                const uterusValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'Uterus')[0].itemId);
                const lochiaValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'Lochia')[0].itemId);
                const postpartumhaemorrhageValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'PostPartumHaemorrhage')[0].itemId);
                const episiotomyValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'Episiotomy')[0].itemId);
                const c_sectionsiteValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'C_SectionSite')[0].itemId);
                const fistula_screeningValue = result.filter(obj =>
                    obj.examId == this.motherExaminationOptions.filter(x => x.itemName == 'Fistula_Screening')[0].itemId);

                this.PostNatalForm.get('breast').setValue(breastValue[0].findingId);
                this.PostNatalForm.get('uterus').setValue(uterusValue[0].findingId);
                this.PostNatalForm.get('lochia').setValue(lochiaValue[0].findingId);
                this.PostNatalForm.get('postpartumhaemorrhage').setValue(postpartumhaemorrhageValue[0].findingId);
                this.PostNatalForm.get('episiotomy').setValue(episiotomyValue[0].findingId);
                this.PostNatalForm.get('c_sectionsite').setValue(c_sectionsiteValue[0].findingId);
                this.PostNatalForm.get('fistula_screening').setValue(fistula_screeningValue[0].findingId);
            },
            (error) => {

            }
        );
    }
}
