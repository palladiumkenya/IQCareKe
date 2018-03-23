import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';

@Injectable()
export class DataService {
    private hasConsented = new BehaviorSubject<boolean>(false);
    private isPositive = new BehaviorSubject<boolean>(false);
    currentHasConsented = this.hasConsented.asObservable();
    currentIsPositive = this.isPositive.asObservable();

    constructor() {

    }

    updateHasConsented(consent: boolean) {
        this.hasConsented.next(consent);
    }

    updateIsPositive(isPositive: boolean) {
        this.isPositive.next(isPositive);
    }
}
