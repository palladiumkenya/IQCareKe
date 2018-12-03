import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Injectable } from '../../../../node_modules/@angular/core';
import { RecordsService } from './records.service';

@Injectable()
export class OccupationResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private recordsService: RecordsService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.recordsService.getOccupationOptions();
    }
}
