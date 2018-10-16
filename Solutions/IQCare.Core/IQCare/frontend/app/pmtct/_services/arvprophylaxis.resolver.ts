import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Injectable } from '@angular/core';
@Injectable()
export class ARVProphylaxisResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private _lookupItemService: LookupItemService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('ARVProphylaxis');
    }
}
