import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '../../../../node_modules/@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { Injectable } from '../../../../node_modules/@angular/core';
import { RecordsService } from './records.service';

@Injectable()
export class MaritalStatusResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private recordsService: RecordsService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.recordsService.getMaritalStatusOptions();
    }
}
