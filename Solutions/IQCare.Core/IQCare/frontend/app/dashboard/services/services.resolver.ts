import { PersonHomeService } from './person-home.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class ServicesResolver implements Resolve<Observable<any[]>> {
    /**
     *
     */
    

    constructor(private personhomeService: PersonHomeService) {
    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {        
        return this.personhomeService.getAllServices();
    }
}

