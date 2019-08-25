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

    private motherIdObservable = new BehaviorSubject<number>(0);
    motherId = this.motherIdObservable.asObservable();

    constructor() { }

    public changeHivStatus(newHivStatus: string) {
        this.personHivStatusSource.next(newHivStatus);
    }

    public labHasBeenCompleted(labDone: boolean) {
        this.labDoneObservable.next(labDone);
    }

    public motherHasBeenSet(motherId: number) {
        this.motherIdObservable.next(motherId);
    }
}
