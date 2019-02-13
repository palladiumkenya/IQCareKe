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
     formControls.forEach(control =>
     {
        if(control === undefined)
            return;
        formGroup[control.key] =  new FormControl({value: control.value || '', disabled:control.disabled },Validators.pattern(control.pattern));
                                                      
     });
     return new FormGroup(formGroup);

  }
}
