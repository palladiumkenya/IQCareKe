import { LookupItemService } from './../../../shared/_services/lookup-item.service';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable, Inject } from '@angular/core';

@Injectable()
export class PrepAdherenceResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('PrepAdherence');
    }
}




@Injectable()
export class AdherenceAssessmentReasonsResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('AdherenceAssessmentReasons');
    }

}

@Injectable()
export class RefillPrepStatusResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('RefillPrepStatus');
    }
}



@Injectable()
export class PrepDiscontinueReasonResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('PrepDiscontinueReason');
    }
}





@Injectable()
export class AdherenceCounsellingResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('AdherenceCounselling');
    }
}




@Injectable()
export class AppointmentGivenResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('AppointmentGiven');
    }
}


@Injectable()
export class PrepAppointmentReasonResolver implements Resolve<Observable<LookupItemView[]>> {

    constructor(private lookupItemService: LookupItemService) {

    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('PrepAppointmentReason');
    }
}