import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { QueueDetailsService } from './queue.service';

@Injectable()
export class LookupItemsListResolver implements Resolve<Observable<any[]>>{

    constructor(private queueservice: QueueDetailsService) {

    }
    public resolve(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any[]> {
        return this.queueservice.getLookupList();
    }
}