import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs/index';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LookupItemService } from '../../shared/_services/lookup-item.service';

@Injectable()
export class HeiMotherRegimenResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private _lookupItemService: LookupItemService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('PMTCTHEIMotherRegimen');
    }
}
