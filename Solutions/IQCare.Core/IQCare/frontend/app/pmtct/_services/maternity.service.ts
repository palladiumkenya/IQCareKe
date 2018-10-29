import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {ErrorHandlerService} from '../../shared/_services/errorhandler.service';
import {catchError, tap} from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
};


@Injectable({
    providedIn: 'root'
})
export class MaternityService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {
    }

    public getCurrentVisitDetails(patientId: number) {
        return this.http.get<any>(this.API_URL + '/api/VisitDetails/GetCurrentVisit/' + patientId).pipe(
            tap(getCurrentVisitDetails => this.errorHandler.log('get current visit data')),
            catchError(this.errorHandler.handleError<any[]>('getCurrentVisitDetails'))
        );
    }

}
