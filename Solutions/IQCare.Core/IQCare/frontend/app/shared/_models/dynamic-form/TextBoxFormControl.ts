import { FormControlBase } from "../dynamic-form/FormControlBase";

export class TextboxFormControl extends FormControlBase<string>{
    controlType = 'textbox';
    type : string;

    constructor(options : {} = {}){
        super(options);
        this.type = options['type'] || '';
    }
}


export class NumericTextboxFormControl extends FormControlBase<number>{
    controlType = 'numeric';
    type : number;

    constructor(options :{} = {}){
        super(options);
        this.type = options['type'] || '';
    }
}