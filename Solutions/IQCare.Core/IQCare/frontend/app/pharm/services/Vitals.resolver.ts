

import { PharmacyService } from './pharmacy.service';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class PersonCurrentVitalsResolver implements Resolve<Observable<any[]>>
{

    public personId = 0;
    constructor(private pharmacyservice: PharmacyService) {
    }


    public resolve(
        route: ActivatedRouteSnapshot, state: RouterStateSnapshot
    ): Observable<any[]> {
        this.personId = route.params['personId'];
        return this.pharmacyservice.GetCurrentPatientVitalsInfo(this.personId);
    }
}



