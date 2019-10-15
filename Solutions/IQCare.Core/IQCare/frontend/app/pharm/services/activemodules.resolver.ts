import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Injectable } from '@angular/core';
import { PharmacyService } from '../services/pharmacy.service';



@Injectable()
export class ActiveModulesResolver implements Resolve<Observable<any[]>> {
    /**
     *
     */
    constructor(private pharmservice: PharmacyService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {

        let locationId: number;
        locationId = parseInt(JSON.parse(localStorage.getItem('appLocationId')), 10);

        return this.pharmservice.getActiveFacilityModules(locationId);
    }
}