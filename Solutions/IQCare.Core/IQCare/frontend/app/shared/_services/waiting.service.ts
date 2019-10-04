import { of, Observable, BehaviorSubject } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/abstract_emitter';




const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable()
export class WaitingListService {
    private API_QUEUE_URL = environment.API_QUEUE_URL;
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    getPriorityList(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetPriorityItems').pipe(tap(
            getPriorityList => this.errorHandler.log(`fetched  priorities`)),
            catchError(this.errorHandler.handleError<any[]>(' getPriorityList'))
        );
    }

    getWaitingListByPatientId(id: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetQueueListByPatientId/' + id).pipe(tap(
            getWaitingListByPatientId => this.errorHandler.log(`fetched  PatientWaitingList by PatientId`)),
            catchError(this.errorHandler.handleError<any[]>('getWaitigListByPatientId'))
        );
    }
    getLinkedRooms(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetAllLinkedRooms').pipe(tap(
            getLinkedRooms => this.errorHandler.log(`fetched linked rooms`)),
            catchError(this.errorHandler.handleError<any[]>('getLinkedRooms'))
        );
    }
    checkQueueExists(ServiceRoomId: number, patientId: number): Observable<any[]> {
        const Indata = {
            'ServiceRoomId': ServiceRoomId,
            'PatientId': patientId,

        };
        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/CheckQueueExists', JSON.stringify(Indata), httpOptions).pipe(
            tap((checkQueueExists: any) => this.errorHandler.log(`Check Queue Exists`)),
            catchError(this.errorHandler.handleError<any>('checkQueueExists'))
        );

    }
    editQueue(id: number, deleteflag: boolean, status: boolean, updatedBy: number) {
        const Indata = {
            'Id': id,
            'DeleteFlag': deleteflag,
            'Status': status,
            'UpdatedBy': updatedBy

        };

        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/EditQueueList', JSON.stringify(Indata), httpOptions).pipe(
            tap((editQueue: any) => this.errorHandler.log(`Edited the Queue  `)),
            catchError(this.errorHandler.handleError<any>('EditQueue'))
        );
    }



    addQueue(serviceRoomId: number, patientid: number,
        priorityid: number, deletef: Boolean,
        status: Boolean, createdby: number): Observable<any[]> {

        const Indata = {
            'ServiceRoomId': serviceRoomId,
            'PatientId': patientid,
            'Priority': priorityid,
            'DeleteFlag': deletef,
            'Status': status,
            'CreatedBy': createdby

        };

        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/AddQueue', JSON.stringify(Indata), httpOptions).pipe(
            tap((addQueue: any) => this.errorHandler.log(`Added to the Queue  `)),
            catchError(this.errorHandler.handleError<any>('addQueue'))
        );
    }
}