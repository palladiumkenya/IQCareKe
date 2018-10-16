import { Injectable } from '@angular/core';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {Observable} from 'rxjs/index';
import {LookupItemView} from '../../shared/_models/LookupItemView';

@Injectable({
  providedIn: 'root'
})
export class GeneXpertResolverService {

  constructor(private _lookupItemService: LookupItemService) {

  }

    public resolve(): Observable<LookupItemView[]> {
        return this._lookupItemService.getByGroupName('GeneXpert');
    }
}
