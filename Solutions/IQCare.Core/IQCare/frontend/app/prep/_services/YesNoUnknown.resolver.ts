import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Injectable } from '@angular/core';

@Injectable()
export class YesNoUnknownResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private lookupItemService: LookupItemService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('YesNoUnknown');
    }
}
