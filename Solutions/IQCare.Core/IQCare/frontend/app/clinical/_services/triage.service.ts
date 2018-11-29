import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { AddPatientVitalCommand } from "../_models/AddPatientVitalCommand";
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class TriageService {
    private API_URL = environment.API_URL;

    constructor(private httpClient: HttpClient, private errorHandlerService: ErrorHandlerService) {

    }


    public AddPatientVitalInfo(patientVitalCommand: AddPatientVitalCommand): Observable<any> {
        return this.httpClient.post<AddPatientVitalCommand>(this.API_URL + '/api/PatientVitals/Add',
            JSON.stringify(patientVitalCommand), httpOptions).pipe(
                tap(AddPatientTriageInfo => this.errorHandlerService.log(`successfully added patient vitals info`)),
                catchError(this.errorHandlerService.handleError<any>('Error adding ntmastervisit'))
            );
    }


    public GetPatientVitalsInfo(masterVisitId: number): Observable<any> {
        return this.httpClient.get<any>(this.API_URL + '/api/PatientVitals/GetByMasterVisitId/' + masterVisitId).pipe(
            tap(GetPatientVitalsInfo => this.errorHandlerService.log('get patient master visit details')),
            catchError(this.errorHandlerService.handleError<any>('GetPatientVitalsInfo'))
        );
    }

    public calculateBmi(weight: number, heightInCm: number): any {
        const heightInMetres = heightInCm / 100;
        return weight / (heightInMetres * heightInMetres);
    }
}
