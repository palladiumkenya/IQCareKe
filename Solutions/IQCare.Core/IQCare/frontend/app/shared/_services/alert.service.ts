import { Injectable } from '@angular/core';
import {Subject, Observable} from 'rxjs';
import {Alert, AlertType} from '../_models/alert';
import {NavigationStart, Router} from '@angular/router';

@Injectable()
export class AlertService {
    private subject = new Subject<Alert>();
    private keepAfterRouteChange = false;

    constructor(private router: Router) {
        // clear alert message on route change unless 'keepAfterRouteChange' flag is true
        router.events.subscribe(event => {
             if (event instanceof NavigationStart) {
                if (this.keepAfterRouteChange) {
                    // only keep for a single route change
                    this.keepAfterRouteChange = false;
                } else {
                    // clear alert messages
                    // this.clear();
                }
             }
        });
    }

    public getAlert(): Observable<any> {
        return this.subject.asObservable();
    }

    public success(message: string, keepAfterRouteChange = false) {
        this.alert(AlertType.Success, message, keepAfterRouteChange);
    }

    public error(message: string, keepAfterRouteChange = false) {
        this.alert(AlertType.Error, message, keepAfterRouteChange);
    }

    public info(message: string, keepAfterRouteChange = false) {
        this.alert(AlertType.Info, message, keepAfterRouteChange);
    }

    public warn(message: string, keepAfterRouteChange = false) {
        this.alert(AlertType.Warning, message, keepAfterRouteChange);
    }

    public alert(type: AlertType, message: string, keepAfterRouteChange = false) {
        console.log('pushing to subject');
        console.log(keepAfterRouteChange);
        this.keepAfterRouteChange = keepAfterRouteChange;
        this.subject.next(<Alert>{ type: type, message: message });
    }

    public clear() {
        // clear alerts
        this.subject.next();
    }
}
