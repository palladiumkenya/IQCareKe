import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { RecordsService } from './records.service';

@Injectable()
export class PersonNHIFIdentifiersResolver implements Resolve<Observable<any>> {
    /**
     *
     */
    constructor(private recordsService: RecordsService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        return this.recordsService.getPersonNHIFIdentifiers();
    }
}
