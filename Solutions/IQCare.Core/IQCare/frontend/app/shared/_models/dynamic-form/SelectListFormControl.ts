import { FormControlBase } from "../dynamic-form/FormControlBase";

export class SelectlistFormControl extends FormControlBase<string> {
    controlType = 'selectlist';
    options : {key: string, value:string} [] = [];

    constructor(options : {} = {}){
      super(options);
      this.options = options['options'] || [];

    }
}