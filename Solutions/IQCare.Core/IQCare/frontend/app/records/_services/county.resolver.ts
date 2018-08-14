import { CountyService } from './county.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { County } from '../_models/county';

@Injectable()
export class CountyResolver implements Resolve<Observable<County[]>> {
    /**
     *
     */
    constructor(private countyService: CountyService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<County[]> {
        return this.countyService.getCounties();
    }
}
