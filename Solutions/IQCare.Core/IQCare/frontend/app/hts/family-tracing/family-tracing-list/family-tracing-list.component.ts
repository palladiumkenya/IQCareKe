import { NotificationService } from './../../../shared/_services/notification.service';
import { PnsService } from './../../_services/pns.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, NgZone, AfterViewInit, ViewChild } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';

@Component({
    selector: 'app-family-tracing-list',
    templateUrl: './family-tracing-list.component.html',
    styleUrls: ['./family-tracing-list.component.css']
})
export class FamilyTracingListComponent implements OnInit, AfterViewInit {
    displayedColumns = ['tracingDate', 'tracingMode', 'tracingOutcome', 'consent', 'dateBookedTesting'];
    dataSource = new MatTableDataSource();
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    personId: number;

    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private pnsService: PnsService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.personId = JSON.parse(localStorage.getItem('partnerId'));
        this.dataSource.sort = this.sort;

        this.pnsService.geTracingList(this.personId).subscribe(
            (result) => {
                console.log(result);
                this.dataSource.data = result;
            },
            (error) => {
                this.snotifyService.error('Error fetching person tracing records ' + error, 'SEARCH', this.notificationService.getConfig());
            }
        );
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
    }

    newTracing() {
        this.zone.run(() => { this.router.navigate(['/hts/family/tracing'], { relativeTo: this.route }); });
    }
}
