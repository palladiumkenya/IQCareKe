import { Observable } from 'rxjs/index';
import { Injectable } from '@angular/core';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { RouterStateSnapshot, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';

@Injectable()
export class MotherReceiveDrugsResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private _lookupItemService: LookupItemService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('PMTCTHEIMotherReceiveDrugs');
    }
}
