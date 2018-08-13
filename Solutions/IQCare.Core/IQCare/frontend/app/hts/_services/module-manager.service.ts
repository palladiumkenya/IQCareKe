
import {throwError as observableThrowError, Observable} from 'rxjs';

import {catchError} from 'rxjs/operators';
import { Injectable } from '@angular/core';

import {HttpClient, HttpErrorResponse} from '@angular/common/http';



import { Module } from '../_models/module';

@Injectable()
export class ModuleManagerService {
  private _url: string = './api/modules';
  private _http: HttpClient;

  public constructor(http: HttpClient) {
      this._http = http;
  }

  public getModules(): Observable<Module[]> {
      return this._http.get<Module[]>(this._url).pipe(
          catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    if (err.status === 404) {
        return observableThrowError('no record(s) found');
    }
    return observableThrowError(err.error);
  }

}
