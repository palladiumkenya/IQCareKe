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
    }

    ngOnInit() {
    }

}
