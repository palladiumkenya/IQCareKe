import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { Resolve } from '@angular/router';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { ActivatedRouteSnapshot } from '@angular/router';
import { RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class InfantFeedingOptionsResolver implements Resolve<Observable<LookupItemView[]>>  {

    constructor(private _lookupItemService: LookupItemService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<LookupItemView[]> | Observable<Observable<LookupItemView[]>> | Promise<Observable<LookupItemView[]>> {
        return this._lookupItemService.getByGroupName('InfantFeeding');
    }

}
