import {Injectable} from '@angular/core';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {LookupItemView} from '../../../shared/_models/LookupItemView';
import {Observable} from 'rxjs/index';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class VisitOptionsResolverService implements Resolve<Observable<LookupItemView[]>> {

    constructor(private _lookupItemService: LookupItemService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('ANCVisitType');
    }
}
