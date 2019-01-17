import { FormControlBase } from "../dynamic-form/FormControlBase";
import { extend } from "webdriver-js-extender";

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

export class CheckboxFormControl extends FormControlBase<boolean>
{
    controlType = 'checkbox';
    type : boolean;

    constructor(options : {} = {}){
        super(options);
        this.type = options['type'] || '';
    }
}