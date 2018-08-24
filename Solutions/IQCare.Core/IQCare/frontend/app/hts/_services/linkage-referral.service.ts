import { Store } from '@ngrx/store';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { Referral } from '../_models/referral';
import { Tracing } from '../_models/tracing';
import { Linkage } from '../_models/linkage';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class LinkageReferralService {
    private API_URL = environment.API_URL;
    private _lookupurl = '/api/lookup/htsTracingOptions';
    private url = '/api/Referral';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getReferralReasons() {
        const options = JSON.stringify(['ReferralReason']);

        return this.http.post<any[]>(this.API_URL + '/api/Lookup/getCustomOptions', options, httpOptions).pipe(
            tap(getReferralReasons => this.errorHandler.log('get referral reasons')),
            catchError(this.errorHandler.handleError<any[]>('getReferralReasons'))
        );
    }

    public filterFacilities(filterString: string) {
        return this.http.get<any[]>(this.API_URL + '/api/Lookup/searchFacilityList?searchString=' + filterString).pipe(
            tap(filterFacilities => this.errorHandler.log('fetched filtered facilities')),
            catchError(this.errorHandler.handleError<any[]>('filterFacilities'))
        );
    }

    public getFacility(mflCode: string) {
        return this.http.get<any>(this.API_URL + '/api/Lookup/getFacility/' + mflCode).pipe(
            tap(getFacility => this.errorHandler.log('get Facility')),
            catchError(this.errorHandler.handleError<any[]>('getFacility'))
        );
    }

    public getTracingOptions(): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this._lookupurl).pipe(
            tap(tracingoptions => this.errorHandler.log('fetched all tracing options')),
            catchError(this.errorHandler.handleError<any[]>('getTracingOptions'))
        );
    }

    public addLinkage(linkage: Linkage) {
        return this.http.post(this.API_URL + this.url + '/linkpatient', JSON.stringify(linkage), httpOptions).pipe(
            tap((addedLinkage: Linkage) => this.errorHandler.log(`added linkage w/ id`)),
            catchError(this.errorHandler.handleError<Linkage>('addLinkage'))
        );
    }

    public getPersonLinkage(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Linkage/GetPersonLinkage/' + personId).pipe(
            tap((getPersonLinkage) => this.errorHandler.log(`fetched client linkage`)),
            catchError(this.errorHandler.handleError<any[]>('getPersonLinkage'))
        );
    }

    public getClientReferral(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + this.url + '/getClientReferral/' + personId).pipe(
            tap((getClientReferral) => this.errorHandler.log(`fetched client referral`)),
            catchError(this.errorHandler.handleError<any[]>('getClientReferral'))
        );
    }

    public getClientPreviousTracing(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/hts/Tracing/GetPersonTracingList/' + personId).pipe(
            tap((getClientPreviousTracing) => this.errorHandler.log(`fetched client previous referral`)),
            catchError(this.errorHandler.handleError<any[]>('getClientPreviousTracing'))
        );
    }

    public addReferralTracing(referral: Referral, tracing: Tracing[], tracingType: number, isEdit: boolean): Observable<Referral> {
        const trace = [];
        for (let i = 0; i < tracing.length; i++) {
            const mode = tracing[i]['mode']['itemId'];
            const outcome = tracing[i]['outcome']['itemId'];
            trace.push({
                TracingDate: tracing[i].tracingDate,
                Mode: mode,
                Outcome: outcome,
                TracingType: tracingType
            });
        }

        const Indata = {
            ReferredTo: referral.referredTo,
            DateToBeEnrolled: referral.dateToBeEnrolled,
            ReferralReason: referral.referralReason,
            UserId: referral.userId,
            ServiceAreaId: referral.serviceAreaId,
            PersonId: referral.personId,
            FromFacilityId: referral.facilityId,
            Tracing: trace,
            IsEdit: isEdit,
            OtherFacility: referral.otherFacility
        };

        return this.http.post(this.API_URL + this.url, JSON.stringify(Indata), httpOptions).pipe(
            tap((addedLinkageTracing: Referral) => this.errorHandler.log(`added referral and tracing w/ id`)),
            catchError(this.errorHandler.handleError<Referral>('addReferralTracing'))
        );
    }
}
