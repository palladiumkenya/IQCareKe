import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { tap, catchError } from '../../../../node_modules/rxjs/operators';
import { Observable } from '../../../../node_modules/rxjs';
import { Search } from '../_models/search';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class SearchService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {

    }

    searchClient(clientSearch: Search): Observable<any[]> {

        return this.http.get<any[]>(this.API_URL + '/records/api/Register/searchpersonlist'
            + '?IdentifierValue=' + clientSearch.identifierValue + '&FullName=' + clientSearch.fullName).pipe(
                tap((searchClient: any) => this.errorHandler.log(`search client`)),
                catchError(this.errorHandler.handleError<any>('searchClient'))
            );
    }

}
