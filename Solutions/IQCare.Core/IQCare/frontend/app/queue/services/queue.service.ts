import { of, Observable, BehaviorSubject } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/abstract_emitter';
import { AddRoomComponent } from '../room/add-room/add-room.component';



const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable()
export class QueueDetailsService {
    private API_QUEUE_URL = environment.API_QUEUE_URL;
    private API_URL = environment.API_URL;

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
    getPriorityList(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetPriorityItems').pipe(tap(
            getPriorityList => this.errorHandler.log(`fetched  priorities`)),
            catchError(this.errorHandler.handleError<any[]>(' getPriorityList'))
        );
    }
    getServiceAreas(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetServiceAreas').pipe(tap(
            getServiceAreas => this.errorHandler.log(`fetched  service areas`)),
            catchError(this.errorHandler.handleError<any[]>(' getServiceAreas'))
        );
    }


    getLinkedRooms(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetAllLinkedRooms').pipe(tap(
            getLinkedRooms => this.errorHandler.log(`fetched linked rooms`)),
            catchError(this.errorHandler.handleError<any[]>('getLinkedRooms'))
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

    checkRoomLinkageExists(serviceAreaId: number, roomId: number, servicePointId: number): Observable<any[]> {
        const Indata = {
            'ServiceAreaId': serviceAreaId,
            'RoomId': roomId,
            'ServicePointId': servicePointId
        };
        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/CheckRoomLinkageExists', JSON.stringify(Indata), httpOptions).pipe(
            tap((checkRoomLinkageExist: any) => this.errorHandler.log(`Check Room Linkage Exists`)),
            catchError(this.errorHandler.handleError<any>('checkRoomLinkageExist'))
        );

    }

    addRoomLinkage(serviceAreaId: number, roomId: number, servicePointId: number, deletef: Boolean, createdby: number): Observable<any[]> {
        const LinkageList = [];
        LinkageList.push({
            'ServiceAreaId': serviceAreaId,
            'Roomid': roomId,
            'ServicePointId': servicePointId,
            'DeleteFlag': deletef,
            'UserId': createdby

        });
        const Indata = {
            'Linkagelist': LinkageList
        };
        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/AddRoomLinkage', JSON.stringify(Indata), httpOptions).pipe(
            tap((addRoomLinkage: any) => this.errorHandler.log(`Add Room  Linkage`)),
            catchError(this.errorHandler.handleError<any>('addRoomLinkage'))
        );
    }

    editroomlinkage(id: number, servicePointId: number,
        roomId: number, serviceAreaId: number, deletef: Boolean, active: boolean, updatedBy: number) {


        const Indata = {
            'Id': id,
            'ServicePointId': servicePointId,
            'RoomId': roomId,
            'ServiceAreaId': serviceAreaId,
            'DeleteFlag': deletef,
            'Active': active,
            'UpdatedBy': updatedBy
        };

        return this.http.post<any[]>(this.API_QUEUE_URL + '/api/Queue/EditRoomLinkage', JSON.stringify(Indata), httpOptions).pipe(
            tap((editroomlinkage: any) => this.errorHandler.log(`Edit Room  Linkage`)),
            catchError(this.errorHandler.handleError<any>('editRoomLinkage'))
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

    getWaitingListByPatientId(id: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetQueueListByPatientId/' + id).pipe(tap(
            getWaitingListByPatientId => this.errorHandler.log(`fetched  PatientWaitingList by PatientId`)),
            catchError(this.errorHandler.handleError<any[]>('getWaitigListByPatientId'))
        );
    }

    getWaitingListByServiceAreaId(id: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetQueueListByServiceAreaId/' + id).pipe(tap(
            getWaitingListByServiceAreaId => this.errorHandler.log(`fetched WaitingList by ServiceArea`)),
            catchError(this.errorHandler.handleError<any[]>('getWaitingListByServiceAreaId'))
        );
    }
    getWaitingList(): Observable<any[]> {
        return this.http.get<any[]>(this.API_QUEUE_URL + '/api/Queue/GetQueueList').pipe(tap(
            getWaitingList => this.errorHandler.log(`fetched  all waitinglist`)),
            catchError(this.errorHandler.handleError<any[]>('getWaitingList'))
        );
    }

   
}

