import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { RecordsService } from './records.service';

@Injectable()
export class ContactCategoryResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private recordsService: RecordsService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.recordsService.getContactCategory();
    }
}
