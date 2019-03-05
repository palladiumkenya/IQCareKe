import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { FormDetailsService } from './formdetails.service';
import { Injectable } from '@angular/core';



@Injectable()
export class FormDetailResolver implements Resolve<Observable<any[]>>{
    constructor(private formdetailservice: FormDetailsService) {

    }

    public resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<any[]> {

        let number: any = route.params['reportingFormId'];
        

        return this.formdetailservice.getFormDetails(number);
    }
}



