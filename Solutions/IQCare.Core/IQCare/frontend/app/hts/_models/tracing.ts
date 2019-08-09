import {FormControl, Validators} from '@angular/forms';

export class Tracing {
    tracingDate: string;
    mode: number;
    outcome: number;
    reasonNotContacted?: number;
    otherReasonSpecify?: string;
}
