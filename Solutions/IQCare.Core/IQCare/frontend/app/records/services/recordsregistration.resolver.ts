import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { RegistrationService } from './RecordsRegistrationService';
import { Injectable } from '@angular/core';


@Injectable()
export class RegistrationResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getRelGenderOptions();

    }
}

@Injectable()
export class MaritalStatusResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getMaritalStatusOptions();

    }
}

@Injectable()
export class EducationOppConsentResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getOppConsentEduOptions()

    }
}

@Injectable()
export class ConsentTypeResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(

        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getOppConsentType();

    }
}


@Injectable()
export class IdentifierTypeResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getIdentifierType();

    }
}




