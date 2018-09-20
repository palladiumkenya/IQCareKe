import { Observable } from 'rxjs/index';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { environment } from '../../../environments/environment';
import { tap, catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class HeiService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public saveHieDelivery(patientId: number, patientMasterVisitId: number, heidelivery: any): Observable<any> {
        const Indata = {
            'PatientId': patientId,
            'PatientMasterVisitId': patientMasterVisitId,
            'PlaceOfDelivery': '',
            'ModeOfDelivery': '',
            'BirthWeight': '',
            'ProphylaxisReceived': ''
        };

        return this.http.post<any>(this.API_URL + '/api/DeliveryMaternalHistory', JSON.stringify(Indata), httpOptions).pipe(
            tap(saveHieDelivery => this.errorHandler.log(`successfully added hei delivery`)),
            catchError(this.errorHandler.handleError<any>('Error saving hei delivery'))
        );
    }
}
