import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/Observable';
@Injectable()
export class NavigationService {
    private profile: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    get patientprofile() {
        return this.profile.asObservable();
    }
    constructor(private router: Router) {}


    ShowProfile() {

        this.profile.next(true);
                
    }

    RegistrationProfile() {
        this.profile.next(false);
      
    }
}
