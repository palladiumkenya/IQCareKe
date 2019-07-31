import { PersonHomeService } from './person-home.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class HTSEncounterHistoryResolver implements Resolve<Observable<any[]>>
{

    public personId = 0;
    constructor(private personhomeService: PersonHomeService) {
    }


    public resolve(
        route: ActivatedRouteSnapshot, state: RouterStateSnapshot
    ): Observable<any[]> {
        this.personId = route.params['id'];
        return this.personhomeService.getAllHTSEncounterBypersonId(this.personId);
    }
}