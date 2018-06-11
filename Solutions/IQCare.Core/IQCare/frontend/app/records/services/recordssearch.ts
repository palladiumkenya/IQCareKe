import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import { catchError, tap } from 'rxjs/operators';
import { Search ,SearchList} from '../models/search';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class SearchService {
    private API_URL = environment.API_URL;
    private _url = '/records/api/Register/search';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

        public searchPerson(personsearch: Search): Observable<any[]> {
            return this.http.get<any[]>(this.API_URL + this._url + '?identificationNumber=' + personsearch.identifierValue +
                '&firstName=' + personsearch.firstName + '&middleName=' + personsearch.midName + '&lastName=' + personsearch.lastName, httpOptions).pipe(
                tap((searchClient: any) => this.errorHandler.log(`search client`)),
                catchError(this.errorHandler.handleError<any>('searchClients'))
                );
        }
            
            
  }

