
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { MatDialog, MatDialogConfig } from "@angular/material";
import { Component, OnInit, NgZone, EventEmitter, Inject, AfterViewInit, ViewChild } from '@angular/core';
import { QueueDetailsService } from '../../services/queue.service';
import { DialogService } from '../../services/dialog.service';
import { FormGroup, FormBuilder, AbstractControl } from '@angular/forms';

import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatTableDataSource, MatButton, MatPaginator, MatSort } from '@angular/material';
import * as moment from 'moment';
import { Priority, serviceRoom, Roomlist, serviceAreas, servicePoint, SpecificRoomLinkage } from '../../models/model';
@Component({
    selector: 'app-link-room',
    templateUrl: './link-room.component.html',
    styleUrls: ['./link-room.component.css']
})
export class LinkRoomComponent implements OnInit {

    LookupItems: any[] = [];
    ServiceAreas: serviceAreas[] = [];
    Priority: Priority[] = [];
    ServicePoint: servicePoint[] = [];
    rooms: Roomlist[] = [];
    ServiceRoomLink: any[] = [];
    LinkedRooms: any[] = [];
    SpecificRoomLink: SpecificRoomLinkage[] = [];
    servicerm: serviceRoom;
    exists: boolean;
    updated: boolean;
    formGroup: FormGroup;
    configuration: boolean;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    displayedColumns: any[] = ['serviceAreaName', 'servicePointName', 'roomName', 'Action'];
    dataSource = new MatTableDataSource(this.ServiceRoomLink);

    constructor(private formBuilder: FormBuilder,
        private queuedetailsservice: QueueDetailsService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        public zone: NgZone,
        private router: Router,
        private dialogService: DialogService,
        private spinner: NgxSpinnerService) {

        this.servicerm = new serviceRoom();
        this.formGroup = this.formBuilder.group({
            ServiceArea: new FormControl(this.servicerm.ServiceAreaId, [Validators.required]),
            ServicePoint: new FormControl(this.servicerm.ServicePointId, [Validators.required]),
            Room: new FormControl(this.servicerm.RoomId, [Validators.required])

        });
    }

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
    }
    ngOnInit() {
        this.servicerm = new serviceRoom()
        this.route.data.subscribe((res) => {
            const { LookupItemList } = res;
            this.LookupItems = LookupItemList;

            console.log(this.LookupItems);
            console.log(this.LookupItems['priority']);
            this.LookupItems['priority'].forEach(element => {
                this.Priority.push({
                    id: element.itemId,
                    name: element.itemDisplayName
                });
            });
            this.LookupItems['serviceAreas'].forEach(data => {
                this.ServiceAreas.push({
                    id: data.id,
                    displayName: data.displayName
                });
            });


            this.LookupItems['rooms'].forEach(data => {
                this.rooms.push({
                    id: data.id,
                    displayName: data.displayName
                });
            });



            this.LookupItems['servicePoint'].forEach(data => {
                this.ServicePoint.push({
                    id: data.itemId,
                    name: data.itemDisplayName
                });
            });



        });
        this.getLinkedRooms();
    }
    checkRooms(): Boolean {
        if (this.rooms.length < 1) {
            this.configuration = true;
        } else {
            this.configuration = false;
        }
        return this.configuration;
    }

    getLinkedRooms() {
        this.ServiceRoomLink = [];
        this.queuedetailsservice.getLinkedRooms().subscribe((result) => {
            this.LinkedRooms = result['serviceRoomViews'];
            console.log(this.LinkedRooms);
            this.LinkedRooms.forEach(data => {
                this.ServiceRoomLink.push({
                    serviceAreaName: data.serviceAreaName,
                    serviceAreaId: data.serviceAreaId,
                    servicePointId: data.servicePointId,
                    servicePointName: data.servicePointName,
                    roomName: data.roomName,
                    roomId: data.roomId,
                    id: data.id
                });
            });
            this.dataSource = new MatTableDataSource(this.ServiceRoomLink);
            this.dataSource.paginator = this.paginator;
        });

    }
    OnDelete(Id: number) {
        this.dialogService.openConfirmDialog('Are you sure to delete this record ?')
            .afterClosed().subscribe(res => {
                if (res) {
                    const updatedby = parseInt(localStorage.getItem('appUserId'));

                    this.SpecificRoomLink = this.ServiceRoomLink.find(x => x.id == Id);

                    console.log(this.SpecificRoomLink);
                    console.log(this.SpecificRoomLink['id']);
                    this.queuedetailsservice.editroomlinkage(this.SpecificRoomLink['id'], this.SpecificRoomLink['servicePointId'],
                        this.SpecificRoomLink['roomId'],
                        this.SpecificRoomLink['serviceAreaId'],
                        true, true, updatedby).subscribe((result) => {
                            console.log(result);
                            this.updated = result['updated'];
                            if (this.updated == true) {
                                this.snotifyService.success('Kindly note the linked room '
                                    + ' to the servicepoint and service area has been deleted', 'Link',
                                    this.notificationService.getConfig());


                            }

                            this.spinner.hide();

                        },
                            (error) => {
                                this.snotifyService.error('Error deleting the room linkage to service area and service point ' + error, 'Linkage',
                                    this.notificationService.getConfig());
                                this.spinner.hide();
                            },
                            () => {
                                this.ServiceRoomLink = [];
                                this.getLinkedRooms();
                                this.spinner.hide();


                            }
                        );
                }
            });
    }

    AddRoom() {
        this.zone.run(() => {
            this.router.navigate(['/queue/'],
                { relativeTo: this.route });
        });
    }

    AddLinkage() {
        if (this.formGroup.valid) {
            this.spinner.show();
            this.servicerm = new serviceRoom();
            console.log(this.servicerm);
            console.log(this.formGroup);
            this.servicerm.ServiceAreaId = this.formGroup.controls.ServiceArea.value;
            this.servicerm.RoomId = this.formGroup.controls.Room.value;
            this.servicerm.ServicePointId = this.formGroup.controls.ServicePoint.value;
            console.log(this.servicerm);
            this.queuedetailsservice.checkRoomLinkageExists(
                this.servicerm.ServiceAreaId, this.servicerm.RoomId
                , this.servicerm.ServicePointId).subscribe((result) => {
                    console.log(result);
                    this.exists = result['exists'];
                    if (this.exists == true) {
                        this.snotifyService.error('Kindly note the room has' +
                            ' already been linked' + ' to the servicepoint and service area', 'Link',
                            this.notificationService.getConfig());

                        this.spinner.hide();
                        return;

                    } else {
                        const createdby = parseInt(localStorage.getItem('appUserId'));
                        this.queuedetailsservice.addRoomLinkage(this.servicerm.ServiceAreaId, this.servicerm.RoomId
                            , this.servicerm.ServicePointId, false, createdby).subscribe((result) => {
                                this.snotifyService.success(result['message'], 'Add Linkage',
                                    this.notificationService.getConfig());
                                this.ServiceRoomLink = [];
                                this.getLinkedRooms()

                            },
                                (error) => {
                                    this.snotifyService.error('Error Linking rooms to swrviceArea and servicePoint ' + error, 'Linkage',
                                        this.notificationService.getConfig());
                                    this.spinner.hide();
                                },
                                () => {
                                    this.spinner.hide();
                                    this.formGroup.reset();
                                }
                            );
                    }
                },
                    (error) => {
                        this.snotifyService.error('Error Linking rooms to swrviceArea and servicePoint ' + error, 'Linkage',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                    () => {
                        this.spinner.hide();
                    }
                );

        }
    }

}
