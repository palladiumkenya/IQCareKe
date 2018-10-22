import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-babyexamination',
    templateUrl: './pnc-babyexamination.component.html',
    styleUrls: ['./pnc-babyexamination.component.css']
})
export class PncBabyexaminationComponent implements OnInit {
    BabyExaminationForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.BabyExaminationForm = this._formBuilder.group({
            babycondition: new FormControl('', [Validators.required]),
            breastfeeding: new FormControl('', [Validators.required])
        });

        this.notify.emit(this.BabyExaminationForm);
    }

}
