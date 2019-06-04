import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Injectable } from '@angular/core';

@Injectable()
export class PrepStatusResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private lookupItemService: LookupItemService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('PrEP_Status');
    }
}
