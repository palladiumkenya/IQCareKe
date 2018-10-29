import { PatientMasterVisitEncounter } from './../_models/PatientMasterVisitEncounter';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PncService {
    private API_URL = environment.API_URL;
    private API_LAB_URL = environment.API_LAB_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public savePncMasterVisit(patientMasterVisitEncounter: PatientMasterVisitEncounter): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientMasterVisit', JSON.stringify(patientMasterVisitEncounter), httpOptions).pipe(
            tap(savePncMasterVisit => this.errorHandler.log(`successfully added pnc patientmastervisit`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc patientmastervisit'))
        );
    }
}
