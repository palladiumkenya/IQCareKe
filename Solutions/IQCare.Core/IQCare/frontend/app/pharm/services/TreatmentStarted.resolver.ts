import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Injectable } from '@angular/core';
import { PharmacyService } from '../services/pharmacy.service';



@Injectable()
export class TreatmentStartedResolver implements Resolve<Observable<any[]>> {
    /**
     *
     */
    public patientId = 0;
    constructor(private pharmacyService: PharmacyService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {

        this.patientId = route.params['patientId'];
        return this.pharmacyService.hasPatientStartedTreatment(this.patientId);
    }
}
