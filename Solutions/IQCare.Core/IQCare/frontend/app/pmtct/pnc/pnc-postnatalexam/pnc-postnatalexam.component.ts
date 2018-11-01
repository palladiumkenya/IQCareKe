import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

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

    @Input('postNatalExamOptions') postNatalExamOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

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
            fistulaScreeningOptions } = this.postNatalExamOptions[0];

        this.breastOptions = breastOptions;
        this.uterusOptions = uterusOptions;
        this.lochiaOptions = lochiaOptions;
        this.postpartumhaemorrhageOptions = postpartumhaemorrhageOptions;
        this.episiotomyOptions = episiotomyOptions;
        this.cSectionSiteOptions = cSectionSiteOptions;
        this.fistulaScreeningOptions = fistulaScreeningOptions;

        this.notify.emit(this.PostNatalForm);
    }

}
