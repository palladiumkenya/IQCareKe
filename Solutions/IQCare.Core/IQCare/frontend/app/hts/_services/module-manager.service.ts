import { Injectable } from '@angular/core';

import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Module } from '../_models/module';

@Injectable()
export class ModuleManagerService {
  private _url: string = './api/modules';
  private _http: HttpClient;

  public constructor(http: HttpClient) {
      this._http = http;
  }

  public getModules(): Observable<Module[]> {
      return this._http.get<Module[]>(this._url)
          .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse) {
    if (err.status === 404) {
        return Observable.throw('no record(s) found');
    }
    return Observable.throw(err.error);
  }

}
