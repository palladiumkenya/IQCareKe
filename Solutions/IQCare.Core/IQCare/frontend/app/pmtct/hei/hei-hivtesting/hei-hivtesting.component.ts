import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-hei-hivtesting',
    templateUrl: './hei-hivtesting.component.html',
    styleUrls: ['./hei-hivtesting.component.css']
})
export class HeiHivtestingComponent implements OnInit {
    HivTestingForm: FormGroup;

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.HivTestingForm = this._formBuilder.group({
            testtype: new FormControl('', [Validators.required]),
            dateofsamplecollection: new FormControl('', [Validators.required]),
            result: new FormControl('', [Validators.required]),
            dateresultscollected: new FormControl('', [Validators.required]),
            comments: new FormControl(''),
        });
    }
}
