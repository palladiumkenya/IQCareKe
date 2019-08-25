import { PersonHomeService } from './person-home.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { mergeMap } from 'rxjs/operators';

@Injectable()
export class CareendDetailsResolver implements Resolve<Observable<any[]>>
{
    public personId = 0;
    public patientId = 0;
    constructor(private personService: PersonHomeService) {

    }

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
        : Observable<any[]> {

        this.personId = route.params['id'];
        return this.personService.getPatientByPersonId(this.personId).pipe(mergeMap(
            res => this.personService.getPatientCareEndedHistory(res['patientId'])
        ));



    }
}