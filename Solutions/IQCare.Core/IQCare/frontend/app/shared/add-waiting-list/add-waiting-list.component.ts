
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Component, OnInit, NgZone,Output, EventEmitter, Inject, AfterViewInit, ViewChild } from '@angular/core';
import { WaitingListService } from '../_services/waiting.service';
import { DialogService } from '../_services/dialog.service';
import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';

import { NotificationService } from '../_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatTableDataSource, MatButton, MatPaginator, MatSort } from '@angular/material';
import * as moment from 'moment';
import {
    Priority, WaitingList, ServiceRoomList,
    serviceRoom, Roomlist, serviceAreas, servicePoint, PatientList, SpecificRoomLinkage
} from '../_models/waitinglist';


@Component({
  selector: 'app-add-waiting-list',
  templateUrl: './add-waiting-list.component.html',
  styleUrls: ['./add-waiting-list.component.css']
})
export class AddWaitingListComponent implements OnInit {
    ServiceRoomItems: any[] = [];
    items: ServiceRoomList[] = [];

    QueueList: PatientList[] = [];
    PatientWaitingList: any[] = [];
    waitList: WaitingList;
    addwaitList: WaitingList;
    formGroup: FormGroup;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    Priorityitems: any[] = [];
    Priorities: Priority[] = [];
    patientId: number;
    personId: number;
    exists: boolean;
    updated: boolean;
    configuration: boolean;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns: any[] = ['FirstName', 'LastName', 'MiddleName', 'ServiceArea'
        , 'ServicePoint', 'RoomName', 'Priority', 'AddedBy', 'Action'];
        dataSource = new MatTableDataSource(this.QueueList);
  constructor(private formBuilder: FormBuilder,
    private queuedetailsservice: WaitingListService,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService,
    private route: ActivatedRoute,
    public zone: NgZone,
    private router: Router,
    private dialogService: DialogService,
    private spinner: NgxSpinnerService,
    private dialogRef: MatDialogRef<AddWaitingListComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { 

    this.patientId = data.patientId;
    this.personId = data.personId;
     }

ngOnInit() {

    /* this.route.params.subscribe((param) => {
        this.patientId = param.patientId;
        this.personId = param.personId;
    }); */
    
    this.getLinkedRooms();
    this.getPriorityList();
    this.waitList = new WaitingList();
    this.getQueueListByPatientId();
    this.formGroup = this.formBuilder.group({
        Priority: new FormControl(this.waitList.PriorityId, [Validators.required]),
        ServiceRoom: new FormControl(this.waitList.ServiceRoomId, [Validators.required])
    });
}
checkServiceRoomExist(): Boolean{
    if (this.items.length < 1) {
        this.configuration = true;
    } else {
        this.configuration = false;
    }
        return this.configuration;
}

AddServiceRoom() {
    this.close();
    this.zone.run(() => {
        this.router.navigate(['/queue/link'],
            { relativeTo: this.route });
    });

}
getPriorityList() {
    this.queuedetailsservice.getPriorityList().subscribe((result) => {
        this.Priorityitems = result['priority'];
        this.Priorityitems.forEach(element => {
            this.Priorities.push({
                id: element.itemId,
                name: element.itemDisplayName
            });
        });
        console.log(this.Priorities);
    },
        (error) => {
            this.snotifyService.error('Error extractiong the priority list' + error, 'List',
                this.notificationService.getConfig());
        },
        () => {
        });
}

getQueueListByPatientId() {
    this.queuedetailsservice.getWaitingListByPatientId(this.patientId).subscribe
        ((result) => {
            this.PatientWaitingList = result['waitingListViews'];
            console.log(result);
            console.log(this.PatientWaitingList);
            this.QueueList = [];
            this.PatientWaitingList.forEach(x => {
                this.QueueList.push({
                    FirstName: x.firstName,
                    MiddleName: x.middleName,
                    LastName: x.lastName,
                    Id: x.id,
                    ServiceAreaName: x.serviceAreaName,
                    ServicePointName: x.servicePointName,
                    RoomName: x.roomName,
                    Priority: x.priority,
                    CreatedBy: x.createdBy,
                    CreateDate: x.createDate,
                    PatientId : x.patientId,
                    PersonId : x.personId
                   
                });



            });
            console.log(this.QueueList);
            this.dataSource = new MatTableDataSource(this.QueueList);
            this.dataSource.paginator = this.paginator;
        },
            (error) => {
                this.snotifyService.error('Error extracting the patient waiting list' + error, 'WaitingList',
                    this.notificationService.getConfig());
            },
            () => {
            });
}

getLinkedRooms() {
    this.queuedetailsservice.getLinkedRooms().subscribe((result) => {
        console.log(result);
        this.ServiceRoomItems = result['roomServices'];
        console.log(this.ServiceRoomItems);
        this.ServiceRoomItems.forEach(x => {
            this.items.push({
                id: x.id,
                name: x.name
            });
        });

        console.log(this.items);
    },
        (error) => {
            this.snotifyService.error('Error extractiong the service room list' + error, 'List',
                this.notificationService.getConfig());
        },
        () => {

        }
    );
}

AddQueue() {
    if (this.formGroup.valid) {
        this.spinner.show();
        this.addwaitList = new WaitingList();

        this.addwaitList.PriorityId = this.formGroup.controls.Priority.value;
        this.addwaitList.ServiceRoomId = this.formGroup.controls.ServiceRoom.value;

        this.queuedetailsservice.checkQueueExists(this.addwaitList.ServiceRoomId, this.patientId
        ).subscribe((result) => {
            console.log(result);
            this.exists = result['exists'];
            if (this.exists == true) {
                this.snotifyService.error('Kindly note you have already added the patient in the waitinglist', 'WaitingList',
                    this.notificationService.getConfig());

                this.spinner.hide();
                return;

            } else {
                const createdby = parseInt(localStorage.getItem('appUserId'));
                this.queuedetailsservice.addQueue(this.addwaitList.ServiceRoomId,
                    this.patientId, this.addwaitList.PriorityId, false, false, createdby).subscribe((result) => {
                        this.snotifyService.success(result['message'], 'Add WaitingList',
                            this.notificationService.getConfig());
                        this.getQueueListByPatientId();


                    },
                        (error) => {
                            this.snotifyService.error('Error adding Patient to the Waiting List ' + error, 'WaitingList',
                                this.notificationService.getConfig());
                            this.spinner.hide();
                        },
                        () => {
                            this.spinner.hide();
                            this.formGroup.reset();
                            this.getQueueListByPatientId();
                        }
                    );
            }
        },
            (error) => {
                this.snotifyService.error('Error adding patient to the waiting list ' + error, 'WaitingList',
                    this.notificationService.getConfig());
                this.spinner.hide();
            },
            () => {
                this.spinner.hide();
            }
        );

    }
}

OnDelete(Id: number) {
    this.dialogService.openConfirmDialog('Are you sure to delete this record ?')
        .afterClosed().subscribe(res => {
            if (res) {
                const updatedby = parseInt(localStorage.getItem('appUserId'));

                const queueIndividual = this.QueueList.find(x => x.Id == Id);

                console.log(queueIndividual);
                console.log(queueIndividual.Id);
                this.queuedetailsservice.editQueue(queueIndividual.Id, true,
                    false, updatedby).subscribe((result) => {
                        console.log(result);
                        this.updated = result['updated'];
                        if (this.updated == true) {
                            this.snotifyService.success('Kindly note the item in the '
                                + '  waiting has been deleted', 'WaitingList',
                                this.notificationService.getConfig());


                        }

                        this.spinner.hide();
                        this.getQueueListByPatientId();


                    },
                        (error) => {
                            this.snotifyService.error('Error deleting the patient in the waitingList ' + error, 'WaitingList',
                                this.notificationService.getConfig());
                            this.spinner.hide();
                        },
                        () => {
                            this.getQueueListByPatientId();

                            this.spinner.hide();


                        }
                    );


            }
        });


}

public close() {
    this.dialogRef.close();
}

}



