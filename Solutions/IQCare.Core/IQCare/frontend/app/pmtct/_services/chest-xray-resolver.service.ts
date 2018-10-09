import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/index';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {LookupItemService} from '../../shared/_services/lookup-item.service';

@Injectable({
  providedIn: 'root'
})
export class ChestXrayResolverService {

  constructor(private _lookupItemService: LookupItemService) {

  }
    public resolve(): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('sputumSmear');
    }
}
