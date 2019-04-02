import { of, Observable, BehaviorSubject } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/abstract_emitter';
import { AddRoomComponent } from '../room/add-room/add-room.component';
import { now } from 'moment';
import moment = require('moment');


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable()
export class QueueDetailsService {
    private API_QUEUE_URL = environment.API_QUEUE_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {

    }

    getRooms(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetRooms').pipe(tap(
            getRooms => this.errorHandler.log(`fetched rooms`)),
            catchError(this.errorHandler.handleError<any[]>('getRooms'))
        );
    }
    getLookupList(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetLookupItems').pipe(tap(
            getLookupList => this.errorHandler.log(`fetched rooms`)),
            catchError(this.errorHandler.handleError<any[]>('getLookupList'))
        );
    }
    getRoomsById(id: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetRoomById/' + id).pipe(tap(
            getRoomsById => this.errorHandler.log(`fetched roomsbyId`)),
            catchError(this.errorHandler.handleError<any[]>('getRoomsbyId'))
        );
    }
    checkRoomExist(roomname: string): Observable<any[]> {
        const Indata = {
            'RoomName': roomname
        };
        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/CheckRoomExists', JSON.stringify(Indata), httpOptions).pipe(
            tap((checkRoomExist: any) => this.errorHandler.log(`Check Room Exists`)),
            catchError(this.errorHandler.handleError<any>('checkRoomExist'))
        );
    }
    editRoom(editroomList: any[]): Observable<any[]> {
        const IR = [];
        if (editroomList.length == 0) {
            return of([]);
        }
        console.log(editroomList);
        const Indata = {
            'Id': editroomList['id'],
            'RoomName': editroomList['roomName'],
            'DisplayName': editroomList['displayName'],
            'Description': editroomList['description'],
            'DeleteFlag': editroomList['deleteFlag'],
            'CreatedBy': editroomList['createdBy'],
            'Active': editroomList['active'],
            'CreateDate': editroomList['createDate'],
            'UpdateDate': editroomList['updateDate'],
            'UpdatedBy': editroomList['updatedBy']
        };
        console.log(Indata);

        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/EditRoom', JSON.stringify(Indata), httpOptions).pipe(
            tap((editRoom: any) => this.errorHandler.log(`Edit Room Exists`)),
            catchError(this.errorHandler.handleError<any>('editRoomExist'))
        );

    }
    addRoom(roomName: string, displayName: string, description: string
        , deleteFlag: boolean, createdBy: number, active: boolean, createDate: string

    ): Observable<any[]> {

        const Indata = {

            'RoomName': roomName,
            'DisplayName': displayName,
            'Description': description,
            'DeleteFlag': deleteFlag,
            'CreatedBy': createdBy,
            'Active': active,
            'CreateDate': createDate,
            'UpdateDate': '',
            'UpdatedBy': ''
        };


        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/AddRooms', JSON.stringify(Indata), httpOptions).pipe(
            tap((addRoom: any) => this.errorHandler.log(`Add Room Exists`)),
            catchError(this.errorHandler.handleError<any>('addRoomExist'))
        );
    }

}

