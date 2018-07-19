import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {FamilyService} from '../_services/family.service';
import {Injectable} from '@angular/core';

@Injectable()
export class FamilyScreeningResolver implements Resolve<Observable<LookupItemView[]>> {
    constructor(private familyService: FamilyService) {
        
    }
    
    public resolve(route: ActivatedRouteSnapshot,
                   state: RouterStateSnapshot): Observable<LookupItemView[]> {
        return this.familyService.getCustomOptions();
    }
}
