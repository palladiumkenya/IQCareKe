import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs';
import { Resolve } from '@angular/router';
import { Injectable } from '@angular/core';
import { LookupItemService } from '../../shared/_services/lookup-item.service';

@Injectable()
export class ScreenedForSTIResolver implements Resolve<Observable<LookupItemView[]>> {

    /**
     *
     */
    constructor(private lookupItemService: LookupItemService) {
    }

    public resolve(): Observable<LookupItemView[]> {
        return this.lookupItemService.getByGroupName('ScreenedForSTI');
    }
}
