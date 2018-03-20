import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {of} from 'rxjs/observable/of';
import {Referral} from '../_models/referral';
import {Encounter} from '../_models/encounter';
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

    public getTracingOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl).pipe(
            tap(tracingoptions => this.log('fetched all tracing options')),
            catchError(this.handleError<any[]>('getTracingOptions'))
        );
    }

    public addLinkage(linkage) {
        return this.http.post(this.API_URL + this.url, JSON.stringify(), httpOptions).pipe(
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
            ReferredTo : referral.toFacility,
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

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    /** Log a HeroService message with the MessageService */
    private log(message: string) {
        console.log(message);
    }

}
