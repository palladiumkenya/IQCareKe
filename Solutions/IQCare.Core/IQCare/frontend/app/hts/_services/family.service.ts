import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {catchError, tap} from 'rxjs/operators';
import {FamilyScreening} from '../_models/familyScreening';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class FamilyService {
    private API_URL = environment.API_URL;
    private url = '/api/Register';
    private lookup = '/api/Lookup/getCustomOptions';
    private pns = '/api/HtsEncounter/pnsScreening';

    constructor(private http: HttpClient) { }

    public getClientFamily(patientId: number): Observable<any[]> {
        const relationshipTypes = JSON.stringify(['Sibling', 'Child', 'Father', 'Mother']);

        return this.http.post<any[]>(this.API_URL + this.url + '/getPartners/?patientId='
            + patientId, relationshipTypes, httpOptions).pipe(
            tap(getClientFamily => this.log('fetched all family')),
            catchError(this.handleError<any[]>('getClientFamily'))
        );
    }

    public getCustomOptions(): Observable<any[]> {
        const options = JSON.stringify(['YesNo', 'ScreeningHivStatus', 'FamilyScreening']);

        return this.http.post<any[]>(this.API_URL + this.lookup, options, httpOptions).pipe(
            tap(getCustomOptions => this.log('fetched family screening options')),
            catchError(this.handleError<any[]>('getCustomOptions'))
        );
    }

    public addFamilyScreening(screeningForm: FamilyScreening, familyScreening: any[]): Observable<any[]> {
        const Indata = {
            'Screening': familyScreening,
            'ScreeningDate': screeningForm.dateOfScreening,
            'BookingDate': screeningForm.dateBooked,
            'PatientId': screeningForm.patientId,
            'PersonId': screeningForm.personId,
            'PatientMasterVisitId': screeningForm.patientMasterVisitId,
            'UserId': screeningForm.userId
        };

        console.log('post');

        return this.http.post<any[]>(this.API_URL + this.pns, JSON.stringify(Indata), httpOptions).pipe(
            tap(addFamilyScreening => this.log(`add family screening w/ id`)),
            catchError(this.handleError<any[]>('addFamilyScreening'))
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
