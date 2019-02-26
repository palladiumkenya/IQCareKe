import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    private personHivStatusSource = new BehaviorSubject<string>('');
    currentHivStatus = this.personHivStatusSource.asObservable();

    constructor() { }

    public changeHivStatus(newHivStatus: string) {
        console.log(newHivStatus);
        this.personHivStatusSource.next(newHivStatus);
    }
}
