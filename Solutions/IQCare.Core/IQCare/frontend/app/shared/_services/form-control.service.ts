import { Injectable } from '@angular/core';
import { FormControlBase } from '../_models/dynamic-form/FormControlBase';
import { FormControl, Validators, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormControlService {

  constructor() { }

  toFormGroup(formControls : FormControlBase<any>[]) {
     let formGroup : any = {};

     formControls.forEach(control =>
     {
        formGroup[control.key] = control.required ? new FormControl(control.value || '',Validators.required) 
                                                        : new FormControl(control.value || '');
     });
     return new FormGroup(formGroup);

  }
}
