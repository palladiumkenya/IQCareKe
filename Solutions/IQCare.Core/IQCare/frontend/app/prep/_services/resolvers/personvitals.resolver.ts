

import { PrepService} from '../prep.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class PersonCurrentVitalsResolver implements Resolve <Observable<any[]>>
{
    
    public personId = 0;
    constructor(private prepService: PrepService) {
    }
        
    
    public resolve(
            route: ActivatedRouteSnapshot, state: RouterStateSnapshot
        ): Observable<any[]> {
            this.personId = route.params['personId'];
           return this.prepService.GetCurrentPatientVitalsInfo(this.personId);
        }
    }


