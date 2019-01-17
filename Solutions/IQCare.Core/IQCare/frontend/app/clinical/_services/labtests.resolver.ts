import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs/index';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { LaborderService } from '../../clinical/_services/laborder.service';

@Injectable()
export class LabTestsResolver implements Resolve<Observable<LookupItemView[]>> {
    
    constructor(private _laborderService: LaborderService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this._laborderService.getConfiguredLabTests();
    }
}
