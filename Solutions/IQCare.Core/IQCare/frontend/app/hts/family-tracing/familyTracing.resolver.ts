import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {Injectable} from '@angular/core';
import {FamilyService} from '../_services/family.service';

@Injectable()
export class FamilyTracingResolver implements Resolve<Observable<LookupItemView[]>> {
    constructor(private familyService: FamilyService) {

    }

    public resolve(route: ActivatedRouteSnapshot,
                   state: RouterStateSnapshot): Observable<LookupItemView[]> {
        return this.familyService.getFamilyTracingOptions();
    }
}
