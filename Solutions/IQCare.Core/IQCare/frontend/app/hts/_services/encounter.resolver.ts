import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {EncounterService} from './encounter.service';
import {Injectable} from '@angular/core';
import {LookupItemView} from '../../shared/_models/LookupItemView';

@Injectable()
export class EncounterResolver implements Resolve<Observable<any[]>> {
    constructor(private encounterService: EncounterService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<LookupItemView[]> {
        return this.encounterService.getCustomOptions();
    }
}
