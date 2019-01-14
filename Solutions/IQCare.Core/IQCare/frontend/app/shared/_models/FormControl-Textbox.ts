import { FormControlBase } from "./FormControlBase";

export class TextboxFormControl extends FormControlBase<string> {
    controlType = 'textbox';
    type: string;

    constructor(options: {} = {}) {
        super(options);
        this.type = options['type'] || '';
    }
}
