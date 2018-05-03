import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {catchError, tap} from 'rxjs/operators';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import {Pnsform} from '../_models/pnsform';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class PnsService {
    private API_URL = environment.API_URL;
    private url = '/api/Register';
    private pns = '/api/HtsEncounter/pnsScreening';
    private lookup = '/api/Lookup/getCustomOptions';

    constructor(private http: HttpClient,
                private errorHandler: ErrorHandlerService) { }

    public getClientPartners(patientId: number): Observable<any[]> {
        const relationshipTypes = JSON.stringify(['Co-Wife', 'Partner', 'Spouse']);

        return this.http.post<any[]>(this.API_URL + this.url + '/getPartners/?patientId='
            + patientId, relationshipTypes, httpOptions).pipe(
            tap(getClientPartners => this.errorHandler.log('fetched all partners')),
            catchError(this.errorHandler.handleError<any[]>('getClientPartners'))
        );
    }

    public getCustomOptions(): Observable<any[]> {
        const options = JSON.stringify(['YesNoNA', 'YesNo', 'IpvOutcome', 'YesNoDeclined', 'PNSRelationship', 'HivStatus', 'PnsApproach']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getCustomOptions => this.errorHandler.log('fetched all custom options')),
            catchError(this.errorHandler.handleError<any[]>('getCustomOptions'))
        );
    }

    public getScreeningCategories(): Observable<any[]> {
        const options = JSON.stringify(['PnsScreening']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getScreeningCategories => this.errorHandler.log('fetched all screening categories')),
            catchError(this.errorHandler.handleError<any[]>('getScreeningCategories'))
        );
    }

    public addPnsScreening(pnsForm: Pnsform, pnsScreening: any[]): Observable<any> {
        const Indata = {
            'Screening': pnsScreening,
            'ScreeningDate': pnsForm.screeningDate,
            'Occupation': pnsForm.occupation,
            'BookingDate': pnsForm.bookingDate,
            'PatientId': pnsForm.patientId,
            'PersonId': pnsForm.personId,
            'PatientMasterVisitId': pnsForm.patientMasterVisitId,
            'UserId': pnsForm.userId
        };

        return this.http.post<any>(this.API_URL + this.pns, JSON.stringify(Indata), httpOptions).pipe(
            tap(addedPnsScreening => this.errorHandler.log('add pns screening')),
            catchError(this.errorHandler.handleError<any[]>('addPnsScreening'))
        );
    }
}
