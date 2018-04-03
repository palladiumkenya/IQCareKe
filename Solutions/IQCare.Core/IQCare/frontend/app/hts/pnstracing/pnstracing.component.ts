import {Component, NgZone, OnInit} from '@angular/core';
import {PnstracingService} from '../_services/pnstracing.service';
import {PnsTracing} from '../_models/pnstracing';
import {ActivatedRoute, Router} from '@angular/router';
import {Store} from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';

@Component({
  selector: 'app-pnstracing',
  templateUrl: './pnstracing.component.html',
  styleUrls: ['./pnstracing.component.css']
})
export class PnsTracingComponent implements OnInit {
    pnsTracing: PnsTracing;
    yesNoOptions: any[];
    tracingModeOptions: any[];
    pnsTracingOutcome: any[];

    constructor(private pnsTracingService: PnstracingService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private store: Store<AppState>) { }

    ngOnInit() {
        this.pnsTracing = new PnsTracing();
        this.yesNoOptions = [];
        this.tracingModeOptions = [];
        this.pnsTracingOutcome = [];

        this.getTracingOptions();
    }

    public getTracingOptions() {
        this.pnsTracingService.getTracingOptions().subscribe(data => {
            console.log(data);
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'TracingMode') {
                    this.tracingModeOptions = options[i].value;
                } else if (options[i].key == 'PnsTracingOutcome') {
                    this.pnsTracingOutcome = options[i].value;
                }
            }
        });
    }

    onSubmit() {
        this.pnsTracing.PersonId = JSON.parse(localStorage.getItem('partnerId'));
        this.pnsTracing.UserId = 1; // JSON.parse(localStorage.getItem('partnerId'));

        console.log(this.pnsTracing);

        this.pnsTracingService.addPnsTracing(this.pnsTracing).subscribe(data => {
            console.log(data);
            this.store.dispatch(new Consent.IsPnsTracingDone(true));
            this.zone.run(() => { this.router.navigate(['/hts/pns'], {relativeTo: this.route }); });
        }, err => {
            console.log(err);
        });
    }
}
