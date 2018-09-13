import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Observable } from 'rxjs/index';
import { Resolve } from '@angular/router';
import { Injectable } from '@angular/core';
import { LookupItemService } from '../../shared/_services/lookup-item.service';

@Injectable()
export class MotherStateResolver implements Resolve<Observable<LookupItemView[]>> {
    /**
     *
     */
    constructor(private _lookupItemService: LookupItemService) {

    }

    public resolve(): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('MotherState');
    }
}
