import { Injectable } from '@angular/core';
import { FormControlBase } from '../_models/dynamic-form/FormControlBase';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { skip } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FormControlService {

  constructor() { }

  toFormGroup(formControls : FormControlBase<any>[]) {
     let formGroup : any = {};
     console.log('Form Controls at toFromGroup method >> ' + formControls.length)
     
     formControls.forEach(control =>
     {
        if(control === undefined)
            return;
            console.log(control.disabled +' Disabled')
        formGroup[control.key] = control.required ? new FormControl({value: control.value || '', disabled:control.disabled },Validators.required) 
                                                      : new FormControl({value :control.value || '',disabled:control.disabled});
     });
     return new FormGroup(formGroup);

  }
}
