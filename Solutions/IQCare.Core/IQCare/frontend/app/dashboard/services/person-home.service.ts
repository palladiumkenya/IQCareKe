import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import {environment} from '../../../environments/environment';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {PersonView} from '../../records/_models/personView';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class PersonHomeService {
    private API_URL = environment.API_URL;
    private _url = '/api/PatientServices/GetPatientByPersonId';
    public person: PersonView;
  constructor(private http: HttpClient,
              private errorHandler: ErrorHandlerService) { }

    public getPatientByPersonId(personId: Number): Observable<PersonView> {
        return this.http.get<PersonView>(this.API_URL + '' + this._url + '/' + personId ).pipe(
            tap(getPatientByPersonId => this.errorHandler.log('get ' + personId + 'options by Name')),
            catchError(this.errorHandler.handleError<PersonView>('getPatientByPersonId', ), )
        );
    }
}
