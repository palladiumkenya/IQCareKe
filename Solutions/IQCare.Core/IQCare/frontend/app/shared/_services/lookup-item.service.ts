import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import {Observable} from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {environment} from "../../../environments/environment";
import {ErrorHandlerService} from "./errorhandler.service";
import {LookupItemView} from "../_models/LookupItemView";

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LookupItemService {

    private API_URL = environment.API_URL;
    private _url = '/api/HtsEncounter';
    private _lookupurl = '/api/lookup';
    private lookup = '/api/Lookup/getCustomOptions';

  constructor(private http: HttpClient,
              private errorHandler: ErrorHandlerService) { }

    public getByGroupName(groupName: string): Observable<LookupItemView[]> {
        return this.http.get<LookupItemView[]>(this.API_URL + '/api/Lookup/GetbyGroupName/' + groupName ).pipe(
            tap(getByGroupName => this.errorHandler.log('get '+ groupName + 'options by Name')),
            catchError(this.errorHandler.handleError<LookupItemView[]>('getbyGroupName', []), )
        );
    }
}
