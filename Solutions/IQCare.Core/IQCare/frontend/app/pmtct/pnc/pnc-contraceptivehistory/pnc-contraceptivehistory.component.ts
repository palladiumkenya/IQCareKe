import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-pnc-contraceptivehistory',
    templateUrl: './pnc-contraceptivehistory.component.html',
    styleUrls: ['./pnc-contraceptivehistory.component.css']
})
export class PncContraceptivehistoryComponent implements OnInit {
    ContraceptiveHistoryForm: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder) { }

    ngOnInit() {
        this.ContraceptiveHistoryForm = this._formBuilder.group({
            onFamilyPlanning: new FormControl('', [Validators.required]),
            familyPlanningMethod: new FormControl('', [Validators.required]),
            pncExercisesGiven: new FormControl('', [Validators.required])
        });

        this.notify.emit(this.ContraceptiveHistoryForm);
    }

}
