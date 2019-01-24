import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Injectable } from '../../../../node_modules/@angular/core';
import { RecordsService } from './records.service';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '../../../../node_modules/@angular/router';

@Injectable()
export class RelationshipResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private recordsService: RecordsService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.recordsService.getRelationshipOptions();
    }
}
