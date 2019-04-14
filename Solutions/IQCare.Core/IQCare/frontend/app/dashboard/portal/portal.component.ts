import { Component, OnInit } from '@angular/core';
import * as AppState from '../../shared/reducers/app.states';
import { Store } from '@ngrx/store';

@Component({
    selector: 'app-portal',
    templateUrl: './portal.component.html',
    styleUrls: ['./portal.component.css']
})
export class PortalComponent implements OnInit {

    constructor(private store: Store<AppState>) {
        store.dispatch(new AppState.ClearState());
        localStorage.setItem('selectedService', '');
    }

    ngOnInit() {
        localStorage.removeItem('appQueueMenu');
    }
    checkQueue(): boolean {
        let appQueue: number;
        appQueue = parseInt(localStorage.getItem('appQueue'), 10);
        if (appQueue == parseInt('1', 10)) {
           

            return true;

        } else {
            localStorage.removeItem('appQueueMenu');

            return false;
        }
    }
}
