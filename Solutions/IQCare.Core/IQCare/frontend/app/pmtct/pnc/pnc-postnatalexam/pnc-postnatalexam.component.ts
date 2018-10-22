import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-postnatalexam',
    templateUrl: './pnc-postnatalexam.component.html',
    styleUrls: ['./pnc-postnatalexam.component.css']
})
export class PncPostnatalexamComponent implements OnInit {
    PostNatalForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.PostNatalForm = this._formBuilder.group({
            breast: new FormControl('', [Validators.required]),
            uterus: new FormControl('', [Validators.required]),
            lochia: new FormControl('', [Validators.required]),
            postpartumhaemorrhage: new FormControl('', [Validators.required]),
            episiotomy: new FormControl('', [Validators.required]),
            c_section: new FormControl('', [Validators.required]),
            fistula_screening: new FormControl('', [Validators.required])
        });


        this.notify.emit(this.PostNatalForm);
    }

}
