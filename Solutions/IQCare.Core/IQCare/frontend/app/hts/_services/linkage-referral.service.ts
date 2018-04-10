import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import 'rxjs/add/observable/throw';
import {Referral} from '../_models/referral';
import {Tracing} from '../_models/tracing';
import {Linkage} from '../_models/linkage';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class LinkageReferralService {
    private API_URL = environment.API_URL;
    private _lookupurl = '/api/lookup/htsTracingOptions';
    private url = '/api/Referral';

    constructor(private http: HttpClient) { }

    public getReferralReasons() {
        const options = JSON.stringify(['ReferralReason']);

        return this.http.post<any[]>(this.API_URL + '/api/Lookup/getCustomOptions', options, httpOptions).pipe(
            tap(getReferralReasons => this.log('get referral reasons')),
            catchError(this.handleError<any[]>('getReferralReasons'))
        );
    }

    public filterFacilities(filterString: string) {
        return this.http.get<any[]>(this.API_URL + '/api/Lookup/searchFacilityList?searchString=' + filterString).pipe(
            tap(filterFacilities => this.log('fetched filtered facilities')),
            catchError(this.handleError<any[]>('filterFacilities'))
        );
    }

    public getTracingOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl).pipe(
            tap(tracingoptions => this.log('fetched all tracing options')),
            catchError(this.handleError<any[]>('getTracingOptions'))
        );
    }

    public addLinkage(linkage: Linkage) {
        return this.http.post(this.API_URL + this.url + '/linkpatient', JSON.stringify(linkage), httpOptions).pipe(
            tap((addedLinkage: Linkage) => this.log(`added linkage w/ id`)),
            catchError(this.handleError<Linkage>('addLinkage'))
        );
    }

    public addReferralTracing(referral: Referral, tracing: Tracing[]): Observable<Referral> {
        const trace = [];
        for (let i = 0; i < tracing.length; i++) {
            const mode = tracing[i]['mode']['itemId'];
            const outcome = tracing[i]['outcome']['itemId'];
            trace.push({
                TracingDate: tracing[i].tracingDate,
                Mode: mode,
                Outcome: outcome
            });
        }

        const Indata = {
            ReferredTo : referral.referredTo,
            DateToBeEnrolled: referral.dateToBeEnrolled,
            ReferralReason: referral.referralReason,
            UserId: referral.userId,
            ServiceAreaId: referral.serviceAreaId,
            PersonId: referral.personId,
            FromFacilityId: referral.facilityId,
            Tracing: trace
        };

        return this.http.post(this.API_URL + this.url, JSON.stringify(Indata), httpOptions).pipe(
            tap((addedLinkageTracing: Referral) => this.log(`added referral and tracing w/ id`)),
            catchError(this.handleError<Referral>('addReferralTracing'))
        );
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            return Observable.throw(error.message);
        };
    }

    /** Log a HeroService message with the MessageService */
    private log(message: string) {
        console.log(message);
    }

}
