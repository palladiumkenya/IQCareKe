import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs/index';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Injectable } from '@angular/core';
@Injectable()
export class DeliveryModeResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private _lookupItemService: LookupItemService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('DeliveryMode');
    }
}