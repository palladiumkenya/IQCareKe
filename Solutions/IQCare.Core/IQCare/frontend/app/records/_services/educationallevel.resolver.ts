import { Observable } from 'rxjs';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '../../../../node_modules/@angular/router';
import { RecordsService } from './records.service';
import { Injectable } from '../../../../node_modules/@angular/core';

@Injectable()
export class EducationLevelResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private recordsService: RecordsService) {
    }
    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.recordsService.getEducationLevelOptions();
    }
}
