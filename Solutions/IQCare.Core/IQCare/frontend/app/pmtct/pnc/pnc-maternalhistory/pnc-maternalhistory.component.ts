import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-pnc-maternalhistory',
    templateUrl: './pnc-maternalhistory.component.html',
    styleUrls: ['./pnc-maternalhistory.component.css']
})
export class PncMaternalhistoryComponent implements OnInit {
    MaternalHistoryForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    maxDate: Date;

    constructor(private _formBuilder: FormBuilder) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.MaternalHistoryForm = this._formBuilder.group({
            dateofdelivery: new FormControl('', [Validators.required]),
            modeofdelivery: new FormControl('', [Validators.required])
        });


        this.notify.emit(this.MaternalHistoryForm);
    }

}
