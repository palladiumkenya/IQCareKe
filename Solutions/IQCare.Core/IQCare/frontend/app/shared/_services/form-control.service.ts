import { Injectable } from '@angular/core';
import { FormControlBase } from '../_models/dynamic-form/FormControlBase';
import { FormControl, Validators, FormGroup } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})
export class FormControlService {

    constructor() { }

    toFormGroup(formControls: FormControlBase<any>[]) {
        const formGroup: any = {};
        formControls.forEach(control => {
            if (control === undefined) {
                return;
            }

            formGroup[control.key] =
                new FormControl({ value: control.value || '', disabled: control.disabled }, Validators.pattern(control.pattern));
        });
        return new FormGroup(formGroup);

    }
}
