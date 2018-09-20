import { NativeDateAdapter } from '@angular/material';
import * as moment from 'moment';
import { Platform } from '../../../../node_modules/@angular/cdk/platform';


export class AppDateAdapter extends NativeDateAdapter {

    /*constructor() {
        super('en-US', Platform);
    }*/

    format(date: Date, displayFormat: Object): string {
        if (displayFormat === 'input') {
            return moment(date).format('DD-MMM-YYYY');
        } else {
            return date.toDateString();
        }
    }
}

export const APP_DATE_FORMATS = {
    parse: {
        dateInput: { month: 'short', year: 'numeric', day: 'numeric' },
    },
    display: {
        dateInput: 'input',
        monthYearLabel: { year: 'numeric', month: 'numeric' },
        dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
        monthYearA11yLabel: { year: 'numeric', month: 'long' },
    }
};

