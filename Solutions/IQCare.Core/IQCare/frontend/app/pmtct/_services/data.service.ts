import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    private personHivStatusSource = new BehaviorSubject<string>('');
    currentHivStatus = this.personHivStatusSource.asObservable();

    private labDoneObservable = new BehaviorSubject<boolean>(false);
    labDone = this.labDoneObservable.asObservable();

    constructor() { }

    public changeHivStatus(newHivStatus: string) {
        this.personHivStatusSource.next(newHivStatus);
    }

    public labHasBeenCompleted(labDone: boolean) {
        this.labDoneObservable.next(labDone);
    }
}
