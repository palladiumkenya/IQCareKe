import { FormControlBase } from './../_models/FormControlBase';
import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-custom-form',
    templateUrl: './custom-form.component.html',
    styleUrls: ['./custom-form.component.css']
})
export class CustomFormComponent implements OnInit {
    @Input() question: FormControlBase<any>;
    @Input() form: FormGroup;

    constructor() { }

    ngOnInit() {
    }

    get isValid() { return this.form.controls[this.question.key].valid; }

}
