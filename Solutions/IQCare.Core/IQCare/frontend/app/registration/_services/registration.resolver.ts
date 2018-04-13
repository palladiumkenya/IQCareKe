import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {RegistrationService} from './registration.service';
import {Injectable} from '@angular/core';

@Injectable()
export class RegistrationResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getRegistrationOptions();
    }
}
