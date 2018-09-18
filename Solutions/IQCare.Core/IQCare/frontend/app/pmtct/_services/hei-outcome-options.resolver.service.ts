import { Injectable } from '@angular/core';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { LookupItemView } from '../../shared/_models/LookupItemView';

@Injectable({
  providedIn: 'root'
})
export class HeiOutcomeOptionsResolver implements Resolve<Observable<LookupItemView[]>> {

  public  resolve(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): Observable<LookupItemView[]> | 
    Observable<Observable<LookupItemView[]>> | 
    Promise<Observable<LookupItemView[]>> {
      return this._lookupItemService.getByGroupName('OutcomeAt24Months');
    }

  constructor(private _lookupItemService: LookupItemService) { }
}
