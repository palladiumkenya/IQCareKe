import { LookupItemView } from '../../shared/_models/LookupItemView';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class ExitReasonsResolver implements Resolve<Observable<any[]>> {
    /**
     *
     */

    constructor(private lookupItemService: LookupItemService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.lookupItemService.getByGroupName('PrepCareEnd');


    }
}

