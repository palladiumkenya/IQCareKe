import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { RegistrationService } from './RecordsRegistrationService';
import { Injectable } from '@angular/core';


@Injectable()
export class RelationshipResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getRelOptions();

    }
}


@Injectable()
export class OccupationResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getOppOptions();

    }
}


@Injectable()
export class GenderResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getGenderOptions();

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
export class OppEducationResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getOppEducationOptions()

    }
}

@Injectable()
export class OppConsentResolver implements Resolve<Observable<any[]>> {
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.registrationService.getOppConsentOptions()

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
@Injectable()
export class GetPersonDetailsResolver implements Resolve<Observable<any[]>>{
    constructor(private registrationService: RegistrationService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        
        return this.registrationService.getPersonDetails(parseInt(localStorage.getItem("personId")));

    }


}




