import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-babyexamination',
    templateUrl: './pnc-babyexamination.component.html',
    styleUrls: ['./pnc-babyexamination.component.css']
})
export class PncBabyexaminationComponent implements OnInit {
    BabyExaminationForm: FormGroup;
    yesnoOptions: any[] = [];
    babyConditionOptions: any[] = [];

    @Input('babyExaminationOptions') babyExaminationOptions: any;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.BabyExaminationForm = this._formBuilder.group({
            babycondition: new FormControl('', [Validators.required]),
            breastfeeding: new FormControl('', [Validators.required])
        });

        const { yesnoOptions, babyConditionOptions } = this.babyExaminationOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.babyConditionOptions = babyConditionOptions;

        this.notify.emit(this.BabyExaminationForm);
    }

}
