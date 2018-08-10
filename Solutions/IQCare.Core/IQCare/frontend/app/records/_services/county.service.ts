import { ErrorHandlerService } from './../../shared/_services/errorhandler.service';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '../../../../node_modules/@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from '../../../../node_modules/rxjs';
import { County } from '../_models/county';
import { tap, catchError } from '../../../../node_modules/rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class CountyService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {

    }

    getCounties(): Observable<County[]> {
        return this.http.get<County[]>(this.API_URL + '/api/county/getcounties').pipe(
            tap((getCounties: any) => this.errorHandler.log(`get counties`)),
            catchError(this.errorHandler.handleError<any>('getCounties'))
        );
    }

    getSubCounties(countyId: number): Observable<County[]> {
        return this.http.get<County[]>(this.API_URL + '/api/county/getSubCounties/' + countyId).pipe(
            tap((getSubCounties: any) => this.errorHandler.log(`get sub counties`)),
            catchError(this.errorHandler.handleError<any>('getSubCounties'))
        );
    }

    getWards(subCountyId: number): Observable<County[]> {
        return this.http.get<County[]>(this.API_URL + '/api/county/getWards/' + subCountyId).pipe(
            tap((getWards: any) => this.errorHandler.log(`get wards`)),
            catchError(this.errorHandler.handleError<any>('getWards'))
        );
    }
}
